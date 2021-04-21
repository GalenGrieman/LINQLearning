using Cars;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQEntityFramework
{
    //This is a class representation of our Database
    //Properties represent tables on the database
    public class CarDb : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}