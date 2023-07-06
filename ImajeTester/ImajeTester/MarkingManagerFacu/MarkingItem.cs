using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Model
{
    [Serializable]
    public class MarkingItem
    {
        private int idMarking;
        private int idBatch;
        private string station;
        private string cicloActivo;
        private string mask;
        private bool hasSECU;
        private int secu;
        private bool check;

        public int Secu
        {
            get
            {
                return secu;
            }

            set
            {
                secu = value;
            }
        }
        public bool Check
        {
            get
            {
                return check;
            }

            set
            {
                check = value;
            }
        }
        public bool HasSECU
        {
            get
            {
                return hasSECU;
            }

            set
            {
                hasSECU = value;
            }
        }
        public string Mask
        {
            get
            {
                return mask;
            }

            set
            {
                mask = value;
            }
        }
        public string CicloActivo
        {
            get
            {
                return cicloActivo;
            }

            set
            {
                cicloActivo = value;
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
        public int IdBatch
        {
            get
            {
                return idBatch;
            }

            set
            {
                idBatch = value;
            }
        }
        public int IdMarking
        {
            get
            {
                return idMarking;
            }

            set
            {
                idMarking = value;
            }
        }
    }
}
