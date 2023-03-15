using Travel.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Business.Contracts
{
    public interface ILogRegister : IDisposable
    {
        void Save(string classMessage, string method, string message);      

    }
}
