using Microsoft.EntityFrameworkCore;

namespace FitnessClubDB.Models.Database
{
    public partial class FitnessClubContext : DbContext
    {
        private readonly string _connectionString;

        public FitnessClubContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FitnessClubContext(DbContextOptions<FitnessClubContext> options, string connectionString)
            : base(options)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<Cheque> Cheques { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Complex> Complexes { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MembershipService> MembershipServices { get; set; } = null!;
        public virtual DbSet<Specialization> Specializations { get; set; } = null!;
        public virtual DbSet<Trainer> Trainers { get; set; } = null!;
        public virtual DbSet<Visit> Visits { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cheque>(entity =>
            {
                entity.ToTable("cheque");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("date")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.ServiceId).HasColumnName("service_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Cheques)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cheque__client_i__17F790F9");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Cheques)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__cheque__membersh__17036CC0");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(15)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(15)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(15)
                    .HasColumnName("middle_name");

                entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__client__trainer___06CD04F7");
            });

            modelBuilder.Entity<Complex>(entity =>
            {
                entity.ToTable("complex");

                entity.HasIndex(e => e.ComplexName, "complex_complex_name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComplexName)
                    .HasMaxLength(15)
                    .HasColumnName("complex_name");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("membership");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price)
                    .HasColumnType("smallmoney")
                    .HasColumnName("price");

                entity.Property(e => e.TimeLimitUntil).HasColumnName("time_limit_until");

                entity.Property(e => e.ValidityInDays).HasColumnName("validity_in_days");
            });

            modelBuilder.Entity<MembershipService>(entity =>
            {
                entity.ToTable("membership_service");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ComplexId).HasColumnName("complex_id");

                entity.Property(e => e.MembershipId).HasColumnName("membership_id");

                entity.HasOne(d => d.Complex)
                    .WithMany(p => p.MembershipServices)
                    .HasForeignKey(d => d.ComplexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__membershi__compl__14270015");

                entity.HasOne(d => d.Membership)
                    .WithMany(p => p.MembershipServices)
                    .HasForeignKey(d => d.MembershipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__membershi__membe__1332DBDC");
            });

            modelBuilder.Entity<Specialization>(entity =>
            {
                entity.ToTable("specialization");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasMaxLength(15)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.ToTable("trainer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(15)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(15)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(15)
                    .HasColumnName("middle_name");

                entity.Property(e => e.SpecializationId).HasColumnName("specialization_id");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Trainers)
                    .HasForeignKey(d => d.SpecializationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("trainer_specialization_id_fk");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.ToTable("visit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.ComplexId).HasColumnName("complex_id");

                entity.Property(e => e.VisitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("visit_time");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__visit__client_id__0F624AF8");

                entity.HasOne(d => d.Complex)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.ComplexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__visit__complex_i__10566F31");
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("workout");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.TrainerId).HasColumnName("trainer_id");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__workout__client___09A971A2");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__workout__trainer__0A9D95DB");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}