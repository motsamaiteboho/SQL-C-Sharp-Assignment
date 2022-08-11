﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace RouletteGameApi.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20220809155855_secondaddition")]
    partial class secondaddition
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Entities.Models.Bet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("BetId");

                    b.Property<string>("BetOn")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("BetValue")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<Guid?>("SpinId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimestampUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SpinId");

                    b.ToTable("Bets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            BetOn = "HIGH",
                            BetValue = 50.62m,
                            SpinId = new Guid("c8d4c053-49b6-410c-bc78-2d54a9891870"),
                            TimestampUtc = new DateTime(2022, 8, 9, 15, 58, 54, 334, DateTimeKind.Utc).AddTicks(7207)
                        },
                        new
                        {
                            Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            BetOn = "RED",
                            BetValue = 46.45m,
                            SpinId = new Guid("4d490a70-94ce-4d15-9494-5248280c2ce3"),
                            TimestampUtc = new DateTime(2022, 8, 9, 15, 58, 54, 334, DateTimeKind.Utc).AddTicks(7210)
                        });
                });

            modelBuilder.Entity("Entities.Models.Payout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("PayoutId");

                    b.Property<Guid?>("BetId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimestampUtc")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPayout")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BetId");

                    b.ToTable("Payouts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                            BetId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                            TimestampUtc = new DateTime(2022, 8, 9, 15, 58, 54, 334, DateTimeKind.Utc).AddTicks(7131),
                            TotalPayout = 522.5m
                        },
                        new
                        {
                            Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52a"),
                            BetId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                            TimestampUtc = new DateTime(2022, 8, 9, 15, 58, 54, 334, DateTimeKind.Utc).AddTicks(7133),
                            TotalPayout = 145.5m
                        });
                });

            modelBuilder.Entity("Entities.Models.Spin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("SpinId");

                    b.Property<long?>("SpinResult")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimestampUtc")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Spins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8d4c053-49b6-410c-bc78-2d54a9891870"),
                            SpinResult = 5L,
                            TimestampUtc = new DateTime(2022, 8, 9, 15, 58, 54, 334, DateTimeKind.Utc).AddTicks(6946)
                        },
                        new
                        {
                            Id = new Guid("4d490a70-94ce-4d15-9494-5248280c2ce3"),
                            SpinResult = 7L,
                            TimestampUtc = new DateTime(2022, 8, 9, 15, 58, 54, 334, DateTimeKind.Utc).AddTicks(6951)
                        });
                });

            modelBuilder.Entity("Entities.Models.Bet", b =>
                {
                    b.HasOne("Entities.Models.Spin", "Spin")
                        .WithMany("Bets")
                        .HasForeignKey("SpinId");

                    b.Navigation("Spin");
                });

            modelBuilder.Entity("Entities.Models.Payout", b =>
                {
                    b.HasOne("Entities.Models.Bet", "Bet")
                        .WithMany("Payouts")
                        .HasForeignKey("BetId");

                    b.Navigation("Bet");
                });

            modelBuilder.Entity("Entities.Models.Bet", b =>
                {
                    b.Navigation("Payouts");
                });

            modelBuilder.Entity("Entities.Models.Spin", b =>
                {
                    b.Navigation("Bets");
                });
#pragma warning restore 612, 618
        }
    }
}
