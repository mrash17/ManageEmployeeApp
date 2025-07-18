using ManageEmployeeApp.Data.Entities;
using ManageEmployeeApp.Repositories;

namespace ManageEmployeeApp.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task ListEmployeesAsync(string? filter = null)
        {
            var employees = string.IsNullOrWhiteSpace(filter)
                ? await _repo.GetAllAsync()
                : await _repo.SearchByNameOrTitleAsync(filter);

            foreach (var emp in employees)
            {
                var latestSalary = emp.Salaries
                    .OrderByDescending(s => s.FromDate)
                    .FirstOrDefault();

                Console.WriteLine($"{emp.Name} - {latestSalary?.Title} - ${Math.Round(latestSalary?.Salary ?? 0, 2)}");
            }
        }

        public async Task ListTitlesAsync()
        {
            var titles = await _repo.GetTitleStatsAsync();
            foreach (var t in titles)
            {
                Console.WriteLine($"{t.Title} - Min: ${Math.Round(t.Min, 2)}, Max: ${Math.Round(t.Max, 2)}");
            }
        }

        public async Task AddEmployeeAsync()
        {
            var name = ReadNonEmptyString("Name");
            var ssn = ReadNonEmptyString("SSN");

            var phone = ReadNumericString("Phone");
            var dob = ReadDate("DOB");

            var age = DateTime.Today.Year - dob.Year;

            // If the birthday hasn't occurred yet this year, then subtracting 1 from the age
            if (dob.Date > DateTime.Today.AddYears(-age))
                age--;

            if (age < 22 || age > 64)
            {
                Console.WriteLine("Employee must be between 22 and 64 years old.");
                return;
            }

            var address = ReadNonEmptyString("Address");
            var city = ReadNonEmptyString("City");
            var state = ReadNonEmptyString("State");
            var zip = ReadNumericString("Zip Code");

            var title = ReadNonEmptyString("Title");

            double salary;
            while (true)
            {
                Console.Write("Salary: ");
                var input = Console.ReadLine();
                if (double.TryParse(input, out salary) && salary > 0)
                {
                    break;
                }
                Console.WriteLine("Invalid salary.");
            }

            var fromDate = ReadDate("From Date");
            var toDate = ReadDate("To Date");

            var emp = new Employee
            {
                Name = name!,
                SSN = ssn!,
                DOB = dob,
                Address = address!,
                City = city!,
                State = state!,
                Phone = phone,
                JoinDate = DateTime.Today,
                Salaries = new List<EmployeeSalary>
                {
                    new EmployeeSalary
                    {
                        FromDate = fromDate,
                        ToDate = toDate,
                        Title = title!,
                        Salary = salary
                    }
                }
            };

            await _repo.AddAsync(emp);
            await _repo.SaveAsync();

            Console.WriteLine("Employee added successfully.");
        }

        private string ReadNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{prompt} cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        private string ReadNumericString(string prompt)
        {
            string input;
            do
            {
                Console.Write($"{prompt}: ");
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input) || !input.All(char.IsDigit))
                {
                    Console.WriteLine($"{prompt} must be numeric and cannot be empty.");
                    input = null;
                }
            } while (input == null);
            return input;
        }
        private DateTime ReadDate(string prompt)
        {
            DateTime date;
            string? input;
            do
            {
                Console.Write($"{prompt} (dd-MM-yyyy): ");
                input = Console.ReadLine()?.Trim();
                if (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    Console.WriteLine("Invalid date format. Please use dd-MM-yyyy.");
                }
            } while (!DateTime.TryParseExact(input, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out date));
            return date;
        }
    }
}