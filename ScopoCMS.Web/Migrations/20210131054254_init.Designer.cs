﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScopoCMS.Web.Models;

namespace ScopoCMS.Web.Migrations
{
    [DbContext(typeof(CMSDbContext))]
    [Migration("20210131054254_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("PostSection", b =>
                {
                    b.Property<int>("PostspostID")
                        .HasColumnType("int");

                    b.Property<int>("SectionssectionId")
                        .HasColumnType("int");

                    b.HasKey("PostspostID", "SectionssectionId");

                    b.HasIndex("SectionssectionId");

                    b.ToTable("PostSection");
                });

            modelBuilder.Entity("ScopoCMS.Web.Models.Category", b =>
                {
                    b.Property<int>("categoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("categoryID");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("ScopoCMS.Web.Models.Post", b =>
                {
                    b.Property<int>("postID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("categoryID")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("publishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("postID");

                    b.HasIndex("categoryID");

                    b.ToTable("posts");
                });

            modelBuilder.Entity("ScopoCMS.Web.Models.Section", b =>
                {
                    b.Property<int>("sectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("sectionId");

                    b.ToTable("sections");
                });

            modelBuilder.Entity("PostSection", b =>
                {
                    b.HasOne("ScopoCMS.Web.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("PostspostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScopoCMS.Web.Models.Section", null)
                        .WithMany()
                        .HasForeignKey("SectionssectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ScopoCMS.Web.Models.Post", b =>
                {
                    b.HasOne("ScopoCMS.Web.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("categoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ScopoCMS.Web.Models.Category", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
