using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.Mappers.AutoMapper
{
    public class AutoMapperAdapter : IMapperAdapter
    {
        public TTarget Adapt<TTarget>(object source) where TTarget : class, new()
        {
            return Mapper.Map<TTarget>(source);
        }

        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        public TTarget Merge<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return Mapper.Map<TSource, TTarget>(source, target);
        }
    }
}
