using SibersDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Repos
{
    public class ProjectEmployeesRepo : BaseRepo<ProjectEmployees>, IRepo<ProjectEmployees>
    {
        public ProjectEmployeesRepo()
        {
            Table = Context.ProjectEmployees;
        }

        public new ProjectEmployees GetOne(int? id) => Table.FirstOrDefault(x => x.Id == id);
        public new Task<ProjectEmployees> GetOneAsync(int? id) => Table.FirstOrDefaultAsync(x => x.Id == id);

        public new int Delete(ProjectEmployees entity)
        {
            var projectEmployee = Table.FirstOrDefault(x => x.Id == entity.Id && x.Timestamp == entity.Timestamp);
            Context.Entry(projectEmployee).State = EntityState.Deleted;
            return SaveChanges();
        }
        public new Task<int> DeleteAsync(ProjectEmployees entity)
        {
            var projectEmployee = Table.FirstOrDefaultAsync(x => x.Id == entity.Id && x.Timestamp == entity.Timestamp).Result;
            Context.Entry(projectEmployee).State = EntityState.Deleted;
            return SaveChangesAsync();
        }

        public int Delete(int id, byte[] timeStamp)
        {
            var projectEmployee = Table.FirstOrDefault(x => x.Id == id && x.Timestamp == timeStamp);
            Context.Entry(projectEmployee).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Task<int> DeleteAsync(int id, byte[] timeStamp)
        {
            var projectEmployee = Table.FirstOrDefaultAsync(x => x.Id == id && x.Timestamp == timeStamp).Result;
            Context.Entry(projectEmployee).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
    }
}
