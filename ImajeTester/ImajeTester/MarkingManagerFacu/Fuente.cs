using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Model
{
    [Serializable]
    public class Fuente
    {
        private string nombre;
        private int valor;
        private string api;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
               nombre=value;
            }
        }
        public int Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }
        public string Api
        {
            get
            {
                return api;
            }

            set
            {
                api = value;
            }
        }
    }
}
