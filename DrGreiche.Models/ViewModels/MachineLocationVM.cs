using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.Web.Mvc;

namespace DrGreiche.Models.ViewModels
{
    public class MachineLocationVM
    {
        [Required(ErrorMessage = "Machine ID is required.")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Machine Code is required.")]
        public string MachineCode { get; set; }

        [Required(ErrorMessage = "Machine Name is required.")]
        public string MachineName { get; set; }

        [Display(Name = "MTBF")]
        [Required(ErrorMessage = "MTBF is required.")]
        public string Purpose_MTBF { get; set; }

        [Display(Name = "MTTR")]
        [Required(ErrorMessage = "MTTR is required.")]
        public string Purpose_MTTR { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public int SelectedLocationID { get; set; }

        public List<SelectListItem>? LocationSelectList { get; set; }

        [Required(ErrorMessage = "Working Hours are required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Working Hours must be greater than 0.")]
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
