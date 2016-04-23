using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RentMe
{
    public class SortableBindingList<T> : BindingList<T>
    {
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return this.sortPropertyValue; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return this.sortDirectionValue; }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return this.isSortedValue; }
        }

        private bool isSortedValue;
        private ListSortDirection sortDirectionValue;
        private PropertyDescriptor sortPropertyValue;

        public SortableBindingList()
        {
        }

        public SortableBindingList(IEnumerable<T> list)
        {
            foreach (object o in list)
            {
                Add((T) o);
            }
        }

        protected override void ApplySortCore(PropertyDescriptor prop,
            ListSortDirection direction)
        {
            var interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType == null && prop.PropertyType.IsValueType)
            {
                var underlyingType = Nullable.GetUnderlyingType(prop.PropertyType);

                if (underlyingType != null)
                {
                    interfaceType = underlyingType.GetInterface("IComparable");
                }
            }

            if (interfaceType != null)
            {
                this.sortPropertyValue = prop;
                this.sortDirectionValue = direction;

                IEnumerable<T> query = Items;

                if (direction == ListSortDirection.Ascending)
                {
                    query = query.OrderBy(i => prop.GetValue(i));
                }
                else
                {
                    query = query.OrderByDescending(i => prop.GetValue(i));
                }

                var newIndex = 0;
                foreach (object item in query)
                {
                    Items[newIndex] = (T) item;
                    newIndex++;
                }

                this.isSortedValue = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
            {
                throw new NotSupportedException("Cannot sort by " + prop.Name +
                                                ". This" + prop.PropertyType +
                                                " does not implement IComparable");
            }
        }
    }
}