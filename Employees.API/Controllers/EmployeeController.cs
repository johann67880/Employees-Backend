namespace Employees.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Employees.Services.Interfaces;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowMyOrigin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            this._logger = logger;
        }

        /// <summary>
        /// Retrieves all employees
        /// </summary>
        /// <returns>Response with list of all employees</returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await this.employeeService.GetAllEmployeesAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retrieves single employee that matches with specific Id sent as parameter
        /// </summary>
        /// <param name="id">Id of employee</param>
        /// <returns>Response with single Employee</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var result = await this.employeeService.GetEmployeeAsync(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
