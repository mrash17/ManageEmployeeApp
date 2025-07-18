using Bogus;
using ManageEmployeeApp.Data.Context;
using ManageEmployeeApp.Data.Entities;

namespace ManageEmployeeApp.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext db)
        {
            if (db.Employees.Any()) return;

            var faker = new Faker("en");
            var jobTitles = new[] { "Developer", "Manager", "Analyst", "Tester", "Consultant" };

            var employees = new List<Employee>();

            for (int i = 0; i < 120; i++)
            {
                var dob = faker.Date.Past(42, DateTime.Today.AddYears(-22)); // Age 22–64
                var joinDate = faker.Date.Between(dob.AddYears(22), DateTime.Today.AddYears(-1));
                var name = faker.Name.FullName();

                var emp = new Employee
                {
                    Name = name,
                    SSN = faker.Random.Replace("###-##-####"),
                    DOB = dob,
                    Address = faker.Address.StreetAddress(),
                    City = faker.Address.City(),
                    State = faker.Address.StateAbbr(),
                    Zip = faker.Address.ZipCode(),
                    Phone = faker.Phone.PhoneNumber(),
                    JoinDate = joinDate
                };

                var title = faker.PickRandom(jobTitles);
                emp.Salaries.Add(new EmployeeSalary
                {
                    FromDate = joinDate,
                    Title = title,
                    Salary = faker.Random.Double(50000, 150000)
                });

                employees.Add(emp);
            }

            db.Employees.AddRange(employees);
            db.SaveChanges();
        }
    }
}