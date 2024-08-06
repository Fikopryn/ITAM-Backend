using Core.Interfaces.IRepositories.Tables;
using Core.Interfaces.IRepositories.Views;
using Microsoft.Data.SqlClient;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> ExecRawSqlAsync(string sql, List<SqlParameter> parameters);
        Task<IEnumerable<Dictionary<string, object>>> ExecuteQueryAsync(string query, params object[] parameters);
        IEnumerable<Dictionary<string, object>> ExecuteQuery(string query, params object[] parameters);
        Task<int> CompleteAsync();
        ITSessionRepository Sessions { get; }
        ITExPersonContactRepository ExPersonContacts { get; }
        ITExPersonIdentificationRepository ExPersonIdentifications { get; }
        ITExPersonRepository ExPersons { get; }

        IVwExPersonRepository VwExPersons { get; }
        ITExFileReferenceRepository ExFileReference { get; }
        ITExAuditTrailActivityRepository ExAuditTrailActivity { get; }
        ITExMasterLovRepository ExMasterLov { get; }
        ITAssetStatusRepository AssetStatus { get; }
        ITAssetStatusFinRepository AssetStatusFin { get; }
        ITCompanyRepository Company { get; }
        ITProductCatalogRepository ProductCatalog { get; }
        ITMasterRoleRepository MasterRole { get; }
        ITEmployeeRepository Employee {  get; }
        ITContractRepository Contract { get; }
        ITRequestPurchaseOrderRepository RequestPurchaseOrder { get; }
    }
}
