using System;
using System.Configuration;

namespace ImajeTester
{
    [Serializable]
    public class Speed : ConfigurationElement
    {
        private const string NameProperty = "name";
        private const string ValueProperty = "value";

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty(NameProperty, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base[NameProperty];
            }

            set
            {
                base[NameProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the speed value.
        /// </summary>
        [ConfigurationProperty(ValueProperty, IsRequired = true)]
        public int Value
        {
            get
            {
                return (int)base[ValueProperty];
            }

            set
            {
                base[ValueProperty] = value;
            }
        }
    }
}
