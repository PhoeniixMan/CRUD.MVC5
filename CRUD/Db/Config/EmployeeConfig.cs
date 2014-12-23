using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using CRUD.Models;

namespace CRUD.Db.Config
{
    public class EmployeeConfig : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfig()
        {
            ToTable("Employee");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.Address)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.Email)
                .IsRequired();

            Property(x => x.Contact)
                .IsRequired();
        }
    }
}