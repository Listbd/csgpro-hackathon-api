using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGProHackathonAPI.Shared.Models
{
    public abstract class BaseModel
    {
        private Type _type;

        /// <summary>
        /// Returns the primary key property value.
        /// </summary>
        /// <returns>Returns the primary key property value.</returns>
        public int GetPrimaryKeyPropertyValue()
        {
            // Get the type.
            if (_type == null)
                _type = GetType();

            // Attempt to get a reference to the "primary key" property
            // by combining the type name with an "Id" suffix and searching
            // for a property with that name.
            var primaryKeyPropertyName = string.Format("{0}Id", _type.Name);
            var primaryKeyProperty = _type.GetProperty(primaryKeyPropertyName);

            if (primaryKeyProperty != null)
            {
                // Make sure that the primary key property's type is of type "int".
                if (primaryKeyProperty.PropertyType != typeof(int))
                {
                    throw new ApplicationException(
                        string.Format("The primary key property for type '{0}' is not of type 'int'.",
                        _type.FullName));
                }

                // Get the primary key property value.
                var primaryKeyPropertyValue = (int)primaryKeyProperty.GetValue(this);

                return primaryKeyPropertyValue;
            }
            else
            {
                throw new ApplicationException(
                    string.Format("Unable to find a primary key property with the name '{0}' for the '{1}' type.",
                    primaryKeyPropertyName, _type.FullName));
            }
        }

        /// <summary>
        /// Indicates if a model is new or not.
        /// </summary>
        /// <returns>Returns true if the model represents a new record, otherwise false.</returns>
        public bool IsNew()
        {
            var primaryKeyPropertyValue = GetPrimaryKeyPropertyValue();

            // Return "true" is the property value is "0".
            return primaryKeyPropertyValue == 0;
        }
    }
}
