﻿// <auto-generated />
using System;
using InvertedIndexLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvertedIndexLibrary.Migrations
{
    [DbContext(typeof(InvertedIndexContext))]
    [Migration("20210303093014_AddSearchItemAndDocTableToDb")]
    partial class AddSearchItemAndDocTableToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InvertedIndexLibrary.Doc", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SearchItemId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("SearchItemId");

                    b.ToTable("Docs");
                });

            modelBuilder.Entity("InvertedIndexLibrary.SearchItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("SearchItems");
                });

            modelBuilder.Entity("InvertedIndexLibrary.Doc", b =>
                {
                    b.HasOne("InvertedIndexLibrary.SearchItem", null)
                        .WithMany("ContainingDocs")
                        .HasForeignKey("SearchItemId");
                });

            modelBuilder.Entity("InvertedIndexLibrary.SearchItem", b =>
                {
                    b.Navigation("ContainingDocs");
                });
#pragma warning restore 612, 618
        }
    }
}
