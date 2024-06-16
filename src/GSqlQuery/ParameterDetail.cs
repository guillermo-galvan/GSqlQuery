﻿using System;

namespace GSqlQuery
{
    /// <summary>
    /// Parameter detail 
    /// </summary>
    public sealed class ParameterDetail
    {
        /// <summary>
        /// Get Column
        /// </summary>
        public PropertyOptions PropertyOptions { get; }

        /// <summary>
        /// Get Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get Value
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Initializes a new instance of the ParameterDetail class.
        /// </summary>
        /// <param name="name">Name parameter</param>
        /// <param name="value">Value parameter</param>
        /// <exception cref="ArgumentNullException">Name must not be null or empty</exception>
        public ParameterDetail(string name, object value, PropertyOptions propertyOptions)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PropertyOptions = propertyOptions ?? throw new ArgumentNullException(nameof(propertyOptions));
            Value = value;
        }
    }
}