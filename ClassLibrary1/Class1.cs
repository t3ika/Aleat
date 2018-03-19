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

        public List<main> SetUser(string value1, int value2)
        {
            using (var context = new sql_test1Entities())
            {
                var user = new main
                {
                    na = value1,
                    wi = value2
                };

                return context.mains.Add(main);

            }
        }
    }
}
