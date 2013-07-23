using System.Reflection;

namespace Supermarkets
{
    static class Utils
    {
        // http://stackoverflow.com/questions/6961248/copy-properties-between-objects-using-reflection-and-extesnion-method
        public static void LoadPropertiesFrom(this object target, object source)
        {
            PropertyInfo[] sourceProps = source.GetType().GetProperties();
            var targetType = target.GetType();

            foreach (PropertyInfo sourceProp in sourceProps)
            {
                var targetProperty = targetType.GetProperty(sourceProp.Name);

                if (targetProperty == null)
                    continue;

                if (!targetProperty.CanWrite)
                    continue;

                object sourceValue = sourceProp.GetValue(source, null);

                if (sourceValue == null)
                    continue;

                targetProperty.SetValue(target, sourceValue, null);
            }
        }
    }
}