namespace Issue320
{
    using System;
    using System.Linq;

    public class Utility
    {
        /// <summary>
        /// Gets the runtime attribute value of a certain type
        /// http://stackoverflow.com/questions/2656189/how-do-i-read-an-attribute-on-a-class-at-runtime
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <returns></returns>
        public TValue GetAttributeValueOfType<TAttribute, TValue>(Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }

            return default(TValue);
        }
    }
}