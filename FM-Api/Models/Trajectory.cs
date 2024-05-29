using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FM_Api.Models
{
    public class Trajectory
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Taxi")]
        public int TaxiId { get; set; }
        public Taxi Taxi { get; set; }
        public DateTime Date { get; set;}
        public double Latitude { get; set;}
        public double Longitude { get; set;}

        


    }
}
