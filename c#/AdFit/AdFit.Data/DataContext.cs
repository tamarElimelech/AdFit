using AdFit.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdminAdvertisement> AdminAdvertisements { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Newspaper> Newspapers { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    }
}
