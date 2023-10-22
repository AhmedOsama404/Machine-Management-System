using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrGreiche.DataAccess.Repository.IRepository;
using DrGreiche.DataAccess.Data;
using DrGreiche.Models;
using DrGreiche.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace DrGreiche.DataAccess.Repository
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        private ApplicationDbContext _db;
        public MachineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Machine> GetAllWithDetails()
        {
            // Use the Include method to load related entities
            return dbSet.Include(m => m.MachineLocations)
                        .ThenInclude(ml => ml.Location)
                        .ToList();
        }

        public Machine GetWithDetails(int id)
        {
            // Use the Include method to load related entities
            return dbSet.Include(m => m.MachineLocations)
                .ThenInclude(ml => ml.Location)
                .FirstOrDefault(m => m.ID == id);
        }
        public bool IsMachineCodeUnique(string machineCode)
        {
            // Check if any machine already exists with the given MachineCode
            return !dbSet.Any(m => m.MachineCode == machineCode);
        }

        //public IEnumerable<Machine> GetWithLocation(int locationID)
        //{

        //    return 
                                    
        // }

    }
}
