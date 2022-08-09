using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQL.Models
{
    internal class PropertyOptions
    {
        public int PositionConstructor { get; internal set; }

        public PropertyInfo PropertyInfo { get; }

        public ColumnAttribute ColumnAttribute { get; }

        public PropertyOptions(int positionObject, PropertyInfo propertyInfo, ColumnAttribute columnAttribute)
        {
            PositionConstructor = positionObject;
            PropertyInfo = propertyInfo;
            ColumnAttribute = columnAttribute;
        }
    }
}
