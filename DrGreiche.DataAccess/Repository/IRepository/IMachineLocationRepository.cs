﻿using DrGreiche.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrGreiche.DataAccess.Repository.IRepository
{
    public interface IMachineLocationRepository  : IRepository<MachineLocation>
    {
        IEnumerable<MachineLocation> GetWithInclude();
        //public IEnumerable<Machine> GetMachinesWithLocation(int id);

    }
}
