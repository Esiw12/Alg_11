namespace yashmetov_yaroslav_variant_1
{
    using System.Collections.Generic;

    public class Firm
    {
        public string Name { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
    }

    public class Department
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    public abstract class Employee
    {
        public string FullName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public abstract void CalculateSalary();
    }

    public class RegularEmployee : Employee
    {
        public decimal Bonus { get; set; }

        public override void CalculateSalary()
        {
            
            Salary = 1000 + Bonus;
        }
    }

    public class ContractEmployee : Employee
    {
        public override void CalculateSalary()
        {
           
            Salary = 800; // фиксированная зарплата
        }
    }
}
