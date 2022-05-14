using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersShopContracts.Attributes
{
    public enum GridViewAutoSize
    {
        NotSet = 0,
        None = 1,
        ColumnHeader = 2,
        AllCellsExceptHeader = 4,
        AllCells = 6,
        DisplayedCellsExceptHeader = 8,
        DisplayedCells = 10,
        Fill = 16
    }
}
