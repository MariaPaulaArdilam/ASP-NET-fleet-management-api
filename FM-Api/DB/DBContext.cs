using Microsoft.EntityFrameworkCore;
using FM_Api.Models;
namespace FM_Api.DB
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    

        public DbSet<Taxi> Taxis { get; set; }

        public DbSet<Trajectorie> Trajectories { get; set; }

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

            modelBuilder.Entity<Trajectorie>(tr =>
            {
                tr.HasKey(col => col.Id);
                tr.Property(col => col.Id).ValueGeneratedOnAdd();
                tr.Property(col => col.Date).HasColumnType("timestamp");
                tr.Property(col => col.Latitude).HasColumnType("double precision");
                tr.Property(col => col.Longitude).HasColumnType("double precision");

                // Configuración de la relación con la tabla Taxi
                tr.HasOne(t => t.Taxi)
                  .WithMany(t => t.Trajectories)
                  .HasForeignKey(tr => tr.TaxiId)
                  .IsRequired(); ;
            });


            modelBuilder.Entity<Trajectorie>().ToTable("Trajectorie");

        }

    }


    }