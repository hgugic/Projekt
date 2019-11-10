using Microsoft.EntityFrameworkCore;
using Projekt.Service.DAL.Entities;
using Projekt.Service.DAL.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt.Service.DAL
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }

        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleMake> VehicleMakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed(); //popunjavanje baze podataka
        }

    }
}
