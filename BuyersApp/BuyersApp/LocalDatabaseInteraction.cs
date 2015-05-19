using System.Collections.Generic;
using System.Linq;

namespace BuyersApp
{
    internal static class LocalDatabaseInteraction
    {
        internal static IEnumerable<Catagories> GetCatagories()
        {
            using (LocalDatabaseEntities database = new LocalDatabaseEntities())
            {
                return database.Catagories.ToList();
            }
        }

        internal static List<Products> GetProductsByCatagoryId(int catagoryId)
        {
            using (LocalDatabaseEntities database = new LocalDatabaseEntities())
            { 
                return database.Products
                    .Where(p => p.Catagories.Id == catagoryId).ToList();
            }
        }
    }

    public partial class Catagories
    {
        public override string ToString()
        {
            return Catagory;
        }
    }

    public partial class Products
    {
        public override string ToString()
        {
            return ProductName;
        }
    }
}
