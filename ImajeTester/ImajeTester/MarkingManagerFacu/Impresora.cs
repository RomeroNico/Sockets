using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenaris.AutoAr.Facu.Manager.Marking.Shared.Model
{
    [Serializable]
    public class Impresora
    {
        private string nombre;
        private string cfgFile;
        private bool use9040structure;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string  CfgFile
        {
            get
            {
                return cfgFile;
            }

            set
            {
                cfgFile = value;
            }
        }

        public bool Use9040structure
        {
            get
            {
                return use9040structure;
            }

            set
            {
                use9040structure = value;
            }
        }
    }
}
