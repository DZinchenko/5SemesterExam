using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_4
{
    class Program
    {
        static void Main(string[] args)
        {

        }
    }

    public abstract class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
    }

    public class Doctor : Employee
    {
        public string Specialization { get; set; }
    }

    public class Engineer : Employee
    {
        public string FavoriteVideogame { get; set; }
    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }

    public class Context : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().Property(d => d.Specialization).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(128);

            modelBuilder.Entity<Employee>().Property(c => c.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Engineer>().Map((e) => {
                e.MapInheritedProperties();
                e.ToTable("Engineers");
            });
            modelBuilder.Entity<Doctor>().Map((e) => {
                e.MapInheritedProperties();
                e.ToTable("Doctors");
            });
        }
    }

    public class EngineerRepository
    {
        public List<Engineer> GetAllEngineersInCityWhichLikeCallOfDuty(int cityId)
        {
            using (var ctx = new Context())
            {
                return ctx.Employees.Where(e => e is Engineer && e.CityId == cityId)
                        .Select(e => e as Engineer)
                        .Where(e => e.FavoriteVideogame.Contains("Call Of Duty"))
                        .ToList();
            }
        }
    }


}
