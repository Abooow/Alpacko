using Microsoft.EntityFrameworkCore;

namespace Alpacko.API.Models
{
    public partial class AlpackoDatabaseContext : DbContext
    {
        public AlpackoDatabaseContext(DbContextOptions<AlpackoDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageDetail> PackageDetail { get; set; }
        public virtual DbSet<PackageRecipient> PackageRecipient { get; set; }
        public virtual DbSet<PackageSender> PackageSender { get; set; }
        public virtual DbSet<PackageSizeName> PackageSizeName { get; set; }
        public virtual DbSet<PostOffice> PostOffice { get; set; }
        public virtual DbSet<RegisteredPackage> RegisteredPackage { get; set; }
        public virtual DbSet<SentPackage> SentPackage { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.PackageDetail)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.PackageDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Package_PackageDetail");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Package_PackageRecipient");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Package_PackageSender");
            });

            modelBuilder.Entity<PackageDetail>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.SizeName)
                    .WithMany(p => p.PackageDetail)
                    .HasForeignKey(d => d.SizeNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageDetail_PackageSize");
            });

            modelBuilder.Entity<PackageRecipient>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Co).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<PackageSender>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<PackageSizeName>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PostOffice>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RegisteredPackage>(entity =>
            {
                entity.Property(e => e.PackageId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.RegisteredPackage)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisteredPackage_Package");

                entity.HasOne(d => d.PostOffice)
                    .WithMany(p => p.RegisteredPackage)
                    .HasForeignKey(d => d.PostOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegisteredPackage_PostOffice");
            });

            modelBuilder.Entity<SentPackage>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.PackageId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.SentPackage)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SentPackage_Package");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SentPackage)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SentPackage_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.PostOffice)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.PostOfficeId)
                    .HasConstraintName("FK_User_PostOffice");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
