using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace EnvironmentCrime.Models
{
    public class Errand
    {
        public int ErrandID { get; set; }
        public string RefNumber { get; set; }

        [Required(ErrorMessage = "Du måste fylla in en plats!")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Du måste fylla in ett brott!")]
        public string TypeOfCrime { get; set; }
        [Required(ErrorMessage = "Du måste fylla in en plats!"), DataType(DataType.Date),
        DisplayFormat(DataFormatString = "{ 0:yyyy - MM - dd}")]
        public DateTime DateOfObservation { get; set; }

        public string Observation { get; set; }
        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }
        [Required(ErrorMessage = "Du måste fylla in ett namn!")]
        public string InformerName { get; set; }
        [Required(ErrorMessage = "Du måste fylla in ett telefonnummer!"), RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$",
ErrorMessage = "Formatet är riktnummer-telefonnummer")]
        public string InformerPhone { get; set; }
        public string StatusId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }

        public ICollection<Sample> Samples { get; set; }
        public ICollection<Picture> Pictures { get; set; }


    }
}
