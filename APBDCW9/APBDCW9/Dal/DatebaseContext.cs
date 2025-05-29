using Microsoft.EntityFrameworkCore;
using APBDCW9.Models;

namespace APBDCW9.Dal;

public class DatabaseContext :DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Medicament>().HasData(

    new Medicament { IdMedicament = 1, Name = "Paracetamol", Description = "Reduces fever and pain", Type = "OTC" },

    new Medicament { IdMedicament = 2, Name = "Metformin", Description = "Used to treat type 2 diabetes", Type = "Prescription" },

    new Medicament { IdMedicament = 3, Name = "Cetirizine", Description = "Antihistamine for allergies", Type = "OTC" }

);
 
modelBuilder.Entity<Doctor>().HasData(

    new Doctor { IdDoctor = 1, FirstName = "Maria", LastName = "Wójcik", Email = "maria.wojcik@gmail.com" },

    new Doctor { IdDoctor = 2, FirstName = "Tomasz", LastName = "Zieliński", Email = "tomasz.zielinski@gmail.com" }

);
 
modelBuilder.Entity<Patient>().HasData(

    new Patient { IdPatient = 1, FirstName = "Katarzyna", LastName = "Nowak", Birthdate = new DateTime(1978, 7, 22) },

    new Patient { IdPatient = 2, FirstName = "Michał", LastName = "Kowalczyk", Birthdate = new DateTime(1982, 11, 3) }

);
 
modelBuilder.Entity<Prescription>().HasData(

    new Prescription { IdPrescription = 1, Date = new DateTime(2024, 4, 15), DueDate = new DateTime(2024, 4, 22), IdPatient = 1, IdDoctor = 1 },

    new Prescription { IdPrescription = 2, Date = new DateTime(2024, 5, 1), DueDate = new DateTime(2024, 5, 8), IdPatient = 2, IdDoctor = 2 },

    new Prescription { IdPrescription = 3, Date = new DateTime(2024, 5, 10), DueDate = new DateTime(2024, 5, 17), IdPatient = 1, IdDoctor = 2 }

);
 
modelBuilder.Entity<PrescriptionMedicament>().HasData(

    new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 1, Details = "Take one tablet every 6 hours" },

    new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 3, Dose = 1, Details = "Take once daily" },

    new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 2, Dose = 2, Details = "Take with meals" },

    new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 3, Dose = 1, Details = "Use as needed for allergy symptoms" }

);

 
    }
}