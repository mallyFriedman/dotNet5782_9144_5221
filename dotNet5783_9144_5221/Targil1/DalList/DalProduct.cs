using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dal.DO;
namespace Dal.DO;

    public class DalProduct 
    {
        public int Create(IDataObject obj)
        {
            Product p = new Product() { };
            p.ProductName = ((Product) obj).ProductName;
        reutrn
        }

        public void Delete(IDataObject obj)
        {

        }

        public IDataObject[] Read()
        {
           
        }

        public void Update(IDataObject obj)
        {

        }
    }
}
    }
