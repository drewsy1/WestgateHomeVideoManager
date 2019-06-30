using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeVideoDB_EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new HomeVideoDBEntities())
            {
                var query = from b in db.Sources
                    orderby b.SourceID
                    select b;

                Debug.WriteLine("All Sources in HomeVideoDB");

                foreach (var item in query)
                {
                    Debug.WriteLine(item.SourceLabel);
                    foreach (var Clip in item.Clips)
                    {
                        Debug.WriteLine("\t"+Clip.ClipID);
                    }
                }

            }
        }
    }
}
