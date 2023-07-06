using System;
using System.Xml.Serialization;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Configuration
{
    [Serializable]
    public class StationConfiguration
    {
        [XmlAttribute("stationCode")]
        public string StationCode { get; set; }

        [XmlAttribute("machine")]
        public string MachineCode { get; set; }

        [XmlAttribute("brokerCommand")]
        public string BrokerCommand { get; set; }

       [XmlAttribute("markingHandshakeCode")]
        public string HandshakeCode { get; set; }

        [XmlAttribute("PrinterController")]
        public PrinterControllerConfiguration PrinterController { get; set; } 
    }
}
