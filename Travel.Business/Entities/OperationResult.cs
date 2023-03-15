using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Entities
{
    public class OperationResult:Journey        
    {
        public bool Successful { get { return string.IsNullOrEmpty(CodeError); } }
        public string CodeError { get; set; }
        public string MessageError { get; set; }

    }
}
