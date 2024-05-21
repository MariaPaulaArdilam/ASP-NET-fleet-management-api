namespace FM_Api.DTO
{
    public class TaxiDTO
    {
        public int TaxiId { get; set; }
        public string Plate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
    }
}
