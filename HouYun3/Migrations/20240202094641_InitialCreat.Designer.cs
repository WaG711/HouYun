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
    [Migration("20240202094641_InitialCreat")]
    partial class InitialCreat
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
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HouYun3.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentID"));

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VideoID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("UserID");

                    b.HasIndex("VideoID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("HouYun3.Models.Like", b =>
                {
                    b.Property<int>("LikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LikeID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VideoID")
                        .HasColumnType("int");

                    b.HasKey("LikeID");

                    b.HasIndex("UserID");

                    b.HasIndex("VideoID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("HouYun3.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationID"));

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NotificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("NotificationID");

                    b.HasIndex("UserID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("HouYun3.Models.SearchHistory", b =>
                {
                    b.Property<int>("SearchHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SearchHistoryID"));

                    b.Property<DateTime>("SearchDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SearchQuery")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("SearchHistoryID");

                    b.HasIndex("UserID");

                    b.ToTable("SearchHistories");
                });

            modelBuilder.Entity("HouYun3.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HouYun3.Models.Video", b =>
                {
                    b.Property<int>("VideoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VideoID"));

                    b.Property<int>("CategoryID")
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

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("VideoID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("HouYun3.Models.View", b =>
                {
                    b.Property<int>("ViewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ViewID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VideoID")
                        .HasColumnType("int");

                    b.HasKey("ViewID");

                    b.HasIndex("UserID");

                    b.HasIndex("VideoID");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("HouYun3.Models.WatchHistory", b =>
                {
                    b.Property<int>("WatchHistoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WatchHistoryID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VideoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("WatchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("WatchHistoryID");

                    b.HasIndex("UserID");

                    b.HasIndex("VideoID");

                    b.ToTable("WatchHistories");
                });

            modelBuilder.Entity("HouYun3.Models.WatchLater", b =>
                {
                    b.Property<int>("WatchLaterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WatchLaterID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VideoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("WatchDate")
                        .HasColumnType("datetime2");

                    b.HasKey("WatchLaterID");

                    b.HasIndex("UserID");

                    b.HasIndex("VideoID");

                    b.ToTable("WatchLaters");
                });

            modelBuilder.Entity("HouYun3.Models.Comment", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany("Comments")
                        .HasForeignKey("VideoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.Like", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany("Likes")
                        .HasForeignKey("VideoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.Notification", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HouYun3.Models.SearchHistory", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("SearchHistory")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HouYun3.Models.Video", b =>
                {
                    b.HasOne("HouYun3.Models.Category", "Category")
                        .WithMany("Videos")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("Videos")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HouYun3.Models.View", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany("Views")
                        .HasForeignKey("VideoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.WatchHistory", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("WatchHistory")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("HouYun3.Models.WatchLater", b =>
                {
                    b.HasOne("HouYun3.Models.User", "User")
                        .WithMany("WatchLaterList")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouYun3.Models.Video", "Video")
                        .WithMany()
                        .HasForeignKey("VideoID")
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
