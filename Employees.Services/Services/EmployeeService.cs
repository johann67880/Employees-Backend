namespace Employees.Services.Services
{
    using Employees.Domain;
    using Employees.Domain.Enums;
    using Employees.Domain.Interfaces;
    using Employees.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) =>
            this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

        /// <summary>
        /// Retrieves list of all employees
        /// </summary>
        /// <returns>List of employees</returns>
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = await this.employeeRepository.GetAllEmployeesAsync();
            return MapEmployees(employees);
        }

        /// <summary>
        /// Retrieves single employee that matches with the specific criteria
        /// </summary>
        /// <param name="id">Id Parameter</param>
        /// <returns>Employee</returns>
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var employees = await this.employeeRepository.GetEmployeeAsync(id);
            var result = MapEmployees(new List<Employee> { employees });
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Method that maps and calculate salary according to the type of employee. Apply Business Logic (That's what services stand for)
        /// </summary>
        /// <param name="employees">List of employees</param>
        /// <returns>List of employees with proper salary calculations.</returns>
        private List<Employee> MapEmployees(List<Employee> employees)
        {
            var list = new List<Employee>();

            employees.ForEach(emp =>
            {
                EmployeeTypes types = (EmployeeTypes)Enum.Parse(typeof(EmployeeTypes), emp.ContractTypeName);

                //Calculating salary depending on type of employee.
                switch(types)
                {
                    case EmployeeTypes.HourlySalaryEmployee:
                        emp = new HourlySalaryEmployee(emp);
                        emp.CalculateSalary();
                        break;
                    case EmployeeTypes.MonthlySalaryEmployee:
                        emp = new MonthlySalaryEmployee(emp);
                        emp.CalculateSalary();
                        break;
                    default:
                        break;
                }

                list.Add(emp);

            });

            return list;
        }
    }
}
