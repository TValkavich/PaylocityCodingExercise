using PaylocityCodingExercise.Model.Models;

namespace PaylocityCodingExercise.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int employeeId);
        public Task<Employee> CreateEmployeeAsync(Employee employeeToCreate);
        public Task<bool> UpdateEmployeeAsync(Employee employeeToUpdate);
        public Task<bool> DeleteEmployeeByIdAsync(int employeeId);

    }
}
