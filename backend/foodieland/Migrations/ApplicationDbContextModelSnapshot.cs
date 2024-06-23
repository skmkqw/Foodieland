﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using foodieland.Data;

#nullable disable

namespace foodieland.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("foodieland.Models.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("02017150-a4cd-4154-9163-a16c7c61516a"),
                            Name = "Pancakes"
                        },
                        new
                        {
                            Id = new Guid("29155b63-3e95-4f93-b4cc-df9b29a66ac8"),
                            Name = "Egg fried rice"
                        },
                        new
                        {
                            Id = new Guid("8b61efe7-a575-4a6d-b61b-a8f10a0ac94a"),
                            Name = "Shepherd's pie"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
