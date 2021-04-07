namespace Employees.Services.Interfaces
{
    using Employees.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();

        Task<Employee> GetEmployeeAsync(int id);
    }
}
