namespace Snappy1.Models
{
    public class CarAvailabiltyModel
    {
        public string Name { get; set; }
        public double AmountPerDay { get; set; }
        public double AmountTotal { get; set; }
        public string ImagePath { get; set; }
        public BookingModel BookingModel { get; set; }
    }
}