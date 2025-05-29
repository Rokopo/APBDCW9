namespace APBDCW9.DTOs;

public class PrescriptionPOST
{
    public PatientDetails Patient { get; set; }
    public ICollection<MedicamentDetails2> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}

public class PatientDetails
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}


public class MedicamentDetails2
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}