using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Model
{
    [Serializable]
    public class Balance
    {
        private string expediente;
       
        public int IdBalance { get; set; }
       
        public int Ciclo { get; set; }

        public int Colada { get; set; }
       
        public string Expediente
        {
            get
            {
                return this.expediente.Substring(0, 1) + "/" +
                       this.expediente.Substring(1, 4) + "." +
                       this.expediente.Substring(5);
            }

            set
            {
                this.expediente = value;
            }
        }

       
        public string ExpedienteSinFormato
        {
            get
            {
                return this.expediente;
            }
        }

       
        public int Campagna { get; set; }

      
        public int Entrada { get; set; }

      
        public int SalidaBuenos { get; set; }

       
        public int SalidaDescartes { get; set; }

      
        public int Ajustes { get; set; }

       
        public int Activo { get; set; }

       
        public int Lote { get; set; }

       
        public int AddedEnteredPieces { get; set; }

       
        public int StenciledCount { get; set; }

      
        public bool IsManualBalance { get; set; }

        public override string ToString()
        {
            return string.Format("Expediente: {0} - Colada: {1} - Ciclo: {2} - Lote: {3}", Expediente, Colada, Ciclo, Lote);
        }
    }
}

