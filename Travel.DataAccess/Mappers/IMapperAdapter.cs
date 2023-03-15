using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.Mappers
{
    public interface IMapperAdapter
    {
        TTarget Adapt<TSource, TTarget>(TSource source)
        where TTarget : class, new()
        where TSource : class;

        TTarget Adapt<TTarget>(object source)
        where TTarget : class, new();

        TTarget Merge<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class;
    }
}
