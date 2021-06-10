using System;

namespace PashaInsuranceTest.DTOs.Interfaces
{
    public interface ICLientMainData
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        DateTime? Birthday { get; set; }
    }
}
