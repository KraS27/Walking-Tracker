using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WT_Models.Entities;

namespace WT_DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrackLocation> TrackLocations { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TrackLocation>(entity =>
            {
                entity.HasKey(t => t.Id).HasName("Primary");

                entity.ToTable("TrackLocation");

                entity.Property(t => t.Id).HasColumnName("id");
                entity.Property(t => t.IMEI).HasColumnName("IMEI");
                entity.Property(t => t.Longitude).HasColumnName("longitude");
                entity.Property(t => t.Latitude).HasColumnName("latitude");
                entity.Property(t => t.DateTrack).HasColumnName("date_track");
            });
        }

    }
}
