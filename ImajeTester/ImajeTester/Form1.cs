using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImajeTester
{
    public partial class Form1 : Form
    {

        public PrinterController printerController;
        public PrinterControllerConfiguration PrinterCfg;

        public Form1()
        {
            InitializeComponent();
            PrinterCfg = new PrinterControllerConfiguration();
            PrinterCfg.JetNumber    = "1";
            PrinterCfg.PosLine1 = "28";
            PrinterCfg.PosLine1 = "1";
            PrinterCfg.AllowTwoLines = true;
            PrinterCfg.AllowOnlyEnterKey = true;
            PrinterCfg.Bold = 1;
            PrinterCfg.FontUp = 208;
            PrinterCfg.FontDown = 207;
            PrinterCfg.SeparationSymbols = 32;
            
            PrinterCfg.Printer = new PrinterConfiguration();
            PrinterCfg.Printer.Name = "Impresora1";
            PrinterCfg.Printer.ConfigFile = "Imaje1.cfg";
            PrinterCfg.Printer.Use9040Structure = true;
            PrinterCfg.Printer.SpeedName = "325";
            PrinterCfg.Printer.SpeedValue = "325";
            PrinterCfg.Printer.FontName = "Chica";
            PrinterCfg.Printer.FontValue = "206";
            PrinterCfg.Printer.APISymbol = "bcd";
            printerController = new PrinterController(PrinterCfg);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            printerController.SendPrintCommand(textBox1.Text);
        }
    }
}
