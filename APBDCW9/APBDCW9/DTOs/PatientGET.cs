namespace APBDCW9.DTOs;

public class PatientGET
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public ICollection<PrescriptionDetails> Prescriptions { get; set; }
}

public class PrescriptionDetails
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentDetails> Medicaments { get; set; }
    public DoctorDetails Doctor { get; set; }
    
}

public class DoctorDetails
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}
public class MedicamentDetails
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
}