namespace Employees.Domain
{
    public class MonthlySalaryEmployee : Employee
    {
        public MonthlySalaryEmployee(Employee employee)
            : base(employee)
        { }

        public override void CalculateSalary()
        {
            this.Salary = this.MonthlySalary * 12;
        }
    }
}
