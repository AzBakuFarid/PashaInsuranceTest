using PashaInsuranceTest.DbEntities.Enums;
using PashaInsuranceTest.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PashaInsuranceTest.Helpers
{
    public static class Mapper
    {
        public static Dictionary<ServiceTypeRequestEnum, ServiceTypeEnum> ServiceRequestTypes;

        static Mapper()
        {
            ServiceRequestTypes = new Dictionary<ServiceTypeRequestEnum, ServiceTypeEnum> {
                { ServiceTypeRequestEnum.Type1, ServiceTypeEnum.Type1 },
                { ServiceTypeRequestEnum.Type2, ServiceTypeEnum.Type2 },
                { ServiceTypeRequestEnum.Type3, ServiceTypeEnum.Type3 }
            };
        }
    }
}
