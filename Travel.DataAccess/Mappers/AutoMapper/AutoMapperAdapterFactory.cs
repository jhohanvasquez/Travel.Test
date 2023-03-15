using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.DataAccess.Mappers.AutoMapper
{
    public class AutoMapperAdapterFactory : IMapperAdapterFactory
    {
        public AutoMapperAdapterFactory()
        {
            var profiles = AppDomain.CurrentDomain
                                   .GetAssemblies()
                                   .SelectMany(a => a.GetTypes())
                                   .Where(t => t.BaseType == typeof(Profile));

            Mapper.Initialize(cfg =>
            {
                foreach (var item in profiles)
                {
                    if (item.FullName != "AutoMapper.Configuration.MapperConfigurationExpression+NamedProfile")
                        cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
        }

        public IMapperAdapter Create()
        {
            return new AutoMapperAdapter();
        }
    }
}
