using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.StoragesContracts
{
    public interface IBackUpInfo
    {
        Assembly GetAssembly();
        List<PropertyInfo> GetFullList();
        List<T> GetList<T>() where T : class, new();

    }
}
