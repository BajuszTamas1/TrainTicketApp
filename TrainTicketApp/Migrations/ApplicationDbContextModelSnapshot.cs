﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TrainTicketApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TrainTicketApp.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TicketType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TrainId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserPhone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TrainTicketApp.Models.Train", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArrivalLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureLocation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Distance")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("TravelTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("TrainTicketApp.Models.TrainDepartureTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("DepartureTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TrainId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TrainId");

                    b.ToTable("TrainDepartureTime");
                });

            modelBuilder.Entity("TrainTicketApp.Models.Order", b =>
                {
                    b.HasOne("TrainTicketApp.Models.Train", "Train")
                        .WithMany()
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Train");
                });

            modelBuilder.Entity("TrainTicketApp.Models.TrainDepartureTime", b =>
                {
                    b.HasOne("TrainTicketApp.Models.Train", null)
                        .WithMany("DepartureTimes")
                        .HasForeignKey("TrainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrainTicketApp.Models.Train", b =>
                {
                    b.Navigation("DepartureTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
