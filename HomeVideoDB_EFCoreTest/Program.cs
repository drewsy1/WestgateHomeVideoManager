using System;
using System.Diagnostics;
using System.Linq;
using HomeVideoDB_EFCoreTest.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Query;

namespace HomeVideoDB_EFCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new HomeVideoDBContext())
            {
                var query = from b in db.Source
                    orderby b.SourceId
                    select b;

                Debug.WriteLine("All Sources in HomeVideoDB");

                foreach (var item in query)
                {
                    Debug.WriteLine(item.SourceLabel);
                    foreach (var Clip in item.Clips)
                    {
                        Debug.WriteLine("\t" + Clip.ClipId);
                    }
                }

            }
        }
    }
}
