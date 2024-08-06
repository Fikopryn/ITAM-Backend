using Core.Interfaces;
using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Views;
using Data.Repositories.Tables;
using Data.Repositories.Views;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Dynamic;

namespace Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;

        public ITExPersonContactRepository ExPersonContacts { get; private set; }
        public ITExPersonIdentificationRepository ExPersonIdentifications { get; private set; }
        public ITExPersonRepository ExPersons { get; private set; }
        public IVwExPersonRepository VwExPersons { get; private set; }
        public ITExFileReferenceRepository ExFileReference { get; private set; }
        public ITExAuditTrailActivityRepository ExAuditTrailActivity { get; private set; }
        public ITExMasterLovRepository ExMasterLov { get; private set; }
        public ITSessionRepository Sessions { get; private set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            ExPersons = new TExPersonRepository(_context);
            ExPersonContacts = new TExPersonContactRepository(_context);
            ExPersonIdentifications = new TExPersonIdentificationRepository(_context);
            ExFileReference = new TExFileReferenceRepository(_context);
            ExAuditTrailActivity = new TExAuditTrailActivityRepository(_context);
            ExMasterLov = new TExMasterLovRepository(_context);
            Sessions = new TSessionRepository(_context);

            VwExPersons = new VwExPersonRepository(_context);
        }

        public async Task<int> ExecRawSqlAsync(string sql, List<SqlParameter> parameters) //use this method CAREFULLY or to call procedure only
        {
            _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
            return await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Dictionary<string, object>> ExecuteQuery(string query, params object[] parameters)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = SqlSanitize(query);

                if (parameters.Any())
                    command.Parameters.AddRange(parameters);

                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                using (var dataReader = command.ExecuteReader())
                {
                    var dataRow = ReadData(dataReader);

                    if (command.Connection.State == ConnectionState.Open)
                        command.Connection.Close();

                    return dataRow;
                }
            }
        }

        public async Task<IEnumerable<Dictionary<string, object>>> ExecuteQueryAsync(string query, params object[] parameters)
        {
            await using (var command = _context.Database.GetDbConnection().CreateCommand())
            {

                command.CommandText = SqlSanitize(query);

                if (parameters.Any())
                    command.Parameters.AddRange(parameters);

                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();

                await using (var dataReader = await command.ExecuteReaderAsync())
                {

                    var dataRow = ReadData(dataReader);

                    if (command.Connection.State == ConnectionState.Open)
                        command.Connection.Close();

                    return dataRow;
                }
            }
        }

        private IEnumerable<Dictionary<string, object>> ReadData(IEnumerable reader)
        {
            var dataList = new List<Dictionary<string, object>>();

            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();

                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }

                dataList.Add(new Dictionary<string, object>(expando));
            }

            return dataList;
        }

        private string SqlSanitize(string query)
        {
            return query.ToUpper().Replace("DROP", "").Replace("DELETE", "")/*.Replace("UPDATE", "")*/;
        }
    }
}
