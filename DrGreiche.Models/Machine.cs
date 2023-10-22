using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrGreiche.Models
{
    public class Machine
    {
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

        public ICollection<MachineLocation> MachineLocations { get; set; }


    }
}
