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
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ComputerShopDatabase6lab+;Trusted_Connection=True");
            }
            base.OnConfiguring(optionsBuilder);
            
        }
        public virtual DbSet<MessageInfo>  MessageInfoes { get ; set; }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Computer> Computers { set; get; }
        public virtual DbSet<ComputerComponent> ComputerComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
    }
}
