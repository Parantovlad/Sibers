using SibersDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Repos
{
    public class EmployeeRepo : BaseRepo<Employee>, IRepo<Employee>
    {
        public EmployeeRepo()
        {
            Table = Context.Employees;
        }

        public int Delete(int id, byte[] timeStamp)
        {
            Context.Entry(new Employee() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Task<int> DeleteAsync(int id, byte[] timeStamp)
        {
            Context.Entry(new Employee() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
    }
}
