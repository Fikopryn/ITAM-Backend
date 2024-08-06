using Domain.R3Framework.R3User;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ITAM.AdminCon.Employee
{
    public interface IEmployeeService
    {
        Task<Result<List<EmployeeDto>>> GetAllEmployee();
        Task<Result<List<EmployeeDto>>> GetEmployeeList (string userId);
        Task<Result<EmployeeDto>> Update(EmployeeDto updateParam);
        Task<Result<EmployeeDto>> CreateEmployee(EmployeeDto createParam, R3UserSession userSession);
        Task<Result<EmployeeDto>> Delete(string company);
      
    }
}
