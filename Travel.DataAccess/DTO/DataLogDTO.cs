using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.DTO
{
    public class DataLogDTO : TableEntity
    {
        public string PhysicalID { get; set; }
        public string User { get; set; }
        public bool State { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime DateLog { get; set; }
    }
}
