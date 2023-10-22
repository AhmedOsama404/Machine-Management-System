using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DrGreiche.Models
{
    public class MachineLocation
    {
        public int MachineLocationID { get; set; }

        [Required(ErrorMessage = "Machine is required.")]

        [ForeignKey("Machine")]
        public int MachineID { get; set; }
        public  Machine Machine { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [ForeignKey("Location")]
        public int LocationID { get; set; }
        public  Location Location { get; set; }

        [Display(Name = "Working Hours")]
        [Required(ErrorMessage = "Working Hours are required.")]
        public double WorkingHours { get; set; }

        [Display(Name = "Last Maintenance Date")]
        [Required(ErrorMessage = "Last Maintenance Date is required.")]
        [DataType(DataType.Date)] // Specify that this is a date field
        public DateTime LastMaintenanceDate { get; set; }

        [Display(Name = "Next Maintenance Date")]
        [Required(ErrorMessage = "Next Maintenance Date is required.")]
        [DataType(DataType.Date)] // Specify that this is a date field
        public DateTime NextMaintenanceDate { get; set; }
    }
}
