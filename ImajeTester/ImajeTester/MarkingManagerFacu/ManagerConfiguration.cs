using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Configuration
{
    [Serializable]
    public class ManagerConfiguration
    {
        [XmlAttribute("sessionName")]
        public string SessionName { get; set; }

        [XmlAttribute("productionManagerName")]
        public string ProductiongManagerName { get; set; }

        [XmlAttribute("handshakeManagerName")]
        public string HandshakeManagerName { get; set; }

        [XmlAttribute("applicationId")]
        public string ApplicationId { get; set; }

        [XmlArray("Stations")]
        [XmlArrayItem("Station")]
        public List<StationConfiguration> Stations { get; set; }
    }
}
