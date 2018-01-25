using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuejasWS.Models
{
    public partial class QuejasDBContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Complain> Complain { get; set; }
        public virtual DbSet<Institution> Institution { get; set; }
        public virtual DbSet<Subcategory> Subcategory { get; set; }
        public virtual DbSet<SubcategoryInstitution> SubcategoryInstitution { get; set; }
        public virtual DbSet<UserC> UserC { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=digitalserver.database.windows.net;Initial Catalog=QuejasDS;Integrated Security=False;User ID=Brian;Password=Cyb3rCl0n;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory);

                entity.ToTable("CATEGORY");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("idCategory")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment);

                entity.ToTable("COMMENT");

                entity.HasIndex(e => e.IdComplain)
                    .HasName("Ref88");

                entity.HasIndex(e => e.IdUser)
                    .HasName("Ref49");

                entity.Property(e => e.IdComment)
                    .HasColumnName("idComment")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment1)
                    .HasColumnName("Comment")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IdComplain)
                    .IsRequired()
                    .HasColumnName("idComplain")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdComplainNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdComplain)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCOMPLAIN8");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefUSER_C9");
            });

            modelBuilder.Entity<Complain>(entity =>
            {
                entity.HasKey(e => e.IdComplain);

                entity.ToTable("COMPLAIN");

                entity.HasIndex(e => e.IdSubcategory)
                    .HasName("Ref137");

                entity.HasIndex(e => e.IdUser)
                    .HasName("Ref42");

                entity.Property(e => e.IdComplain)
                    .HasColumnName("idComplain")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.IdSubcategory)
                    .HasColumnName("idSubcategory")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Photo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSubcategoryNavigation)
                    .WithMany(p => p.Complain)
                    .HasForeignKey(d => d.IdSubcategory)
                    .HasConstraintName("RefSUBCATEGORY7");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Complain)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefUSER_C2");
            });

            modelBuilder.Entity<Institution>(entity =>
            {
                entity.HasKey(e => e.IdInstitution);

                entity.ToTable("INSTITUTION");

                entity.Property(e => e.IdInstitution)
                    .HasColumnName("idInstitution")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.LinkFacebook)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.LinkTwitter)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subcategory>(entity =>
            {
                entity.HasKey(e => e.IdSubcategory);

                entity.ToTable("SUBCATEGORY");

                entity.HasIndex(e => e.IdCategory)
                    .HasName("Ref116");

                entity.Property(e => e.IdSubcategory)
                    .HasColumnName("idSubcategory")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdCategory)
                    .IsRequired()
                    .HasColumnName("idCategory")
                    .HasColumnType("char(10)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Subcategory)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefCATEGORY6");
            });

            modelBuilder.Entity<SubcategoryInstitution>(entity =>
            {
                entity.HasKey(e => e.IdSubCategoryInstitution);

                entity.ToTable("SUBCATEGORY_INSTITUTION");

                entity.HasIndex(e => e.IdInstitution)
                    .HasName("Ref1512");

                entity.HasIndex(e => e.IdSubcategory)
                    .HasName("Ref1311");

                entity.Property(e => e.IdSubCategoryInstitution)
                    .HasColumnName("idSubCategoryInstitution")
                    .HasColumnType("char(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdInstitution)
                    .IsRequired()
                    .HasColumnName("idInstitution")
                    .HasColumnType("char(10)");

                entity.Property(e => e.IdSubcategory)
                    .IsRequired()
                    .HasColumnName("idSubcategory")
                    .HasColumnType("char(10)");

                entity.HasOne(d => d.IdInstitutionNavigation)
                    .WithMany(p => p.SubcategoryInstitution)
                    .HasForeignKey(d => d.IdInstitution)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefINSTITUTION12");

                entity.HasOne(d => d.IdSubcategoryNavigation)
                    .WithMany(p => p.SubcategoryInstitution)
                    .HasForeignKey(d => d.IdSubcategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RefSUBCATEGORY11");
            });

            modelBuilder.Entity<UserC>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("USER_C");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LinkFacebook)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LinkTwitter)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Locale)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Verified)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
