using System;
using System.Collections.Generic;
using System.Linq;
using yashmetov_yaroslav_variant_1;
class Program
{
    static void Main()
    {
   
        List<Firm> firms = InitializeFirms();

        Console.WriteLine("Хотите ли вы выполнить поиск? (y/n)");
        string input = Console.ReadLine();
        if (input.ToLower() == "y")
        {
            Console.WriteLine("Введите название класса для поиска (Firm, Department, Employee, RegularEmployee, ContractEmployee):");
            string className = Console.ReadLine();

            switch (className.ToLower())
            {
                case "firm":
                    SearchFirms(firms);
                    break;
                case "department":
                    SearchDepartments(firms);
                    break;
                case "employee":
                case "regularemployee":
                case "contractemployee":
                    SearchEmployees(firms, className);
                    break;
                default:
                    Console.WriteLine("Класс не найден.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Поиск отменен.");
        }
    }

    static void SearchFirms(List<Firm> firms)
    {
        Console.WriteLine("Введите название фирмы или часть названия для поиска:");
        string searchTermFirm = Console.ReadLine();
        var foundFirms = firms
            .Where(f => f.Name.IndexOf(searchTermFirm, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        Console.WriteLine(foundFirms.Any() ? "Найденные фирмы:" : "Фирмы не найдены.");
        foreach (var f in foundFirms)
        {
            Console.WriteLine($"Фирма: {f.Name}, Количество отделов: {f.Departments.Count}");
        }
    }

    static void SearchDepartments(List<Firm> firms)
    {
        Console.WriteLine("Введите название отдела или часть названия для поиска:");
        string searchTermDept = Console.ReadLine();
        var foundDepartments = firms
            .SelectMany(f => f.Departments)
            .Where(d => d.Name.IndexOf(searchTermDept, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        Console.WriteLine(foundDepartments.Any() ? "Найденные отделы:" : "Отделы не найдены.");
        foreach (var d in foundDepartments)
        {
            Console.WriteLine($"Отдел: {d.Name}, Количество сотрудников: {d.Employees.Count}");
        }
    }

    static void SearchEmployees(List<Firm> firms, string className)
    {
        Console.WriteLine("Введите ФИО сотрудника или часть ФИО для поиска:");
        string searchTermEmp = Console.ReadLine();
        var employees = firms
            .SelectMany(f => f.Departments)
            .SelectMany(d => d.Employees)
            .Where(e => e.FullName.IndexOf(searchTermEmp, StringComparison.OrdinalIgnoreCase) >= 0)
            .ToList();

        List<Employee> matchedEmployees = new List<Employee>();
        switch (className.ToLower())
        {
            case "regularemployee":
                matchedEmployees.AddRange(employees.OfType<RegularEmployee>());
                break;
            case "contractemployee":
                matchedEmployees.AddRange(employees.OfType<ContractEmployee>());
                break;
            default:
                matchedEmployees.AddRange(employees);
                break;
        }

        Console.WriteLine(matchedEmployees.Any() ? "Найденные сотрудники:" : "Сотрудники не найдены.");
        foreach (var e in matchedEmployees)
        {
            Console.WriteLine($"ФИО: {e.FullName}, Должность: {e.Position}, Зарплата: {e.Salary}");
        }
    }

    static List<Firm> InitializeFirms()
    {
      
        return new List<Firm>
        {
            new Firm
            {
                Name = "Рога и Копыта",
                Departments = new List<Department>
                {
                    new Department
                    {
                        Name = "Разработка",
                        Employees = new List<Employee>
                        {
                            new RegularEmployee { FullName = "Иван Иванов", Position = "Инженер", Salary = 50000, Bonus = 10000 },
                            new ContractEmployee { FullName = "Петр Петров", Position = "Тестировщик", Salary = 30000 }
                        }
                    }
                }
            }
        };
    }
}