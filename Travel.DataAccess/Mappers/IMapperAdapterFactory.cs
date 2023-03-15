using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.Mappers
{
    public interface IMapperAdapterFactory
    {
        IMapperAdapter Create();
    }
}
