using Microsoft.EntityFrameworkCore;
using PaylocityCodingExercise.Model.DBContexts;
using PaylocityCodingExercise.Model.Models;

namespace PaylocityCodingExercise.DataAccess.Repositories
{
    public class DependentRepository : IDependentRepository
    {
        public async Task<List<Dependent>> GetAllDependentsAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Dependents.ToListAsync();
            }
        }

        public async Task<List<Dependent>> GetDependentsByEmployeeIdAsync(int employeeId)
        {
            using (var db = new AppDBContext())
            {
                List<Dependent> result = new List<Dependent>();
                List<Dependent> allDependents = await db.Dependents.ToListAsync();

                foreach(var dep in allDependents)
                {
                    if(dep.EmployeeId == employeeId)
                    {
                        result.Add(dep);
                    }
                }

                return result;
            }
        }
        
        public async Task<Dependent> GetDependentByIdAsync(int dependentId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Dependents
                    .FirstOrDefaultAsync(dep => dep.Id == dependentId);
            }
        }

        public async Task<bool> CreateDependentAsync(Dependent dependentToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Dependents.AddAsync(dependentToCreate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    //Log Error - Console log for time
                    Console.WriteLine("CreateDependentAsync Failed");
                    return false;
                }
            }
        }

        public async Task<bool> UpdateDependentAsync(Dependent dependentToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Dependents.Update(dependentToUpdate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    //Log Error - Console log for time
                    Console.WriteLine("UpdateDependentAsync Failed");
                    return false;
                }
            }
        }

        public async Task<bool> DeleteDependenetByIdAsync(int dependentId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Dependent dependentToDelete = await GetDependentByIdAsync(dependentId);

                    db.Remove(dependentToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    //Log Error - Console log for time
                    Console.WriteLine("DeleteDependenetByIdAsync Failed");
                    return false;
                }
            }
        }

    }
}
