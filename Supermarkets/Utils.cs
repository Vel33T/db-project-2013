using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Supermarkets
{
    static class Utils
    {
        static Dictionary<Type, PropertyInfo[]> sourceProperties = new Dictionary<Type, PropertyInfo[]>();

        static Dictionary<Tuple<Type, string>, PropertyInfo> targetProperties = new Dictionary<Tuple<Type, string>, PropertyInfo>();

        // http://stackoverflow.com/questions/6961248/copy-properties-between-objects-using-reflection-and-extesnion-method
        public static void LoadPropertiesFrom(this object target, object source)
        {
            var sourceType = source.GetType();
            PropertyInfo[] sourceProps;
            if (!sourceProperties.TryGetValue(sourceType, out sourceProps))
            {
                sourceProps = (from prop in sourceType.GetProperties()
                               where prop.PropertyType.IsPrimitive || prop.PropertyType == typeof(string)
                               select prop).ToArray();
                sourceProperties[sourceType] = sourceProps;
            }

            var targetType = target.GetType();

            foreach (PropertyInfo sourceProp in sourceProps)
            {

                PropertyInfo targetProperty;

                if (!targetProperties.TryGetValue(Tuple.Create(targetType, sourceProp.Name), out targetProperty))
                {
                    targetProperty = targetType.GetProperty(sourceProp.Name, BindingFlags.IgnoreCase|BindingFlags.Public|BindingFlags.Instance);

                    if (targetProperty != null)
                        if (!targetProperty.CanWrite)
                            targetProperty = null;

                    if (targetProperty != null)
                        if (!targetProperty.PropertyType.IsPrimitive &&
                            targetProperty.PropertyType != typeof(string))
                            targetProperty = null;

                    if (targetProperty != null)
                        if (targetProperty.PropertyType.IsInterface)
                            targetProperty = null;

                    targetProperties[Tuple.Create(targetType, sourceProp.Name)] = targetProperty;
                }

                if (targetProperty == null)
                    continue;

                // if (typeof(IEnumerable).IsAssignableFrom(targetProperty.PropertyType))
                // continue;

                object sourceValue = sourceProp.GetValue(source, null);

                if (sourceValue == null)
                    continue;

                targetProperty.SetValue(target, sourceValue, null);
            }
        }
    }
}