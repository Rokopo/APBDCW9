using APBDCW9.Dal;
using APBDCW9.Models;
using APBDCW9.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBDCW9.Service;

public class PatientService : IPatientService
{
    private readonly DatabaseContext _context;
    
    public PatientService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<PatientGET> GetPatient(int patientId)
    {
        var DTOres = await _context.Patients.Include(p => p.Prescriptions)
            .ThenInclude(p => p.Doctor).Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments).ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (DTOres == null)
        {
            return null;
        }
        
        var result =  new PatientGET
        {
            IdPatient = DTOres.IdPatient, FirstName = DTOres.FirstName, LastName = DTOres.LastName,
            Birthdate = DTOres.Birthdate,
            Prescriptions = DTOres.Prescriptions.OrderBy(pr => pr.DueDate).Select(pr => new PrescriptionDetails
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Doctor = new DoctorDetails
                {
                    IdDoctor = pr.Doctor.IdDoctor,
                    FirstName = pr.Doctor.FirstName
                },
                Medicaments = pr.PrescriptionMedicaments
                    .Select(pm => new MedicamentDetails
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Description = pm.Details
                    }).ToList()
            }).ToList()
        };
        return result;
    }
    
}