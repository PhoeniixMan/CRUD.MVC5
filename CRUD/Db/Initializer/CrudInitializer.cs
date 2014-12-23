using System.Collections.Generic;
using System.Data.Entity;
using CRUD.Db.Context;
using CRUD.Models;

namespace CRUD.Db.Initializer
{
    public class CrudInitializer : DropCreateDatabaseIfModelChanges<CrudContext>
    {
        protected override void Seed(CrudContext context)
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    Name = "Tom Hanks",
                    Address = "79/B, Palo Alto, California",
                    Email = "tom@gmail.com",
                    Contact = "0914566546"
                },
                new Employee
                {
                    Name = "Meryl Streep",
                    Address = "21/C North Park Street, South Dacota",
                    Email = "meril@yahoo.com",
                    Contact = "1654531321"
                },
                new Employee
                {
                    Name = "Denzel Washington",
                    Address = "Louvre Avenue, North Carolina",
                    Email = "denzel@hotmail.com",
                    Contact = "46546465"
                },
                new Employee
                {
                    Name = "Johnny Depp",
                    Address = "Brickview Lake, Kentucky",
                    Email = "depp@gmail.com",
                    Contact = "46546465"
                }
            };

            employees.ForEach(x=>context.Employees.Add(x));
            context.SaveChanges();
        }
    }
}