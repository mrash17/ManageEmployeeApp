using ManageEmployeeApp.Data;
using ManageEmployeeApp.Data.Context;
using ManageEmployeeApp.Repositories;
using ManageEmployeeApp.Services;
using ManageEmployeeApp.Utils;

// Set up DB
using var db = new AppDbContext();
db.Database.EnsureCreated();
SeedData.Initialize(db);

// Setup Repository & Service
var repo = new EmployeeRepository(db);
var service = new EmployeeService(repo);

Console.WriteLine("Employee Management CLI. Press Ctrl + C to forcely exit the terminal");
Console.WriteLine("Available commands: list [filter], titles, add, exit");

while (true)
{
        Console.Write("\n> ");
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) continue;

        args = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var command = args[0].ToLower();
    await ExceptionMiddleware.RunAsync(async () =>
    {
        switch (command)
        {
            case "list":
                await service.ListEmployeesAsync(args.Length > 1 ? args[1] : null);
                break;
            case "titles":
                await service.ListTitlesAsync();
                break;
            case "add":
                await service.AddEmployeeAsync();
                break;
            case "exit":
                return;
            default:
                Console.WriteLine("Unknown command. Try: list, titles, add, exit");
                break;
        }
    });
}