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
    public class Location
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Location Name is required.")]
        public string LocationName { get; set; }

        [Display(Name = "Parent Location")]

        [ForeignKey("ParentLocation")]
        public int? ParentLocationID { get; set; }

       
        public virtual Location? ParentLocation { get; set; }
         
        public virtual ICollection<Location>? SubLocations { get; set; }

        public ICollection<MachineLocation> MachineLocations { get; set; } = new List<MachineLocation>();

    }
}
