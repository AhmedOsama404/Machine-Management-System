using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrGreiche.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IMachineRepository Machine { get; }
        ILocationRepository Location { get; }
        IMachineLocationRepository MachineLocation { get; }
        void Save();
    }
}
