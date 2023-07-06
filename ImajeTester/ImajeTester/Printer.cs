using System;
using System.Configuration;

namespace ImajeTester
{
    [Serializable]
    public class Printer : ConfigurationElement
    {
        private const string NameProperty = "name";
        private const string ConfigFileProperty = "cfgFile";
        private const string Use9040StructureProperty = "use9040structure";

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
        /// Gets or sets the config file.
        /// </summary>
        [ConfigurationProperty(ConfigFileProperty, IsRequired = true)]
        public string ConfigFile
        {
            get
            {
                return (string)base[ConfigFileProperty];
            }

            set
            {
                base[ConfigFileProperty] = value;
            }
        }

        /// <summary>
        /// Gets 9040 Structure Configuration Imaje S8.
        /// </summary>
        [ConfigurationProperty(Use9040StructureProperty, IsRequired = false, DefaultValue = true)]
        public bool Use9040Structure
        {
            get
            {
                return (bool)base[Use9040StructureProperty];
            }

            set
            {
                base[Use9040StructureProperty] = value;
            }
        }
    }

    [Serializable]
    public enum PrintStatus
    {
        Ok = 0,
        GenericError=-1,
        NoPrinterSelected=-2,
        NoSpeedSelected=-3,
        ConnectionError=-4,
        GeneralException=-100

    }
}
