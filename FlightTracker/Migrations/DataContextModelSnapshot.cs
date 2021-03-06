﻿// <auto-generated />
using FlightTracker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightTracker.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("FlightTracker.Metier.Entities.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Airport");
                });

            modelBuilder.Entity("FlightTracker.Metier.Entities.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Destination");

                    b.Property<int>("Origin");

                    b.Property<int>("Plane");

                    b.HasKey("Id");

                    b.ToTable("Flight");
                });

            modelBuilder.Entity("FlightTracker.Metier.Entities.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FuelConsumption");

                    b.Property<string>("Name");

                    b.Property<int>("TakeoffEffort");

                    b.Property<int>("TakeoffTime");

                    b.HasKey("Id");

                    b.ToTable("Plane");
                });
#pragma warning restore 612, 618
        }
    }
}
