﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoAPI.Context;

#nullable disable

namespace TodoAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TodoAPI.Model.Item", b =>
                {
                    b.Property<int>("itemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("itemId"));

                    b.Property<DateTime>("dataFim")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("dataInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("listaId")
                        .HasColumnType("int");

                    b.Property<bool>("status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("itemId");

                    b.HasIndex("listaId");

                    b.ToTable("Item", (string)null);
                });

            modelBuilder.Entity("TodoAPI.Model.Lista", b =>
                {
                    b.Property<int>("listaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("listaId"));

                    b.Property<string>("titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("listaId");

                    b.ToTable("Listas", (string)null);
                });

            modelBuilder.Entity("TodoAPI.Model.Item", b =>
                {
                    b.HasOne("TodoAPI.Model.Lista", "lista")
                        .WithMany("itens")
                        .HasForeignKey("listaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lista");
                });

            modelBuilder.Entity("TodoAPI.Model.Lista", b =>
                {
                    b.Navigation("itens");
                });
#pragma warning restore 612, 618
        }
    }
}
