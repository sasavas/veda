﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Veda.Infrastructure.DataAccess;

#nullable disable

namespace Veda.Infrastructure.Migrations
{
    [DbContext(typeof(VedaDbContext))]
    [Migration("20231228124934_AddPhoneNumberToCustomer")]
    partial class AddPhoneNumberToCustomer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Veda.Application.Modules.CustomerModule.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("TCKimlikNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tc_kimlik_no");

                    b.HasKey("Id")
                        .HasName("pk_customer");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("Veda.Application.Modules.CustomerModule.Models.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<DateTime?>("End")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end");

                    b.Property<int>("MembershipStatusId")
                        .HasColumnType("integer")
                        .HasColumnName("membership_status_id");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start");

                    b.HasKey("Id")
                        .HasName("pk_membership");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_membership_customer_id");

                    b.HasIndex("MembershipStatusId")
                        .HasDatabaseName("ix_membership_membership_status_id");

                    b.ToTable("membership", (string)null);
                });

            modelBuilder.Entity("Veda.Application.Modules.CustomerModule.Models.MembershipStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<long>("DigitalStorageCapacityInBytes")
                        .HasColumnType("bigint")
                        .HasColumnName("digital_storage_capacity_in_bytes");

                    b.Property<int>("RecipientLimit")
                        .HasColumnType("integer")
                        .HasColumnName("recipient_limit");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status_name");

                    b.HasKey("Id")
                        .HasName("pk_membership_status");

                    b.ToTable("membership_status", (string)null);
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.DigitalContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("FolderId")
                        .HasColumnType("integer")
                        .HasColumnName("folder_id");

                    b.Property<string>("HashCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash_code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<long>("SizeInBytes")
                        .HasColumnType("bigint")
                        .HasColumnName("size_in_bytes");

                    b.HasKey("Id")
                        .HasName("pk_digital_content");

                    b.HasIndex("FolderId")
                        .HasDatabaseName("ix_digital_content_folder_id");

                    b.ToTable("digital_content", (string)null);
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("folder_name");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer")
                        .HasColumnName("recipient_id");

                    b.Property<double>("SizeOccupied")
                        .HasColumnType("double precision")
                        .HasColumnName("size_occupied");

                    b.HasKey("Id")
                        .HasName("pk_folder");

                    b.HasIndex("RecipientId")
                        .IsUnique()
                        .HasDatabaseName("ix_folder_recipient_id");

                    b.ToTable("folder", (string)null);
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customer_id");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("EMailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("e_mail_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("TCKimlikNo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tc_kimlik_no");

                    b.HasKey("Id")
                        .HasName("pk_recipient");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("ix_recipient_customer_id");

                    b.ToTable("recipient", (string)null);
                });

            modelBuilder.Entity("Veda.Application.Modules.CustomerModule.Models.Customer", b =>
                {
                    b.OwnsOne("Veda.Application.SharedKernel.Models.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("integer")
                                .HasColumnName("customer_id");

                            b1.Property<string>("CountryCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("country_code");

                            b1.Property<long>("Number")
                                .HasColumnType("bigint")
                                .HasColumnName("number");

                            b1.HasKey("CustomerId")
                                .HasName("pk_customer_phone_numbers");

                            b1.ToTable("customer_phone_numbers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("CustomerId")
                                .HasConstraintName("fk_customer_phone_numbers_customer_customer_id");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Veda.Application.Modules.CustomerModule.Models.Membership", b =>
                {
                    b.HasOne("Veda.Application.Modules.CustomerModule.Models.Customer", null)
                        .WithMany("Memberships")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fk_membership_customer_customer_id");

                    b.HasOne("Veda.Application.Modules.CustomerModule.Models.MembershipStatus", "MembershipStatus")
                        .WithMany()
                        .HasForeignKey("MembershipStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_membership_membership_status_membership_status_id");

                    b.Navigation("MembershipStatus");
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.DigitalContent", b =>
                {
                    b.HasOne("Veda.Application.Modules.RecipientModule.Models.Folder", null)
                        .WithMany("DigitalContents")
                        .HasForeignKey("FolderId")
                        .HasConstraintName("fk_digital_content_folder_folder_id");
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.Folder", b =>
                {
                    b.HasOne("Veda.Application.Modules.RecipientModule.Models.Recipient", null)
                        .WithOne("Folder")
                        .HasForeignKey("Veda.Application.Modules.RecipientModule.Models.Folder", "RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_folder_recipient_recipient_id");
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.Recipient", b =>
                {
                    b.HasOne("Veda.Application.Modules.CustomerModule.Models.Customer", null)
                        .WithMany("Recipients")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_recipient_customer_customer_id");

                    b.OwnsOne("Veda.Application.SharedKernel.Models.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("RecipientId")
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            b1.Property<string>("CountryCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("country_code");

                            b1.Property<long>("Number")
                                .HasColumnType("bigint")
                                .HasColumnName("number");

                            b1.HasKey("RecipientId")
                                .HasName("pk_recipient_phone_numbers");

                            b1.ToTable("recipient_phone_numbers", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RecipientId")
                                .HasConstraintName("fk_recipient_phone_numbers_recipient_id");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("Veda.Application.Modules.CustomerModule.Models.Customer", b =>
                {
                    b.Navigation("Memberships");

                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.Folder", b =>
                {
                    b.Navigation("DigitalContents");
                });

            modelBuilder.Entity("Veda.Application.Modules.RecipientModule.Models.Recipient", b =>
                {
                    b.Navigation("Folder")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
