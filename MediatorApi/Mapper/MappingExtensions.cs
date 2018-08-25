using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;

namespace MediatorApi.Mapper
{
    public static class MappingExtensions
    {
        private static readonly object Sync = new object();

        /// <summary>
        ///     Map source object to destination object
        /// </summary>
        /// <typeparam name="TSource">Source</typeparam>
        /// <typeparam name="TDestination">Destination</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="destination">Destination object</param>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MapTo<TDestination>(source, destination);
        }

        /// <summary>
        ///     Map source object to destination object
        /// </summary>
        /// <typeparam name="TDestination">Destination</typeparam>
        /// <param name="source">Source</param>
        public static TDestination MapTo<TDestination>(this object source) where TDestination : new()
        {
            return MapTo(source, new TDestination());
        }

        /// <summary>
        ///     Map source object to destination object
        /// </summary>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="source">Source</param>
        /// <param name="destination">Destination</param>
        /// <returns></returns>
        private static TDestination MapTo<TDestination>(object source, TDestination destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            var sourceType = GetType(source);
            var destinationType = GetType(destination);
            var map = GetMap(sourceType, destinationType);
            if (map != null)
                return AutoMapper.Mapper.Map(source, destination);
            lock (Sync)
            {
                map = GetMap(sourceType, destinationType);
                if (map != null)
                    return AutoMapper.Mapper.Map(source, destination);
                InitMaps(sourceType, destinationType);
            }

            return AutoMapper.Mapper.Map(source, destination);
        }

        /// <summary>
        /// Gets object type
        /// </summary>
        private static Type GetType(object obj)
        {
            if (obj == null) throw new ArgumentNullException("object is null");

            var type = obj.GetType();
            if (obj is IEnumerable == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new ArgumentException("Generic type parameter can not be empty for mapping.");
            return genericArgumentsTypes[0];
        }

        /// <summary>
        ///     Get Mapping Configuration
        /// </summary>
        /// <param name="sourceType">Source</param>
        /// <param name="destinationType">Destination</param>
        /// <returns></returns>
        private static TypeMap GetMap(Type sourceType, Type destinationType)
        {
            try
            {
                return AutoMapper.Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
            }
            catch (InvalidOperationException)
            {
                lock (Sync)
                {
                    try
                    {
                        return AutoMapper.Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                    }
                    catch (InvalidOperationException)
                    {
                        InitMaps(sourceType, destinationType);
                    }

                    return AutoMapper.Mapper.Configuration.FindTypeMapFor(sourceType, destinationType);
                }
            }
        }

        /// <summary>
        ///     Initialize Mapping Configuration
        /// </summary>
        /// <param name="sourceType">Source</param>
        /// <param name="destinationType">Destination</param>
        private static void InitMaps(Type sourceType, Type destinationType)
        {
            try
            {
                var maps = AutoMapper.Mapper.Configuration.GetAllTypeMaps();
                AutoMapper.Mapper.Initialize(config =>
                {
                    ClearConfig();
                    foreach (var item in maps)
                        config.CreateMap(item.SourceType, item.DestinationType);
                    config.CreateMap(sourceType, destinationType);
                });
            }
            catch (InvalidOperationException)
            {
                AutoMapper.Mapper.Initialize(config => { config.CreateMap(sourceType, destinationType); });
            }
        }

        /// <summary>
        ///     Clear configuration
        /// </summary>
        private static void ClearConfig()
        {
            var typeMapper = typeof(AutoMapper.Mapper).GetTypeInfo();
            var configuration = typeMapper.GetDeclaredField("_configuration");
            configuration.SetValue(null, null, BindingFlags.Static, null, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Map Source collections to target collections
        /// </summary>
        /// <typeparam name="TDestination">Destination</typeparam>
        /// <param name="source">Source</param>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            return MapTo<List<TDestination>>(source);
        }
    }
}
