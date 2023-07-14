using System;
using Microsoft.EntityFrameworkCore;
using PaylocityCodingExercise.Model.DBContexts;
using PaylocityCodingExercise.Model.Models;

namespace PaylocityCodingExercise.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Employees.ToListAsync();
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Employees
                    .FirstOrDefaultAsync(emp => emp.Id == employeeId);
            }
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employeeToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Employees.AddAsync(employeeToCreate);

                    if(await db.SaveChangesAsync() >= 1)
                    {
                        return employeeToCreate;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Employees.Update(employeeToUpdate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteEmployeeByIdAsync(int employeeId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Employee employeeToDelete = await GetEmployeeByIdAsync(employeeId);

                    db.Remove(employeeToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

    }
}
