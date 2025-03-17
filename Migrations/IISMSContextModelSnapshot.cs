﻿// <auto-generated />
using System;
using IISMSBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IISMSBackend.Migrations
{
    [DbContext(typeof(IISMSContext))]
    partial class IISMSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IISMSBackend.Entities.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("productId"));

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("expirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("firstCreationTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("price")
                        .HasColumnType("numeric");

                    b.Property<byte[]>("productBarcode")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("productImage")
                        .HasColumnType("bytea");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("quantity")
                        .HasColumnType("bigint");

                    b.Property<decimal>("size")
                        .HasColumnType("numeric");

                    b.Property<string>("unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("productId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("IISMSBackend.Entities.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("userId"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
