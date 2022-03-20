using ComputerShopDatabseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopDatabseImplement
{
    public class ComputerShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Database.EnsureCreated();
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ComputerShopDatabase;Trusted_Connection=True");
            }
            base.OnConfiguring(optionsBuilder);
            
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Computer> Computers { set; get; }
        public virtual DbSet<ComputerComponent> ComputerComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<WareHouse> WareHouses { get; set; }
        public virtual DbSet<WareHouseComponent> WareHouseComponents { get; set; }
    }
}
