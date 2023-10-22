using DrGreiche.DataAccess.Repository.IRepository;
using DrGreiche.DataAccess.Data;
using DrGreiche.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrGreiche.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DrGreiche.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IMachineRepository Machine { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IMachineLocationRepository MachineLocation { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Machine = new MachineRepository(_db);
            Location = new LocationRepository (_db);
            MachineLocation = new MachineLocationRepositorey(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
