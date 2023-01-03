﻿// <auto-generated />
using ApiGeo.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiGeo.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230103190210_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiGeo.Data.Models.AddressHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Country")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Province")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Street")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("latitud")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("longitude")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("status")
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("AddressHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
