using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UniversalPictureViewer
{
    public static class ReflectionHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        public static void MemberwiseAssign(object target, object source)
        {
            // assign memberwise to fire the property changed events for databinding
            PropertyInfo[] properties = target.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                // skip properties without setter
                if (!property.CanWrite) continue;

                // set the values
                if (source != null)
                {
                    // e. g. target.property1 = source.property1;
                    property.SetValue(target, property.GetValue(source));
                }
                else
                {
                    property.SetValue(target, null);
                }
            }
        }
    }
}
