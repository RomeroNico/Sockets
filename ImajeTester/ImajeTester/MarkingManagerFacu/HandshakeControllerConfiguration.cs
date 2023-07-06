using System;
using System.Xml.Serialization;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Configuration
{
    [Serializable]
    public class HandshakeControllerConfiguration
    {
        [XmlAttribute("handshakeManagerName")]
        public string HandshakeManagerName { get; set; }

        [XmlAttribute("applicationId")]
        public int ApplicationId { get; set; }

        [XmlAttribute("markingHandshakeCode")]
        public string MarkingHandshakeCode { get; private set; }

    }
}