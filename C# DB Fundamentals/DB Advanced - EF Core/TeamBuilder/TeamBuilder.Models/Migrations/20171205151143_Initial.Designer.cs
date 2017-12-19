﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TeamBuilder.Data;
using TeamBuilder.Data.Models;

namespace TeamBuilder.Data.Migrations
{
    [DbContext(typeof(TeamBuilderContext))]
    [Migration("20171205151143_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamBuilder.Data.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("EventId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.Invitation", b =>
                {
                    b.Property<int>("InvitationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvitedUserId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("TeamId");

                    b.HasKey("InvitationId");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasColumnType("CHAR(10)");

                    b.Property<string>("Description")
                        .HasMaxLength(32);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("TeamId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.TeamEvent", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("TeamId");

                    b.HasKey("EventId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamEvents");
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.UserTeam", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("UserId");

                    b.HasKey("TeamId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTeams");
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.Event", b =>
                {
                    b.HasOne("TeamBuilder.Data.Models.User", "Creator")
                        .WithMany("Events")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.Invitation", b =>
                {
                    b.HasOne("TeamBuilder.Data.Models.User", "InvitedUser")
                        .WithMany("Invitations")
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamBuilder.Data.Models.Team", "Team")
                        .WithMany("Invitations")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.TeamEvent", b =>
                {
                    b.HasOne("TeamBuilder.Data.Models.Event", "Event")
                        .WithMany("TeamEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamBuilder.Data.Models.Team", "Team")
                        .WithMany("TeamEvents")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamBuilder.Data.Models.UserTeam", b =>
                {
                    b.HasOne("TeamBuilder.Data.Models.Team", "Team")
                        .WithMany("UserTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamBuilder.Data.Models.User", "User")
                        .WithMany("UserTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}