using DrGreiche.DataAccess.Data;
using DrGreiche.DataAccess.Repository.IRepository;
using DrGreiche.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrGreiche.DataAccess.Repository
{
    public class MachineLocationRepositorey : Repository<MachineLocation>, IMachineLocationRepository
    {
        private ApplicationDbContext _db;

        public MachineLocationRepositorey(ApplicationDbContext db) : base(db)
        {
            _db = db;

        }
      
 
       public IEnumerable<MachineLocation> GetWithInclude()
        {
            return _db.machineLocation.Include(ml => ml.Machine)
                                      .Include(ml => ml.Location)
                                      .ToList(); 
        }

        //IEnumerable<MachineLocation> GetMachinesWithLocation(int id)
        //{
        //    return _db.machineLocation.Include(ml => ml.Machine).Where(ml => ml.LocationID == id).ToList();

        //}
    }
  
}
