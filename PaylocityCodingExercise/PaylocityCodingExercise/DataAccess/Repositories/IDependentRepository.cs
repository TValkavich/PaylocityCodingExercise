using PaylocityCodingExercise.Model.Models;

namespace PaylocityCodingExercise.DataAccess.Repositories
{
    public interface IDependentRepository
    { 
        public Task<List<Dependent>> GetAllDependentsAsync();    
        public Task<List<Dependent>> GetDependentsByEmployeeIdAsync(int dependentId);
        public Task<Dependent> GetDependentByIdAsync(int dependentId);
        public Task<bool> CreateDependentAsync(Dependent dependentToCreate);
        public Task<bool> UpdateDependentAsync(Dependent dependentToUpdate);
        public Task<bool> DeleteDependenetByIdAsync(int dependentId);
    }
}
