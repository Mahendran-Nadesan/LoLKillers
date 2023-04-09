﻿// <auto-generated />
using System;
using LoLKillers.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoLKillers.API.Migrations
{
    [DbContext(typeof(LoLKillersDbContext))]
    [Migration("20230408172921_pc_new_4")]
    partial class pcnew4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoLKillers.API.Models.EF.LoLKillersConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfigKey")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("ConfigValue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("LoLKillersConfigs");
                });

            modelBuilder.Entity("LoLKillers.API.Models.EF.QueueTypeMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("QueueName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("RiotQueueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("QueueTypeMappings");
                });

            modelBuilder.Entity("LoLKillers.API.Models.EF.Summoner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotAccountId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotPuuId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTimeOffset>("SummonerLastUpdatedDate")
                        .HasColumnType("DATETIMEOFFSET");

                    b.Property<DateTimeOffset>("SummonerMatchesLastUpdatedDate")
                        .HasColumnType("DATETIMEOFFSET");

                    b.HasKey("Id");

                    b.ToTable("Summoners");
                });

            modelBuilder.Entity("LoLKillers.API.Models.EF.SummonerMatchChampStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssistsAgainstEnemyChampMatchAssists")
                        .HasColumnType("int");

                    b.Property<int>("DeathsToEnemyChamp")
                        .HasColumnType("int");

                    b.Property<bool>("IsWin")
                        .HasColumnType("bit");

                    b.Property<int>("KillsAgainstEnemyChamp")
                        .HasColumnType("int");

                    b.Property<string>("QueueType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("RiotChampId")
                        .HasColumnType("int");

                    b.Property<string>("RiotChampName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("RiotEnemyChampId")
                        .HasColumnType("int");

                    b.Property<string>("RiotEnemyChampName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotMatchId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotPuuId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("SummonerMatchChampStats");
                });

            modelBuilder.Entity("LoLKillers.API.Models.EF.SummonerMatchSummaryStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("FirstBlood")
                        .HasColumnType("bit");

                    b.Property<bool>("FirstBloodAssist")
                        .HasColumnType("bit");

                    b.Property<int>("GoldEarned")
                        .HasColumnType("int");

                    b.Property<int>("GoldSpent")
                        .HasColumnType("int");

                    b.Property<bool>("IsWin")
                        .HasColumnType("bit");

                    b.Property<int>("LongestTimeSpentLiving")
                        .HasColumnType("int");

                    b.Property<long>("MagicDamageDealtToChampions")
                        .HasColumnType("bigint");

                    b.Property<int>("MatchAssists")
                        .HasColumnType("int");

                    b.Property<int>("MatchDeaths")
                        .HasColumnType("int");

                    b.Property<int>("MatchKills")
                        .HasColumnType("int");

                    b.Property<int>("MinionsKilled")
                        .HasColumnType("int");

                    b.Property<long>("PhysicalDamageDealtToChampions")
                        .HasColumnType("bigint");

                    b.Property<string>("QueueType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("RiotChampId")
                        .HasColumnType("int");

                    b.Property<string>("RiotChampName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotMatchId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotPuuId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("RiotTeamId")
                        .HasColumnType("int");

                    b.Property<int>("Spell1Casts")
                        .HasColumnType("int");

                    b.Property<int>("Spell2Casts")
                        .HasColumnType("int");

                    b.Property<int>("Spell3Casts")
                        .HasColumnType("int");

                    b.Property<int>("Spell4Casts")
                        .HasColumnType("int");

                    b.Property<int>("SummonerSpell1Casts")
                        .HasColumnType("int");

                    b.Property<int>("SummonerSpell1Id")
                        .HasColumnType("int");

                    b.Property<int>("SummonerSpell2Casts")
                        .HasColumnType("int");

                    b.Property<int>("SummonerSpell2Id")
                        .HasColumnType("int");

                    b.Property<int>("TimeSpentAlive")
                        .HasColumnType("int");

                    b.Property<int>("TimeSpentDead")
                        .HasColumnType("int");

                    b.Property<long>("TotalDamageDealtToChampions")
                        .HasColumnType("bigint");

                    b.Property<int>("VisionScore")
                        .HasColumnType("int");

                    b.Property<int>("WardsPlaced")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SummonerMatchSummaryStats");
                });

            modelBuilder.Entity("LoLKillers.API.Models.EF.TeamMatchSummaryStat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsWin")
                        .HasColumnType("bit");

                    b.Property<int>("MatchDuration")
                        .HasColumnType("int");

                    b.Property<string>("QueueType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("RiotMatchId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("RiotTeamId")
                        .HasColumnType("int");

                    b.Property<int>("TeamAssists")
                        .HasColumnType("int");

                    b.Property<int>("TeamDeaths")
                        .HasColumnType("int");

                    b.Property<bool>("TeamFirstBlood")
                        .HasColumnType("bit");

                    b.Property<int>("TeamGoldEarned")
                        .HasColumnType("int");

                    b.Property<int>("TeamGoldSpent")
                        .HasColumnType("int");

                    b.Property<int>("TeamKills")
                        .HasColumnType("int");

                    b.Property<long>("TeamMagicDamageDealtToChampions")
                        .HasColumnType("bigint");

                    b.Property<int>("TeamMinionsKilled")
                        .HasColumnType("int");

                    b.Property<long>("TeamPhysicalDamageDealtToChampions")
                        .HasColumnType("bigint");

                    b.Property<long>("TeamTotalDamageDealtToChampions")
                        .HasColumnType("bigint");

                    b.Property<int>("TeamVisionScore")
                        .HasColumnType("int");

                    b.Property<int>("TeamWardsPlaced")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TeamMatchSummaryStats");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}