using System;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared
{
    [Serializable]
    public class Cartel
    {
        public string cartel = string.Empty;
      

        public Cartel() { }

        /// <summary>
        /// Gets a value indicating whether Success.
        /// </summary>        
        public bool Success { get;  set; }
        public bool Checked { get; set; }

        /// <summary>
        /// Returned Mask from Broker
        /// </summary>
        public string Mask
        {
            get
            {
                return this.cartel;
            }
        }

        /// <summary>
        /// Gets BalanceAbierto.
        /// </summary>
        public IBalance BalanceAbierto { get; private set; }

    }
}
