﻿// <auto-generated />
using System;
using HouYun3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HouYun3.Migrations
{
    [DbContext(typeof(HouYun3Context))]
    [Migration("20240205180446_Hou2")]
    partial class Hou2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HouYun3.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HouYun3.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("HouYun3.Models.Like", b =>
                {
                    b.Property<int>("LikeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LikeId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("LikeId");

                    b.HasIndex("UserId1");

                    b.HasIndex("VideoId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("HouYun3.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NotificationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("HouYun3.Models.SearchHistory", b =>
                {
                    b.Property<int>("SearchHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SearchHistoryId"));

                    b.Property<DateTime>("SearchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SearchQuery")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SearchHistoryId");

                    b.HasIndex("UserId");

                    b.ToTable("SearchHistories");
                });

            modelBuilder.Entity("HouYun3.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HouYun3.Models.Video", b =>
                {
                    b.Property<int>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VideoId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DurationSeconds")
                        .HasColumnType("int");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("VideoId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("HouYun3.Models.View", b =>
                {
                    b.Property<int>("ViewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ViewId"));

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.HasKey("ViewId");

                    b.HasIndex("UserId");

                    b.HasIndex("VideoId");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("HouYun3.Models.WatchHistory", b =>
                {
                    b.Property<int>("WatchHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WatchHistoryId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WatchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("WatchHistoryId");

                    b.HasIndex("UserId1");

                    b.HasIndex("VideoId");

                    b.ToTable("WatchHistories");
                });

            modelBuilder.Entity("HouYun3.Models.WatchLater", b =>
                {
                    b.Property<int>("WatchLaterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WatchLaterId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WatchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("WatchLaterId");

                    b.HasIndex("UserId1");

                    b.HasIndex("VideoId");

                    b.ToTable("WatchLaters");
                });

            modelBuilder.Entity("HouYun3.Models.Comment", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany("Comments")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.Like", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany("Likes")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.Notification", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HouYun3.Models.SearchHistory", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("SearchHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HouYun3.Models.Video", b =>
                {
                    b.HasOne("HouYun3.Models.Category", "Category")
                        .WithMany("Videos")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("Videos")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HouYun3.Models.View", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany("Views")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.WatchHistory", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("WatchHistory")
                        .HasForeignKey("UserId1");

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.WatchLater", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("WatchLaterList")
                        .HasForeignKey("UserId1");

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.Category", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("HouYun3.Models.User", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("SearchHistory");

                    b.Navigation("Videos");

                    b.Navigation("WatchHistory");

                    b.Navigation("WatchLaterList");
                });

            modelBuilder.Entity("HouYun3.Models.Video", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("Views");
                });
#pragma warning restore 612, 618
        }
    }
}
