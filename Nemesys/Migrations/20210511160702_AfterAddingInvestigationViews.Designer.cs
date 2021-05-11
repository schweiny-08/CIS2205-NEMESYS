﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nemesys.DAL;

namespace Nemesys.Migrations
{
    [DbContext(typeof(NemesysContext))]
    [Migration("20210511160702_AfterAddingInvestigationViews")]
    partial class AfterAddingInvestigationViews
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nemesys.Models.FormModels.Investigation", b =>
                {
                    b.Property<int>("idNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("investigatorId")
                        .HasColumnType("int");

                    b.Property<int>("reportId")
                        .HasColumnType("int");

                    b.HasKey("idNum");

                    b.HasIndex("investigatorId");

                    b.HasIndex("reportId")
                        .IsUnique();

                    b.ToTable("Investigation");
                });

            modelBuilder.Entity("Nemesys.Models.FormModels.Report", b =>
                {
                    b.Property<int>("idNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("reporteridNum")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("upvotes")
                        .HasColumnType("int");

                    b.HasKey("idNum");

                    b.HasIndex("reporteridNum");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("Nemesys.Models.UserModels.Reporter", b =>
                {
                    b.Property<int>("idNum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idNum");

                    b.ToTable("Reporter");
                });

            modelBuilder.Entity("Nemesys.ViewModels.Reports.CreateEditReportViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("imageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CreateEditReportViewModel");
                });

            modelBuilder.Entity("Nemesys.ViewModels.Reports.ReportViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("upvotes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ReportViewModel");
                });

            modelBuilder.Entity("Nemesys.Models.UserModels.Investigator", b =>
                {
                    b.HasBaseType("Nemesys.Models.UserModels.Reporter");

                    b.Property<int>("deptNum")
                        .HasColumnType("int");

                    b.ToTable("Investigator");
                });

            modelBuilder.Entity("Nemesys.Models.FormModels.Investigation", b =>
                {
                    b.HasOne("Nemesys.Models.UserModels.Investigator", "investigator")
                        .WithMany("investigations")
                        .HasForeignKey("investigatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nemesys.Models.FormModels.Report", "report")
                        .WithOne("investigation")
                        .HasForeignKey("Nemesys.Models.FormModels.Investigation", "reportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("investigator");

                    b.Navigation("report");
                });

            modelBuilder.Entity("Nemesys.Models.FormModels.Report", b =>
                {
                    b.HasOne("Nemesys.Models.UserModels.Reporter", "reporter")
                        .WithMany("reports")
                        .HasForeignKey("reporteridNum");

                    b.Navigation("reporter");
                });

            modelBuilder.Entity("Nemesys.Models.UserModels.Investigator", b =>
                {
                    b.HasOne("Nemesys.Models.UserModels.Reporter", null)
                        .WithOne()
                        .HasForeignKey("Nemesys.Models.UserModels.Investigator", "idNum")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nemesys.Models.FormModels.Report", b =>
                {
                    b.Navigation("investigation");
                });

            modelBuilder.Entity("Nemesys.Models.UserModels.Reporter", b =>
                {
                    b.Navigation("reports");
                });

            modelBuilder.Entity("Nemesys.Models.UserModels.Investigator", b =>
                {
                    b.Navigation("investigations");
                });
#pragma warning restore 612, 618
        }
    }
}
