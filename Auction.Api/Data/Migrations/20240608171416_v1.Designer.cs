﻿// <auto-generated />
using System;
using Auction.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Auction.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240608171416_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Auction.Core.Models.Bid", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<decimal>("BidValue")
                        .HasColumnType("MONEY");

                    b.Property<long>("BidderId")
                        .HasColumnType("bigint");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BidderId");

                    b.HasIndex("ItemId");

                    b.ToTable("Bids", (string)null);
                });

            modelBuilder.Entity("Auction.Core.Models.Bidder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Bidders", (string)null);
                });

            modelBuilder.Entity("Auction.Core.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("NVARCHAR");

                    b.Property<long?>("HighestBidId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("InicialBidValue")
                        .HasColumnType("MONEY");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("NVARCHAR");

                    b.Property<DateTime>("TimeEndAuction")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Items", (string)null);
                });

            modelBuilder.Entity("Auction.Core.Models.Bid", b =>
                {
                    b.HasOne("Auction.Core.Models.Bidder", "Bidder")
                        .WithMany()
                        .HasForeignKey("BidderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Auction.Core.Models.Item", "Item")
                        .WithMany("Bids")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bidder");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Auction.Core.Models.Item", b =>
                {
                    b.Navigation("Bids");
                });
#pragma warning restore 612, 618
        }
    }
}
