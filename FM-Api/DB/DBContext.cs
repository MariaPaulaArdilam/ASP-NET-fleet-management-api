using Microsoft.EntityFrameworkCore;
using FM_Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace FM_Api.DB
{
    public class DBContext : IdentityDbContext<Users>
    {
        private object builder;

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    

        public DbSet<Taxi> Taxis { get; set; }

        public DbSet<Trajectory> Trajectories { get; set; }


        //definir las caracteristicas de la tabla 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Taxi>(tx =>
            {
                tx.HasKey(col => col.Id);   //indicando cual de mis columna es la llave primaria 
                tx.Property(col => col.Plate).HasMaxLength(50);
            } );

            modelBuilder.Entity<Taxi>().ToTable("Taxi");

            modelBuilder.Entity<Trajectory>(tr =>
            {
                tr.HasKey(col => col.Id);
                tr.Property(col => col.Id).ValueGeneratedOnAdd();
                tr.Property(col => col.Date).HasColumnType("timestamp");
                tr.Property(col => col.Latitude).HasColumnType("double precision");
                tr.Property(col => col.Longitude).HasColumnType("double precision");

              
            });


            modelBuilder.Entity<Trajectory>().ToTable("Trajectory");


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);



        }

    }


    }