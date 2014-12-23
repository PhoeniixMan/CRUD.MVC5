using System.Data.Entity;
using CRUD.Db.Config;
using CRUD.Db.Initializer;
using CRUD.Models;

namespace CRUD.Db.Context
{
    public class CrudContext : DbContext
    {
        static CrudContext()
        {
            Database.SetInitializer(new CrudInitializer());
        }

        public CrudContext() : base("CrudConnection")
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfig());
        }
    }
}