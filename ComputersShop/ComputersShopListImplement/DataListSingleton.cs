using ComputerShopListImplement.Models;
using ComputersShopListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShopListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<WareHouse> Warehouses { get; set; }
        public List<Computer> Computers { get; set; }
        public List<Client> Clients { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Clients = new List<Client>();
            Computers = new List<Computer>();
            Warehouses = new List<WareHouse>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }

    }
}
