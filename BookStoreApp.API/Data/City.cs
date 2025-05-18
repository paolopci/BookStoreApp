using System.ComponentModel.DataAnnotations;


namespace BookStoreApp.API.Data
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string CityName { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

    }
}
