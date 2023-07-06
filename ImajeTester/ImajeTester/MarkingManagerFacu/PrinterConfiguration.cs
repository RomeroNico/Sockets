using System;
using System.Xml.Serialization;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Configuration
{
    [Serializable]
    public class PrinterConfiguration
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("configFile")]
        public string ConfigFile { get; set; }

        [XmlAttribute("use9040Structure")]
        public bool Use9040Structure { get; set; }

        [XmlAttribute("speedName")]
        public string SpeedName { get; set; }

        [XmlAttribute("speedValue")]
        public string SpeedValue { get; set; }

        [XmlAttribute("apiSymbol")]
        public string APISymbol { get; set; }

        [XmlAttribute("fontName")]
        public string FontName { get; set; }

        [XmlAttribute("fontValue")]
        public string FontValue { get; set; }


    }
}
