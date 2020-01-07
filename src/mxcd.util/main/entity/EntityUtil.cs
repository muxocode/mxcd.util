using mxcd.util.exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mxcd.util.entity
{
    /// <summary>
    /// Utils for objects
    /// </summary>
    public static class EntityUtil
    {
        /// <summary>
        /// Get all keys and respected values
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="entity">enity</param>
        /// <param name="includeProps">if props are included, default:true</param>
        /// <param name="includeFields">if fields are included, default:false</param>
        /// <param name="excludedNames">Fields or props names that will be omitted</param>
        /// <returns></returns>
        public static IEnumerable<IObjectPart> GetKeysValues<T>(this T entity, bool includeProps = true, bool includeFields = false, IEnumerable<string> excludedNames = null) where T : class
        {
            try
            {
                var aFields = new List<ObjectPart>();

                if (includeProps)
                {
                    var props = typeof(T).GetProperties()
                    .Select(x => new ObjectPart() { Name = x.Name, Value = x.GetValue(entity), TypePart=TypeObjectPart.Property });

                    aFields.AddRange(props);
                }

                if (includeFields)
                {
                    var fields = typeof(T).GetFields()
                    .Select(x => new ObjectPart() { Name = x.Name, Value = x.GetValue(entity), TypePart = TypeObjectPart.Field });

                    aFields.AddRange(fields);
                }

                if (excludedNames != null)
                {
                    excludedNames = excludedNames.Select(x => x.ToUpper());
                    aFields = aFields
                        .Where(x => !excludedNames.Contains(x.Name.ToUpper()))
                        .ToList();
                }

                return aFields;
            }
            catch (Exception oEx)
            {
                throw new UtilException("Error on EntityUtil in GetKeysValues", oEx);
            }
        }

        /// <summary>
        /// Similar to typescript, assign props or fields from source to destiny
        /// </summary>
        /// <remarks>Names are case sensitive</remarks>
        /// <typeparam name="T">source type</typeparam>
        /// <typeparam name="P">target type</typeparam>
        /// <param name="entity">source</param>
        /// <param name="target">destiny</param>
        /// <param name="includeProps">if props are included, default:true</param>
        /// <param name="includeFields">if fields are included, default:false</param>
        /// <param name="excludedNames">Fields or props names that will be omitted</param>
        /// <returns>destiny</returns>
        public static P Assign<T, P>(this T entity, P target, bool includeProps = true, bool includeFields = false, IEnumerable<string> excludedNames = null) where T : class where P : class
        {
            try
            {
                var source = entity.GetKeysValues(includeProps, includeFields, excludedNames);
                var destiny = target.GetKeysValues(includeProps, includeFields, excludedNames);
                var mix = (from s in source
                          join d in destiny
                          on new { s.Name, s.TypePart } equals new { d.Name, d.TypePart }
                          select s)
                          .ToList();


                var type = typeof(P);

                if (includeProps)
                {
                    var props = from p in type.GetProperties()
                                join m in mix
                                on p.Name equals m.Name
                                where p.CanWrite && (p.GetSetMethod(/*nonPublic*/ true)?.IsPublic).GetValueOrDefault()
                                select new { prop = p, m.Value };

                    foreach (var p in props)
                    {
                        p.prop.SetValue(target, p.Value);
                    }
                }

                if (includeFields)
                {
                    var fields = from p in type.GetFields()
                                join m in mix
                                on p.Name equals m.Name
                                select new { field = p, m.Value };

                    foreach (var f in fields)
                    {
                        f.field.SetValue(target, f.Value);
                    }
                }

                return target;

            }
            catch (Exception oEx)
            {
                throw new UtilException("Error on EntityUtil in Assign", oEx);
            }
        }
    }
}
