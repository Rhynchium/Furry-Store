﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MSIT147thGraduationTopic.EFModels
{
    public partial class GraduationTopicContext : DbContext
    {
        public GraduationTopicContext()
        {
        }

        public GraduationTopicContext(DbContextOptions<GraduationTopicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponOwner> CouponOwners { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<EvaluationInput> EvaluationInputs { get; set; }
        public virtual DbSet<MallDisplay> MallDisplays { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Merchandise> Merchandises { get; set; }
        public virtual DbSet<MerchandiseSearch> MerchandiseSearches { get; set; }
        public virtual DbSet<MerchandiseTag> MerchandiseTags { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderList> OrderLists { get; set; }
        public virtual DbSet<OrderWithMember> OrderWithMembers { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Spec> Specs { get; set; }
        public virtual DbSet<SpecDisplayforOrder> SpecDisplayforOrders { get; set; }
        public virtual DbSet<SpecWithFullMerchandise> SpecWithFullMerchandises { get; set; }
        public virtual DbSet<SpecWithMerchandiseName> SpecWithMerchandiseNames { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GraduationTopic;User ID=sa6;Password=sa6;Integrated Security=True;TrustServerCertificate=true;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasOne(d => d.Spec)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.SpecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartItems_Specs");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponCondition).HasColumnType("money");

                entity.Property(e => e.CouponDiscount).HasColumnType("money");

                entity.Property(e => e.CouponEndDate).HasColumnType("date");

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CouponStartDate).HasColumnType("date");
            });

            modelBuilder.Entity<CouponOwner>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CouponSerialNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Coupon)
                    .WithMany()
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponOwners_Coupons");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponOwners_Members");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.AvatarName).HasMaxLength(50);

                entity.Property(e => e.EmployeeAccount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EmployeePassword)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Evaluation>(entity =>
            {
                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluations_Merchandises");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Evaluations)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluations_Orders");
            });

            modelBuilder.Entity<EvaluationInput>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("EvaluationInput");

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.MerchandiseName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SpecName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MallDisplay>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MallDisplay");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(81);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Avatar).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.ConfirmGuid)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.District).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.NickName).HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.MerchandiseName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Merchandises)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Merchandises_Brands");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Merchandises)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Merchandise_Category");
            });

            modelBuilder.Entity<MerchandiseSearch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MerchandiseSearch");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.MerchandiseName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<MerchandiseTag>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Merchandise)
                    .WithMany()
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchandiseTags_Merchandises");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MerchandiseTags_Tags");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ContactPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PurchaseTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(300);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Member");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_PaymentMethods");
            });

            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLists)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderList_Orders");

                entity.HasOne(d => d.Spec)
                    .WithMany(p => p.OrderLists)
                    .HasForeignKey(d => d.SpecId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderLists_Specs");
            });

            modelBuilder.Entity<OrderWithMember>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OrderWithMember");

                entity.Property(e => e.PaymentMethodName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PurchaseTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(300);
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.Property(e => e.PaymentMethodId).ValueGeneratedNever();

                entity.Property(e => e.PaymentMethodName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Spec>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.SpecName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Merchandise)
                    .WithMany(p => p.Specs)
                    .HasForeignKey(d => d.MerchandiseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Specs_Merchandises");
            });

            modelBuilder.Entity<SpecDisplayforOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SpecDisplayforOrder");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(81);
            });

            modelBuilder.Entity<SpecWithFullMerchandise>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SpecWithFullMerchandise");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.MerchandiseName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SpecName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SpecWithMerchandiseName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SpecWithMerchandiseName");

                entity.Property(e => e.MerchandiseId).HasColumnName("MerchandiseID");

                entity.Property(e => e.MerchandiseName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SpecName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}