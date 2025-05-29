using APBDCW9.Models;
using APBDCW9.DTOs;
using APBDCW9.Dal;
using Microsoft.EntityFrameworkCore;

namespace APBDCW9.Service;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;

    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task PresPOST(PrescriptionPOST prescription)
    {
        if (prescription.Medicaments.Count > 10)
        {
            throw new ArgumentException("Prescription cannot contain more than 10 medicaments.");
        }
        
        if (prescription.DueDate < prescription.Date)
        {
            throw new ArgumentException("DueDate must be greater or equal to Date.");
        }
        
        foreach (var med in prescription.Medicaments)
        {
            bool medExists = await _context.Medicaments.AnyAsync(m => m.IdMedicament == med.IdMedicament);
            if (!medExists)
            {
                throw new ArgumentException($"Medicament with ID {med.IdMedicament} does not exist.");
            }
        }
        
        var patient = await _context.Patients.FirstOrDefaultAsync(p =>
            p.FirstName == prescription.Patient.FirstName &&
            p.LastName == prescription.Patient.LastName &&
            p.Birthdate == prescription.Patient.Birthdate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.Birthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
        
        var newPrescription = new Prescription
        {
            IdPatient = patient.IdPatient,
            IdDoctor = prescription.IdDoctor,
            Date = prescription.Date,
            DueDate = prescription.DueDate
        };

        _context.Prescriptions.Add(newPrescription);
        await _context.SaveChangesAsync();
        
        var newPrescriptionMedicaments = prescription.Medicaments.Select(pm => new PrescriptionMedicament
        {
            IdPrescription = newPrescription.IdPrescription,
            IdMedicament = pm.IdMedicament,
            Dose = pm.Dose,
            Details = pm.Description
        }).ToList();

        await _context.PrescriptionMedicaments.AddRangeAsync(newPrescriptionMedicaments);
        await _context.SaveChangesAsync();
    }
}
