using ItemsFilter.View;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics;

namespace ItemsFilter.Model 
{
    /// <summary>
    /// Defines the range filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [View(typeof(RangeFilterView))]
    public class RangeFilter<T> : PropertyFilter, IRangeFilter<T>
        where T :struct, IComparable 
    {
        Func<object, T> getter;
        private T? compareTo = null;
        private T? compareFrom = null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="getter">Func that return from item values to compare.</param>
        protected RangeFilter(Func<object, T> getter)
        {
            Debug.Assert(getter != null, "getter is null.");
            this.getter = getter;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public RangeFilter(ItemPropertyInfo propertyInfo)
            : base() 
        {
            //if (!typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            Debug.Assert(propertyInfo != null, "propertyInfo is null.");
            Debug.Assert(typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType), "The typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            base.PropertyInfo = propertyInfo;
            Func<object, object> getterItem = ((PropertyDescriptor)(PropertyInfo.Descriptor)).GetValue;
            getter = t => ((T)getterItem(t));
            base.Name = "In range:";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="CompareFrom">Minimum value.</param>
        /// <param name="CompareTo">Maximum value.</param>
        public RangeFilter(ItemPropertyInfo propertyInfo, T CompareFrom, T CompareTo)
            : this(propertyInfo) 
        {
            compareTo = CompareTo;
            compareFrom = CompareFrom;
            RefreshIsActive(); 
        }

        /// <summary>
        /// Get or set the minimum value used in the comparison. 
        /// If CompareFrom and CompareTo is null, filter deactivated.
        /// </summary>
        public T? CompareFrom
        {
            get { return compareFrom; }
            set 
            {
                if (!Object.Equals(compareFrom, value))
                {
                    compareFrom = value;
                    RefreshIsActive();
                    OnIsActiveChanged();
                    OnPropertyChanged("CompareFrom");
                 }
            }
        }
        /// <summary>
        /// Get or set the maximum value used in the comparison.  
        /// If CompareFrom and CompareTo is null, filter deactivated.
        /// </summary>
        public T? CompareTo
        {
            get { return compareTo; }
            set
            {
                if (!Object.Equals(compareTo, value))
                {
                    compareTo = value;
                    RefreshIsActive(); 
                    OnIsActiveChanged();
                    OnPropertyChanged("CompareTo");
                }
            }
        }

        /// <summary>
        /// Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged()
        {
            if (!IsActive) 
            {
                CompareFrom = null;
                CompareTo = null;
            }
            base.OnIsActiveChanged();
        }
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e)
        {
            if (e.Accepted) 
            {
                if (e.Item == null)
                    e.Accepted = false;
                else 
                {
                    T value = getter(e.Item);
                    e.Accepted = (Object.ReferenceEquals(compareFrom, null)| value.CompareTo(compareFrom) >= 0)
                        && (Object.ReferenceEquals(compareTo, null) | value.CompareTo(compareTo) <= 0);
                }
            }
        }
        private void RefreshIsActive()
        {
            base.IsActive = !(Object.ReferenceEquals(compareFrom, null) && Object.ReferenceEquals(compareTo, null));
        }
        
        #region IRangeFilter Members

        object IRangeFilter.CompareFrom
        {
            get { return CompareFrom; }
            set { CompareFrom=(T?)value; }
        }

        object IRangeFilter.CompareTo
        {
            get { return CompareFrom; }
            set { CompareFrom = (T?)value; }
        }

        #endregion
    }
}
