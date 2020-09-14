using System;
using System.Data.Entity;

namespace AsadAli.Models
{
    public class CarsModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
    }

    public class CarsDBContext : DbContext
    {
        public DbSet<CarsModel> cars { get; set; }
    }
}