using SibersDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SibersDAL.Repos
{
    public class ContractorRepo : BaseRepo<Contractor>, IRepo<Contractor>
    {
        public ContractorRepo()
        {
            Table = Context.Contractors;
        }

        public int Delete(int id, byte[] timeStamp)
        {
            Context.Entry(new Contractor() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChanges();
        }

        public Task<int> DeleteAsync(int id, byte[] timeStamp)
        {
            Context.Entry(new Contractor() { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return SaveChangesAsync();
        }
    }
}
