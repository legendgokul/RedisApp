﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RedisApp.Database.Context;

#nullable disable

namespace RedisApp.Database.Migrations
{
    [DbContext(typeof(TestingContext))]
    [Migration("20240127115613_initialTables")]
    partial class initialTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RedisApp.Database.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderUid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<int>("itemCount")
                        .HasColumnType("integer");

                    b.HasKey("OrderUid");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RedisApp.Database.Entities.Orderitems", b =>
                {
                    b.Property<Guid>("OrderitemUid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ItemName")
                        .HasColumnType("text");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("text");

                    b.Property<Guid?>("OrderUid")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<bool>("isActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("placedOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("OrderitemUid");

                    b.HasIndex("OrderUid");

                    b.ToTable("Ordersitems");
                });

            modelBuilder.Entity("RedisApp.Database.Entities.Orderitems", b =>
                {
                    b.HasOne("RedisApp.Database.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderUid");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RedisApp.Database.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
