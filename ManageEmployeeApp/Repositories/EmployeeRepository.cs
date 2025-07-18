using ManageEmployeeApp.Data.Context;
using ManageEmployeeApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageEmployeeApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Salaries)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchByNameOrTitleAsync(string filter)
        {
            filter = filter.ToLower();
            return await _context.Employees
                .Include(e => e.Salaries)
                .Where(e =>
                    e.Name.ToLower().Contains(filter) ||
                    e.Salaries.Any(s => s.Title.ToLower().Contains(filter)))
                .ToListAsync();
        }

        public async Task<IEnumerable<(string Title, double Min, double Max)>> GetTitleStatsAsync()
        {
            return await _context.EmployeeSalaries
                .GroupBy(s => s.Title)
                .Select(g => new ValueTuple<string, double, double>(
                    g.Key,
                    g.Min(s => s.Salary),
                    g.Max(s => s.Salary)))
                .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
