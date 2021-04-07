namespace Employees.Domain
{
    using Employees.Domain.Interfaces;
    using System;

    public class Employee : IEntity, IEmployee
    {
        public Employee() { }

        protected Employee(Employee employee)
        {
            this.Id = employee.Id;
            this.Name = employee.Name;
            this.ContractTypeName = employee.ContractTypeName;
            this.RoleId = employee.RoleId;
            this.RoleName = employee.RoleName;
            this.RoleDescription = employee.RoleDescription;
            this.HourlySalary = employee.HourlySalary;
            this.MonthlySalary = employee.MonthlySalary;
            this.Salary = employee.Salary;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ContractTypeName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public decimal HourlySalary { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal Salary { get; protected set; }

        public virtual void CalculateSalary()
        {
            throw new NotImplementedException();
        }
    }
}
