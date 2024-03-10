using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using yashmetov_yaroslav_variant_1;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        List<Firm> firms = InitializeFirms();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string className = ((ComboBoxItem)cmbSearchClass.SelectedItem)?.Content.ToString();
            string searchTerm = txtSearchTerm.Text;

            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Пожалуйста, выберите класс для поиска и введите критерий поиска.");
                return;
            }

            switch (className.ToLower())
            {
                case "firm":
                    SearchFirms();
                    break;
                case "department":
                    SearchDepartments();
                    break;
                case "employee":
                case "regularemployee":
                case "contractemployee":
                    SearchEmployees(className);
                    break;
                default:
                    MessageBox.Show("Класс не найден.");
                    break;
            }
        }

        private void SearchFirms()
        {
            string searchTermFirm = txtSearchTerm.Text;
            var foundFirms = firms
                .Where(f => f.Name.IndexOf(searchTermFirm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            dataGrid.ItemsSource = foundFirms;
        }

        private void SearchDepartments()
        {
            string searchTermDept = txtSearchTerm.Text;
            var foundDepartments = firms
                .SelectMany(f => f.Departments)
                .Where(d => d.Name.IndexOf(searchTermDept, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            dataGrid.ItemsSource = foundDepartments;
        }

        private void SearchEmployees(string className)
        {
            string searchTermEmp = txtSearchTerm.Text;
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

            dataGrid.ItemsSource = matchedEmployees;
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
}
