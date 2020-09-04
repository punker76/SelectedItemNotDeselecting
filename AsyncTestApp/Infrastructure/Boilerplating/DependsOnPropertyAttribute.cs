using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DependsOnPropertyAttribute : Attribute
    {
        public string Dependence { get; }

        public DependsOnPropertyAttribute(string dependingProperty)
        {
            Dependence = dependingProperty;
        }
    }
}
