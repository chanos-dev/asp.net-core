﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using quiz_server.Models;

namespace quiz_server.Migrations
{
    [DbContext(typeof(QuizDbContext))]
    [Migration("20220321134914_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("quiz_server.Models.Participant", b =>
                {
                    b.Property<int>("ParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("TimeTaken")
                        .HasColumnType("int");

                    b.HasKey("ParticipantId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("quiz_server.Models.Question", b =>
                {
                    b.Property<int>("QnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Option1")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Option2")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Option3")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Option4")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("QnInWords")
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("QnId");

                    b.ToTable("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
