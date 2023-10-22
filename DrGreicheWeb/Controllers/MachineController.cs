using DrGreiche.DataAccess.Data;
using DrGreiche.Models;
using Microsoft.AspNetCore.Mvc;
using DrGreiche.DataAccess.Repository;
using DrGreiche.DataAccess.Repository.IRepository;
using DrGreiche.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Reflection.PortableExecutable;
using Machine = DrGreiche.Models.Machine;

namespace DrGreicheWeb.Controllers
{
    public class MachineController : Controller
    {
     
        private readonly IUnitOfWork _unitOfWork;
        public MachineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Retrieve a list of machines with their details(using include)
            var machines = _unitOfWork.Machine.GetAllWithDetails();
            return View(machines);

        }

        public IActionResult Create()
        {
            // Retrieve a list of locations for creating a machine
            var locations = _unitOfWork.Location.GetAll();

            // Prepare a dropdown list of locations for the view
            ViewBag.LocationSelectList = new SelectList(locations, "ID", "LocationName");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MachineLocationVM machineViewModel)
        {
            if(!(machineViewModel.NextMaintenanceDate > machineViewModel.LastMaintenanceDate
                && machineViewModel.NextMaintenanceDate > DateTime.Now ))
            {
                ModelState.AddModelError("NextMaintenanceDate", "Invalid Date");
            }
            if (!(machineViewModel.LastMaintenanceDate <= DateTime.Now))
            {
                ModelState.AddModelError("LastMaintenanceDate", "Invalid Date");
            }
            if (_unitOfWork.Machine.GetAll().Any(m => m.MachineCode== machineViewModel.MachineCode))
            {
                // Machine code is not unique; show an error message
                ModelState.AddModelError("MachineCode", "Machine Code already exists.");
            }
            if (ModelState.IsValid)
            {
                // Check if the machine code is unique
               
                    // Create a new Machine entity
                    var machine = new Machine
                    {
                        MachineCode = machineViewModel.MachineCode,
                        MachineName = machineViewModel.MachineName,
                        Purpose_MTBF = machineViewModel.Purpose_MTBF,
                        Purpose_MTTR = machineViewModel.Purpose_MTTR
                    };

                    // Create a new MachineLocation entity
                    var machineLocation = new MachineLocation
                    {
                        Machine = machine,
                        LocationID = machineViewModel.SelectedLocationID,
                        WorkingHours = machineViewModel.WorkingHours,
                        LastMaintenanceDate = machineViewModel.LastMaintenanceDate.Date,
                        NextMaintenanceDate = machineViewModel.NextMaintenanceDate.Date
                    };

                    // Add the MachineLocation to the Machine entity
                    machine.MachineLocations = new List<MachineLocation> { machineLocation };

                    // Add the entities to the database
                    _unitOfWork.Machine.Add(machine);
                    _unitOfWork.Save();

                    // Notification for success
                    TempData["Success"] = machine.MachineName + " is Created Successfully";

                    return RedirectToAction("Index");
                }
              
            
            // Populate the location dropdown list
            var locations = _unitOfWork.Location.GetAll();
            ViewBag.LocationSelectList = locations.Select(l => new SelectListItem
            {
                Value = l.ID.ToString(),
                Text = l.LocationName
            }).ToList();
            return View(machineViewModel);
            

        }
         
        public IActionResult Edit(int id)
        {

            // Retrieve the machine details by ID
            var machine = _unitOfWork.Machine.GetWithDetails(id);

            if (machine == null)
            {
                return NotFound(); // Machine not found
            }

            // Get the  MachineLocation data associated with the machine
            var machineLocation = machine.MachineLocations.FirstOrDefault();

            // Create a view model with machine and location details
            var machineViewModel = new MachineLocationVM
            {
                ID = machine.ID,
                MachineCode = machine.MachineCode,
                MachineName = machine.MachineName,
                Purpose_MTBF = machine.Purpose_MTBF,
                Purpose_MTTR = machine.Purpose_MTTR,
                SelectedLocationID = machineLocation?.LocationID ?? 0,
                WorkingHours = machineLocation?.WorkingHours ?? 0.0,
                LastMaintenanceDate = machineLocation?.LastMaintenanceDate.Date ?? DateTime.Now,
                NextMaintenanceDate = machineLocation?.NextMaintenanceDate.Date ?? DateTime.Now
            };

            // Get a list of locations for the location dropdown list
            var locations = _unitOfWork.Location.GetAll();
            ViewBag.LocationSelectList = new SelectList(locations, "ID", "LocationName");

            return View(machineViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MachineLocationVM machineViewModel)
        {
            var machine = _unitOfWork.Machine.GetWithDetails(machineViewModel.ID);

            if (machine == null)
            {
                return NotFound(); // Machine not found
            }


            if (!(machineViewModel.NextMaintenanceDate > machineViewModel.LastMaintenanceDate
              && machineViewModel.NextMaintenanceDate > DateTime.Now))
            {
                ModelState.AddModelError("NextMaintenanceDate", "Invalid Date");
            }
            if (!(machineViewModel.LastMaintenanceDate <= DateTime.Now))
            {
                ModelState.AddModelError("LastMaintenanceDate", "Invalid Date");
            }
          
            if (ModelState.IsValid)
            {
              

                // Update the machine entity with the edited details
                machine.MachineCode = machineViewModel.MachineCode;
                machine.MachineName = machineViewModel.MachineName;
                machine.Purpose_MTBF = machineViewModel.Purpose_MTBF;
                machine.Purpose_MTTR = machineViewModel.Purpose_MTTR;


                // Update the location in the MachineLocation entity
                var machineLocation = machine.MachineLocations.FirstOrDefault();

                if (machineLocation != null)
                {
                    machineLocation.LocationID = machineViewModel.SelectedLocationID;
                    machineLocation.WorkingHours = machineViewModel.WorkingHours;
                    machineLocation.LastMaintenanceDate = machineViewModel.LastMaintenanceDate.Date;
                    machineLocation.NextMaintenanceDate = machineViewModel.NextMaintenanceDate.Date;
                }

                // update the machine entity
                _unitOfWork.Machine.Update(machine);

                // Save the changes to the database
                _unitOfWork.Save();

                // Notification for success
                TempData["Info"] = machine.MachineName + " is Updated Successfully";

                return RedirectToAction("Index");
            }

            // Get a list of locations for the location dropdown list
            var locations = _unitOfWork.Location.GetAll();
            ViewBag.LocationSelectList = new SelectList(locations, "ID", "LocationName");

            return View(machineViewModel);
        }

        public IActionResult Delete(int id)
        {
            var machine = _unitOfWork.Machine.GetWithDetails(id);

            if (machine == null)
            {
                return NotFound(); // Machine not found
            }

            return View(machine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {   
            // Retrieve the machine details by ID
            var machine = _unitOfWork.Machine.GetWithDetails(id);

            if (machine == null)
            {
                return NotFound(); // Machine not found
            }

            // Delete related MachineLocation records
            foreach (var machineLocation in machine.MachineLocations)
            {
                _unitOfWork.MachineLocation.Delete(machineLocation);
            }

            // Delete the machine itself
            _unitOfWork.Machine.Delete(machine);
            _unitOfWork.Save();

            // Notification for success
            TempData["Success"] = machine.MachineName + " is Deleted Successfully";

            return RedirectToAction("Index");
        }
        public IActionResult UpcomingMaintenanceReport()
        {
            // Calculate the date range (3 days from now)
            var startDate = DateTime.Now;
            var endDate = startDate.AddDays(3);

            // Retrieve machines with maintenance within the date range
            var machines = _unitOfWork.Machine
                .GetAllWithDetails() // Include related data
                .Where(machine => machine.MachineLocations.Any(ml =>
                    ml.NextMaintenanceDate >= startDate && ml.NextMaintenanceDate <= endDate))
                .ToList();

            return View(machines);
        }


    }
}
