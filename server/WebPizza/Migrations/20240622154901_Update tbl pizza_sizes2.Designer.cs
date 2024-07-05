﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebPizza.Infrastructure.Data;

#nullable disable

namespace WebPizza.Migrations
{
    [DbContext(typeof(PizzaDbContext))]
    [Migration("20240622154901_Update tbl pizza_sizes2")]
    partial class Updatetblpizza_sizes2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebPizza.Data.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("tbl_categories");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.IngredientEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("tbl_ingredients");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("tbl_pizzas");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaIngredientEntity", b =>
                {
                    b.Property<int>("PizzaId")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.HasKey("PizzaId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("tbl_pizza_ingredients");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaPhotoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PizzaId")
                        .HasColumnType("integer");

                    b.Property<int>("Priority")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId");

                    b.ToTable("tbl_pizza_photos");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaSizeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("tbl_sizes");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaSizePriceEntity", b =>
                {
                    b.Property<int>("PizzaId")
                        .HasColumnType("integer");

                    b.Property<int>("SizeId")
                        .HasColumnType("integer");

                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PizzaId", "SizeId");

                    b.HasIndex("SizeId");

                    b.ToTable("tbl_pizza_sizes");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaEntity", b =>
                {
                    b.HasOne("WebPizza.Data.Entities.CategoryEntity", "Category")
                        .WithMany("Pizzas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaIngredientEntity", b =>
                {
                    b.HasOne("WebPizza.Data.Entities.IngredientEntity", "Ingredient")
                        .WithMany("Pizzas")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebPizza.Data.Entities.PizzaEntity", "Pizza")
                        .WithMany("PizzaIngredients")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaPhotoEntity", b =>
                {
                    b.HasOne("WebPizza.Data.Entities.PizzaEntity", "Pizza")
                        .WithMany("Photos")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaSizePriceEntity", b =>
                {
                    b.HasOne("WebPizza.Data.Entities.PizzaEntity", "Pizza")
                        .WithMany("PizzaSizes")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebPizza.Data.Entities.PizzaSizeEntity", "Size")
                        .WithMany("PizzaSizePrices")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Pizzas");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.IngredientEntity", b =>
                {
                    b.Navigation("Pizzas");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaEntity", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("PizzaIngredients");

                    b.Navigation("PizzaSizes");
                });

            modelBuilder.Entity("WebPizza.Data.Entities.PizzaSizeEntity", b =>
                {
                    b.Navigation("PizzaSizePrices");
                });
#pragma warning restore 612, 618
        }
    }
}
