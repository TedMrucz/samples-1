using System.ComponentModel;

namespace ItemsFilter.Model
{
    /// <summary>
    /// Base class for filter that use property of item.
    /// </summary>
    public abstract class PropertyFilter : Filter, IPropertyFilter
    {
        private ItemPropertyInfo propertyInfo;
        
        /// <summary>
        /// Gets the property info whose property name is filtered.
        /// </summary>
        /// <value>The property info.</value>
        public ItemPropertyInfo PropertyInfo 
        {
            get { return propertyInfo; }
            protected set 
            {
                propertyInfo = value;
            }
        }
    }
}
