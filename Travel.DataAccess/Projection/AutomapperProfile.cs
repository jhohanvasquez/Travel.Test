using AutoMapper;
using Travel.Business.Entities;
using Travel.DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.Projection
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {            
            CreateMap<DataLogDTO, DataLog>();      
            CreateMap<DataLog, DataLogDTO>();
        }
    }
}
