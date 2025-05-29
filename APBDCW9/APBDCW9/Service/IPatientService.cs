using APBDCW9.DTOs;

namespace APBDCW9.Service;

public interface IPatientService
{
    Task<PatientGET> GetPatient(int patientID);
}