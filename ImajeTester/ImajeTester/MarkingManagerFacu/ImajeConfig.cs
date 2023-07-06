using System;
using System.Configuration;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.PrinterConfiguration
{
    [Serializable]
    public class ImajeConfig : ConfigurationElement
    {
        private const string JetNumberProperty = "jetNumber";
        private const string PosLine1Property = "posLine1";
        private const string PosLine2Property = "posLine2";
        private const string AllowTwoLinesProperty = "allowTwoLines";
        private const string AllowOnlyEnterKeyProperty = "allowOnlyEnterKey";
        private const string FontUpProperty = "fontUp";
        private const string FontDownProperty = "fontDown";
        private const string SpeedProperty = "speed";
        private const string BoldProperty = "bold";
        private const string SeparationSymbolsProperty = "separationSymbols";

        /// <summary>
        /// Gets the Jet Number.
        /// </summary>
        [ConfigurationProperty(JetNumberProperty, IsRequired = true)]
        public int JetNumber
        {
            get
            {
                return (int)base[JetNumberProperty];
            }
        }

        /// <summary>
        /// Código de referencia de Posición 1 (Primer Renglon).
        /// </summary>
        [ConfigurationProperty(PosLine1Property, IsRequired = false, DefaultValue = 28)]
        public int PosLine1
        {
            get
            {
                return (int)base[PosLine1Property];
            }
        }

        /// <summary>
        /// Código de referencia de Posición 2 (Segundo Renglon).
        /// </summary>
        [ConfigurationProperty(PosLine2Property, IsRequired = false, DefaultValue = 1)]
        public int PosLine2
        {
            get
            {
                return (int)base[PosLine2Property];
            }
        }

        /// <summary>
        /// Permitir mensajes en 2 líneas.
        /// </summary>
        [ConfigurationProperty(AllowTwoLinesProperty, IsRequired = false, DefaultValue = false)]
        public bool AllowTwoLines
        {
            get
            {
                return (bool)base[AllowTwoLinesProperty];
            }
        }

        /// <summary>
        /// Permitir sólo tecla ENTER en edición de Máscara
        /// </summary>
        [ConfigurationProperty(AllowOnlyEnterKeyProperty, IsRequired = false, DefaultValue = false)]
        public bool AllowOnlyEnterKey
        {
            get
            {
                return (bool)base[AllowOnlyEnterKeyProperty];
            }
        }

        /// <summary>
        /// Código de referencia para fuente en 2 línea (Parte de arriba)
        /// </summary>
        [ConfigurationProperty(FontUpProperty, IsRequired = false, DefaultValue = "208")]
        public string FontUp
        {
            get
            {
                return (string)base[FontUpProperty];
            }
        }

        /// <summary>
        /// Código de referencia para fuente en 2 línea (Parte de abajo)
        /// </summary>
        [ConfigurationProperty(FontDownProperty, IsRequired = false, DefaultValue = "207")]
        public string FontDown
        {
            get
            {
                return (string)base[FontDownProperty];
            }
        }

        /// <summary>
        /// Velocidad de Impresión (en desuso)
        /// </summary>
        [ConfigurationProperty(SpeedProperty, IsRequired = false)]
        public int Speed
        {
            get
            {
                return (int)base[SpeedProperty];
            }
        }

        /// <summary>
        /// Bold Type Imaje.
        /// </summary>
        [ConfigurationProperty(BoldProperty, IsRequired = true)]
        public int Bold
        {
            get
            {
                return (int)base[BoldProperty];
            }
        }

        /// <summary>
        /// Separación entre símbolos de los Extremos
        /// </summary>
        [ConfigurationProperty(SeparationSymbolsProperty, IsRequired = false, DefaultValue = 32)]
        public int SeparationSymbols
        {
            get
            {
                return (int)base[SeparationSymbolsProperty];
            }
        }


    }
}
