using FM_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FM_Api.DTO
{
    public class TrayectoryDTO
    {
        public int Id { get; set; }
      
        public int TaxiId { get; set; }
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


    }
}
