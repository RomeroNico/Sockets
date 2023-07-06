using System;
using System.Configuration;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.PrinterConfiguration
{
    [Serializable]
    public class Font : ConfigurationElement
    {
        private const string NameProperty = "name";
        private const string ValueProperty = "value";
        private const string APIProperty = "api";

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
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty(ValueProperty, IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)base[ValueProperty];
            }

            set
            {
                base[ValueProperty] = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [ConfigurationProperty(APIProperty, IsRequired = false, DefaultValue = "bcd")]
        public string APISymbol
        {
            get
            {
                return (string)base[APIProperty];
            }

            set
            {
                base[APIProperty] = value;
            }
        }
    }
}
