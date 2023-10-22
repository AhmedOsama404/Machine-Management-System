using DrGreiche.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrGreiche.DataAccess.Repository.IRepository
{
    public interface IMachineRepository : IRepository<Machine>
    {
        IEnumerable<Machine> GetAllWithDetails();
        public Machine GetWithDetails(int id);
        public bool IsMachineCodeUnique(string machineCode);
        //public IEnumerable<Machine> GetWithLocation(int id);



    }
}
