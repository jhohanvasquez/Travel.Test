using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Entities
{
    public class DataLog
    {
        public string PhysicalID { get; set; }
        public string LastUserCommand { get; set; }
        public bool StatusSafe { get; set; }
        public string ErrorMessage { get; set; }
        public string DateLog { get; set; }
    }
}
