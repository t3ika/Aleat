using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassSQL1
    {
        public List<main> GetMain()
        {
            using (sql_test1Entities sBase = new sql_test1Entities())
            {
                return sBase.mains.ToList();
            }
        }
    }
}
