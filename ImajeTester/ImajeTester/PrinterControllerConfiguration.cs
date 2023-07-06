using System;
using System.Xml.Serialization;

namespace ImajeTester
{
    [Serializable]
    public class PrinterControllerConfiguration
    {
        [XmlAttribute("jetNumber")]
        public string JetNumber { get; set; }

        [XmlAttribute("posLine1")]
        public string PosLine1 { get; set; }

        [XmlAttribute("posLine2")]
        public string PosLine2 { get; set; }

        [XmlAttribute("allowTwoLines")]
        public bool AllowTwoLines { get; set; }

        [XmlAttribute("allowOnlyEnterKey")]
        public bool AllowOnlyEnterKey { get; set; }

        [XmlAttribute("bold")]
        public int Bold { get; set; }

        [XmlAttribute("fontUp")]
        public int FontUp { get; set; }

        [XmlAttribute("fontDown")]
        public int FontDown { get; set; }

        [XmlAttribute("separationSymbols")]
        public int SeparationSymbols { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        [XmlAttribute("Printer")]
        public PrinterConfiguration Printer { get; set; }


    }
}