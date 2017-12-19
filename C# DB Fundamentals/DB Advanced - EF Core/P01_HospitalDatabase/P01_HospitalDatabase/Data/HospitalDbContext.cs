using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext() { }

        public HospitalDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientMedicament { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configurations.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.Property(e => e.FirstName)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                entity.Property(e => e.Email)
                    .IsRequired(true)
                    .IsUnicode(false)
                    .HasMaxLength(80);

                entity.Property(e => e.HasInsurance)
                .HasDefaultValue(true);
            });

            modelBuilder.Entity<Visitation>(entity =>
            {
                entity.HasKey(e => e.VisitationId);

                entity.Property(e => e.Date)
                .IsRequired(true)
                .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(true);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Visitations)
                .HasForeignKey(e => e.PatientId);

                entity.Property(e => e.DoctorId)
                .IsRequired(false);

                entity.HasOne(e => e.Doctor)
                .WithMany(d => d.Visitations)
                .HasForeignKey(e => e.DoctorId);
            });

            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(e => e.DiagnoseId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);

                entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(true)
                .IsRequired(true);

                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId);
            });

            modelBuilder.Entity<Medicament>(entity =>
            {
                entity.HasKey(e => e.MedicamentId);

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true);
            });

            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(pm => new { pm.MedicamentId, pm.PatientId });

                entity.HasOne(e => e.Medicament)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(e => e.MedicamentId);

                entity.HasOne(e => e.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(e => e.PatientId);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(e => e.Specialty)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(100);
            });
        }
    }
}