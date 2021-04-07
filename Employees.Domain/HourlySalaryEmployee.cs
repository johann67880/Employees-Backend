namespace Employees.Domain
{
    public class HourlySalaryEmployee : Employee
    {
        public HourlySalaryEmployee(Employee employee)
            :base(employee)
        { }

        public override void CalculateSalary()
        {
            this.Salary = 120 * this.HourlySalary * 12;
        }
    }
}
