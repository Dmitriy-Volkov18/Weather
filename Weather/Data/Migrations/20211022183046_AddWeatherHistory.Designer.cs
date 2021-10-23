﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Weather.Data;

namespace Weather.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211022183046_AddWeatherHistory")]
    partial class AddWeatherHistory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Weather.Entities.WeatherEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AverageT")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentT")
                        .HasColumnType("int");

                    b.Property<int>("MaxT")
                        .HasColumnType("int");

                    b.Property<int>("MinT")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });

            modelBuilder.Entity("Weather.Entities.WeatherHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("int");

                    b.Property<int>("WeatherEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeatherEntityId");

                    b.ToTable("WeatherHistories");
                });

            modelBuilder.Entity("Weather.Entities.WeatherHistory", b =>
                {
                    b.HasOne("Weather.Entities.WeatherEntity", "WeatherEntity")
                        .WithMany("WeatherHistory")
                        .HasForeignKey("WeatherEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherEntity");
                });

            modelBuilder.Entity("Weather.Entities.WeatherEntity", b =>
                {
                    b.Navigation("WeatherHistory");
                });
#pragma warning restore 612, 618
        }
    }
}