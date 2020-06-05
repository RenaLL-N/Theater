using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Theater
{
    public partial class DBTheaterContext : DbContext
    {
        public DBTheaterContext()
        {
        }

        public DBTheaterContext(DbContextOptions<DBTheaterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<PerformanceAuthor> PerformanceAuthor { get; set; }
        public virtual DbSet<PerformanceGenres> PerformanceGenres { get; set; }
        public virtual DbSet<TheaterPerformances> TheaterPerformances { get; set; }
        public virtual DbSet<Theaters> Theaters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MYPC\\SQLEXPRESS; Database=DBTheater; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.HasKey(e => e.AuId);

                entity.Property(e => e.AuId).HasColumnName("AU_ID");

                entity.Property(e => e.AuCt).HasColumnName("AU_CT");

                entity.Property(e => e.AuDateb)
                    .HasColumnName("AU_DATEB")
                    .HasColumnType("date");

                entity.Property(e => e.AuDated)
                    .HasColumnName("AU_DATED")
                    .HasColumnType("date");

                entity.Property(e => e.AuInfo).HasColumnName("AU_INFO");

                entity.Property(e => e.AuName)
                    .IsRequired()
                    .HasColumnName("AU_NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.AuCtNavigation)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.AuCt)
                    .HasConstraintName("FK_Authors_Cities1");
            });

            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CtId);

                entity.Property(e => e.CtId).HasColumnName("CT_ID");

                entity.Property(e => e.CoId).HasColumnName("CO_ID");

                entity.Property(e => e.CtName)
                    .IsRequired()
                    .HasColumnName("CT_NAME")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Co)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CoId)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CoId);

                entity.Property(e => e.CoId).HasColumnName("CO_ID");

                entity.Property(e => e.CoName)
                    .IsRequired()
                    .HasColumnName("CO_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GnId)
                    .HasName("PK_Generes");

                entity.Property(e => e.GnId).HasColumnName("GN_ID");

                entity.Property(e => e.GnName)
                    .IsRequired()
                    .HasColumnName("GN_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Performance>(entity =>
            {
                entity.HasKey(e => e.PrId)
                    .HasName("PK_Perfomance");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");

                entity.Property(e => e.PrInfo)
                    .HasColumnName("PR_INFO")
                    .HasColumnType("ntext");

                entity.Property(e => e.PrName)
                    .IsRequired()
                    .HasColumnName("PR_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.PrYear)
                    .HasColumnName("PR_YEAR")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PerformanceAuthor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuId).HasColumnName("AU_ID");

                entity.Property(e => e.Info).HasColumnName("INFO");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");

                entity.HasOne(d => d.Au)
                    .WithMany(p => p.PerformanceAuthor)
                    .HasForeignKey(d => d.AuId)
                    .HasConstraintName("FK_PerfomenseAuthor_Authors");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.PerformanceAuthor)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_PerfomenseAuthor_Perfomance");
            });

            modelBuilder.Entity<PerformanceGenres>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GnId).HasColumnName("GN_ID");

                entity.Property(e => e.Info).HasColumnName("INFO");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");

                entity.HasOne(d => d.Gn)
                    .WithMany(p => p.PerformanceGenres)
                    .HasForeignKey(d => d.GnId)
                    .HasConstraintName("FK_PerfomanceGenres_Genres");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.PerformanceGenres)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_PerfomanceGenres_Perfomance");
            });

            modelBuilder.Entity<TheaterPerformances>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Info).HasColumnName("INFO");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");

                entity.Property(e => e.ThId).HasColumnName("TH_ID");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.TheaterPerformances)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_TheatrePerfomens_Perfomance");

                entity.HasOne(d => d.Th)
                    .WithMany(p => p.TheaterPerformances)
                    .HasForeignKey(d => d.ThId)
                    .HasConstraintName("FK_TheatrePerfomens_Theaters");
            });

            modelBuilder.Entity<Theaters>(entity =>
            {
                entity.HasKey(e => e.ThId);

                entity.Property(e => e.ThId).HasColumnName("TH_ID");

                entity.Property(e => e.ThCt).HasColumnName("TH_CT");

                entity.Property(e => e.ThInfo)
                    .HasColumnName("TH_INFO")
                    .HasColumnType("ntext");

                entity.Property(e => e.ThName)
                    .IsRequired()
                    .HasColumnName("TH_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.ThWebsite).HasColumnName("TH_WEBSITE");

                entity.HasOne(d => d.ThCtNavigation)
                    .WithMany(p => p.Theaters)
                    .HasForeignKey(d => d.ThCt)
                    .HasConstraintName("FK_Theaters_Cities");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
