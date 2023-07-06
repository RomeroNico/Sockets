using System;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Model
{
    [Serializable]
    public class Items
    {
        private int idTracking;
        private string printerName;
        private string station;
        private string message;

        public int IdTrking
        {
            get
            {
                return idTracking;
            }

            set
            {
                idTracking = value;
            }
        }
        public string PrinterName
        {
            get
            {
                return printerName;
            }

            set
            {
                printerName = value;
            }
        }
        public string Station
        {
            get
            {
                return station;
            }

            set
            {
                station = value;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
    }
}
