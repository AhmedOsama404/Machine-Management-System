using DrGreiche.DataAccess.Data;
using DrGreiche.DataAccess.Repository.IRepository;
using DrGreiche.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrGreiche.DataAccess.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private ApplicationDbContext _db;
        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}