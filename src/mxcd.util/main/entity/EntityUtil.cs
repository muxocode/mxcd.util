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
                    .Select(x => new ObjectPart() { Name = x.Name, Value = x.GetValue(entity) });

                    aFields.AddRange(props);
                }

                if (includeFields)
                {
                    var fields = typeof(T).GetFields()
                    .Select(x => new ObjectPart() { Name = x.Name, Value = x.GetValue(entity) });

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
    }
}
