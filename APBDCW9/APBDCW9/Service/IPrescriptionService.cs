using APBDCW9.DTOs;

namespace APBDCW9.Service;

public interface IPrescriptionService
{
    Task PresPOST(PrescriptionPOST prescription);
}