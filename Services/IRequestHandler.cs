using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Services
{
    public interface IRequestHandler
    {
        bool IsSucceded { get; }
        List<string> Errors { get; } 
    }
}
