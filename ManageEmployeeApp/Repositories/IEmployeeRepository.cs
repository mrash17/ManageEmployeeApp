using ManageEmployeeApp.Data.Entities;

namespace ManageEmployeeApp.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> SearchByNameOrTitleAsync(string filter);
        Task<IEnumerable<(string Title, double Min, double Max)>> GetTitleStatsAsync();
        Task AddAsync(Employee employee);
        Task SaveAsync();
    }
}