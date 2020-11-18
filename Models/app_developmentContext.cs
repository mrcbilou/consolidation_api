using Microsoft.EntityFrameworkCore;
using RestApi.Models;

namespace RestApi.Models
{
    public partial class app_developmentContext : DbContext
    {
        public app_developmentContext(DbContextOptions<app_developmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> users { get; set; }
        public virtual DbSet<Column> columns { get; set; }
        public virtual DbSet<Elevator> elevators { get; set; }
        public virtual DbSet<Building> buildings { get; set; }
        public virtual DbSet<Lead> leads { get; set; }
        public virtual DbSet<Battery> batteries { get; set; }
        public virtual DbSet<Address> addresses { get; set; }
        public virtual DbSet<BuildingDetail> building_details { get; set; }
        public virtual DbSet<Customer> customers { get; set; }
        public virtual DbSet<Employe> employees { get; set; }
    }
}