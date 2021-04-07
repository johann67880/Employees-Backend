namespace Employees.Tests
{
    using Employees.Domain;
    using Employees.Domain.Interfaces;
    using Employees.Services.Interfaces;
    using Employees.Services.Services;
    using FluentAssertions;
    using Moq;
    using System.Collections.Generic;
    using Xunit;

    public class EmployeeTest
    {
        [Fact]
        public void GetAllEmployeesOK()
        {
            //Arrange
            var employees = this.CreateEmployeeList();
            var userRepository = new Mock<IEmployeeRepository>();
            userRepository.Setup(x => x.GetAllEmployeesAsync()).ReturnsAsync(employees);
            IEmployeeService employeeService = new EmployeeService(userRepository.Object);

            //Act
            List<Employee> result = employeeService.GetAllEmployeesAsync().Result;

            //Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(employees.Count);
        }

        [Fact]
        public void GetEmployeeByIdOK()
        {
            //Arrange
            int id = 1;
            var employee = this.CreateEmployee();
            var userRepository = new Mock<IEmployeeRepository>();
            var expectedSalary = 120 * employee.HourlySalary * 12;

            userRepository.Setup(x => x.GetEmployeeAsync(id)).ReturnsAsync(employee);
            IEmployeeService employeeService = new EmployeeService(userRepository.Object);

            //Act
            Employee result = employeeService.GetEmployeeAsync(id).Result;
            result.CalculateSalary();

            //Assert
            result.Should().NotBeNull();
            result.Salary.Should().Be(expectedSalary);
        }

        private Employee CreateEmployee() =>
            new Employee()
            {
                ContractTypeName = "HourlySalaryEmployee",
                HourlySalary = 60000,
                MonthlySalary = 80000,
                Id = 1,
                Name = "Johann",
                RoleDescription = "Role Test 1",
                RoleId = 1,
                RoleName = "Administrator"
            };

        private List<Employee> CreateEmployeeList()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    ContractTypeName = "HourlySalaryEmployee",
                    HourlySalary = 60000,
                    MonthlySalary = 80000,
                    Id = 1,
                    Name = "Johann",
                    RoleDescription = "Role Test 1",
                    RoleId = 1,
                    RoleName = "Administrator"
                },
                new Employee()
                {
                    ContractTypeName = "MonthlySalaryEmployee",
                    HourlySalary = 60000,
                    MonthlySalary = 80000,
                    Id = 2,
                    Name = "Beto",
                    RoleDescription = "Role Test 2",
                    RoleId = 2,
                    RoleName = "Contractor"
                }
            };
        }
    }
}
