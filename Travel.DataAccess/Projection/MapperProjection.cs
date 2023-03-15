using Travel.DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.DataAccess.Projection
{
    public static class MapperProjection
    {
        /// <summary>
        /// Project a table entity to entity core
        /// </summary>
        /// <typeparam name="TProjection">The entity core projection</typeparam>
        /// <param name="item">The source table entity to project</param>
        /// <returns>The projected type</returns>
        public static TProjection ProjectedAs<TProjection>(this object item)
            where TProjection : class, new()
        {
            var adapter = MapperAdapterFactory.CreateAdapter();
            return adapter.Adapt<TProjection>(item);
        }

        /// <summary>
        /// projected a enumerable collection of items
        /// </summary>
        /// <typeparam name="TProjection">The entity core projection type</typeparam>
        /// <param name="items">the collection of entity items</param>
        /// <returns>Projected collection</returns>
        public static List<TProjection> ProjectedAsCollection<TProjection>(this IEnumerable<object> items)
            where TProjection : class, new()
        {
            var adapter = MapperAdapterFactory.CreateAdapter();
            return adapter.Adapt<List<TProjection>>(items);
        }

        /// <summary>
        /// Merge between two elements
        /// </summary>
        /// <typeparam name="TMerge">Tipo de objeto a unificar Table Entity</typeparam>
        /// <param name="source">Origen</param>
        /// <param name="target">Destino</param>
        /// <returns>Objeto unificado</returns>
        public static TMerge Merge<TMerge>(this object source, TMerge target)
            where TMerge : class
        {
            var adapter = MapperAdapterFactory.CreateAdapter();
            return adapter.Merge<object, TMerge>(source, target);

        }
    }
}
