using System;

namespace Travel.DataAccess.DTO
{
    public class SecuritySafeCommandDTO
    {
        public string ServiceCode { get; set; }
        public string CommandType { get; set; }
        public string UserName { get; set; }
        public string GenerationDate { get; set; }
        public string Address { get; set; }
    }
}
