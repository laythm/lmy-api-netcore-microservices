using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Framework.Common
{
    public static class ObjectExtension
    {
        public static void CopyPropertyValues(this object destination, object source, params string[] execludeList)
        { 
            if (source != null)
            {
                System.Reflection.PropertyInfo[] destProperties = destination.GetType().GetProperties();

                foreach (System.Reflection.PropertyInfo sourceProperty in source.GetType().GetProperties().Where(x => (x.PropertyType.GetGenericArguments().Count() < 1)))
                {
                    if (execludeList == null || execludeList.All(x => x != sourceProperty.Name))
                    {
                        foreach (System.Reflection.PropertyInfo destProperty in destProperties)
                        {
                            if (destProperty.Name == sourceProperty.Name &&
                        destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                            {
                                if (destProperty.CanWrite)
                                    destProperty.SetValue(destination, sourceProperty.GetValue(
                                        source, new object[] { }), new object[] { });

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
