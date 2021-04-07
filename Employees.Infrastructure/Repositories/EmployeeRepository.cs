namespace Employees.Infrastructure.Repositories
{
    using Employees.Domain;
    using Employees.Domain.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration Configuration;
        private string Uri = string.Empty;

        public EmployeeRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            Uri = Configuration["APIEndpoint"];
        }

        /// <summary>
        /// Retrieve list of all employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        public async Task<List<Employee>> GetAllEmployeesAsync() => 
            await this.GetAllDataAsync();

        /// <summary>
        /// Retrieve one single employee that matches with the specified criteria
        /// </summary>
        /// <param name="id">Id of Employee</param>
        /// <returns>Single Employee</returns>
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var employees = await this.GetAllDataAsync();
            return employees.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }

        /// <summary>
        /// Method that consumes API to retrieve all employees and map it to Employees domain object
        /// </summary>
        /// <returns>List of employees</returns>
        private async Task<List<Employee>> GetAllDataAsync()
        {
            List<Employee> employees = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Uri);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<Employee>>(result);
            }

            return employees;
        }
    }
}
