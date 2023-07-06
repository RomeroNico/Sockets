using System;
//using Tenaris.AutoAr.Facu.Manager.Marking.S8ImageDummy;
//using Tenaris.AutoAr.Facu.Manager.Marking.Shared;
//using Tenaris.AutoAr.Facu.Manager.Marking.Shared.Configuration;
//using Tenaris.AutoAr.Facu.Manager.Marking.Shared.PrinterConfiguration;
using Tenaris.AutoAr.Library.DeviceAccess.Layers.Interop;
//using Tenaris.Library.Log;
using Tenaris.Service.Layer.ImajeS8.Interop;

namespace ImajeTester
{
    public class PrinterController : IDisposable
    {
        private readonly PrinterControllerConfiguration configuration;
                     
        private IS8Imaje printer;
        private IChain chain;
        
        private string printerConfig = string.Empty;
        
        public PrinterController(PrinterControllerConfiguration config)
        {
            this.configuration = config;

            SelectedFont = new Font{ APISymbol =config.Printer.APISymbol,  Value =config.Printer.FontValue,    Name = config.Printer.FontName };
            SelectedSpeed = new Speed { Name=config.Printer.SpeedName, Value=Convert.ToInt32(config.Printer.SpeedValue)};
            SelectedPrinter = new Printer { Name =config.Printer.Name, ConfigFile =config.Printer.ConfigFile, Use9040Structure = config.Printer.Use9040Structure };
        }

        public Printer SelectedPrinter { get;  set; }
        public Speed SelectedSpeed { get;  set; }
        public Font SelectedFont { get;  set; }
        public string LeftSymbolText { get;  set; }
        public string RightSymbolText { get;  set; }
        public string Message { get; private set; } //Mensaje a la DB

        public void Dispose()
        {

        }


        public PrintStatus SendPrintCommand(string message)
        {
            PrintStatus result;
            try
            {

                if (this.SelectedPrinter == null)
                {
                    result = PrintStatus.NoPrinterSelected; 
                    //Trace.Error("No ha seleccionado Impresora.");
                    return result;
                }

                if (this.SelectedSpeed == null)
                {
                    result = PrintStatus.NoSpeedSelected;
                    //Trace.Error("No hay seleccionada una velocidad de impresión.");
                    return result;
                }

            
                if (!this.ConnectToPrinter())
                {
                    result = PrintStatus.ConnectionError;
                    //Trace.Error("Error al conectar con Impresora.");
                    return result;
                }
           
                CharacterGeneratorsEnum font;
                Enum.TryParse(this.SelectedFont.Value, out font);               

                //Trace.Message("Send Message: {0}, Font: {1}", message, font);

                var msg = this.printer.CreateMessage();

                var messageWithOutSymbols = Tools.RemoveSymbols(message, this.LeftSymbolText, this.RightSymbolText);

                var firstLineMessage = messageWithOutSymbols.IndexOf(Environment.NewLine) > 0 ? messageWithOutSymbols.Substring(0, messageWithOutSymbols.IndexOf(Environment.NewLine)) : messageWithOutSymbols;

                var secondLineMessage = string.Empty;

                if (message.IndexOf(Environment.NewLine) > 0)
                {
                    secondLineMessage = messageWithOutSymbols.Remove(0, messageWithOutSymbols.IndexOf(Environment.NewLine)).Replace(Environment.NewLine, string.Empty);
                }

                var firstLine = msg.Lines.AddLine();

                CharacterGeneratorsEnum fontUp;
                Enum.TryParse(this.configuration.FontUp.ToString(), out fontUp);

                var hasLeftSymbol = !string.IsNullOrEmpty(this.LeftSymbolText);
                var hasRightSymbol = !string.IsNullOrEmpty(this.RightSymbolText);

                // Texto Símbolo Izquierdo Arriba
                if (hasLeftSymbol)
                {
                    var leftSymbolUp = firstLine.Blocks.Add();
                    leftSymbolUp.Position = Convert.ToInt32(configuration.PosLine1);
                    leftSymbolUp.CharacterGenerator = fontUp;
                    leftSymbolUp.Bolderization = configuration.Bold;
                    leftSymbolUp.AddText(this.LeftSymbolText);

                    //Trace.Message("Send Message with Left Symbol Up: {0} , Font: {1}", this.LeftSymbolText, fontUp);
                }

                var firstBlock = firstLine.Blocks.Add();
                firstBlock.Position = Convert.ToInt32(configuration.PosLine1);
                firstBlock.CharacterGenerator = font;
                firstBlock.Bolderization = this.configuration.Bold;
                firstBlock.AddText(firstLineMessage);

                // Texto Símbolo Derecho Arriba
                if (hasRightSymbol)
                {
                    var rightSymbolUp = firstLine.Blocks.Add();
                    rightSymbolUp.Position = Convert.ToInt32(configuration.PosLine1);
                    rightSymbolUp.CharacterGenerator = fontUp;
                    rightSymbolUp.Bolderization = configuration.Bold;
                    rightSymbolUp.AddText(this.RightSymbolText);

                    //Trace.Message("Send Message with Right Symbol Up: {0} , Font: {1}", this.RightSymbolText, fontUp);
                }

                if (configuration.AllowTwoLines && !string.IsNullOrEmpty(secondLineMessage))
                {
                    var secondLine = msg.Lines.AddLine();

                    CharacterGeneratorsEnum fontDown;
                    Enum.TryParse(configuration.FontDown.ToString(), out fontDown);

                    // Texto Símbolo Izquierdo Abajo
                    if (hasLeftSymbol)
                    {
                        var leftSymbolDown = secondLine.Blocks.Add();
                        leftSymbolDown.Position = Convert.ToInt32(configuration.PosLine2);
                        leftSymbolDown.CharacterGenerator = fontDown;
                        leftSymbolDown.Bolderization = configuration.Bold;
                        leftSymbolDown.AddText(this.LeftSymbolText);

                        //Trace.Message("Send Message with Left Symbol Down: {0} , Font: {1}", this.LeftSymbolText, fontDown);
                    }

                    var secondBlock = secondLine.Blocks.Add();
                    secondBlock.Position = Convert.ToInt32(configuration.PosLine2);
                    secondBlock.CharacterGenerator = font;
                    secondBlock.Bolderization = this.configuration.Bold;
                    secondBlock.AddText(secondLineMessage);

                    // Texto Símbolo Derecho Abajo
                    if (hasRightSymbol)
                    {
                        var rightSymbolDown = secondLine.Blocks.Add();
                        rightSymbolDown.Position = Convert.ToInt32(configuration.PosLine2);
                        rightSymbolDown.CharacterGenerator = fontDown;
                        rightSymbolDown.Bolderization = this.configuration.Bold;
                        rightSymbolDown.AddText(this.RightSymbolText);

                        //Trace.Message("Send Message with Right Symbol Down: {0} , Font: {1}", this.RightSymbolText, fontDown);
                    }
                }

                msg.Parameters.Use9040Structure = this.SelectedPrinter.Use9040Structure;
                msg.Parameters.PrintingSpeed = this.SelectedSpeed.Value;

                printer.SendCompleteMessage(Convert.ToInt32(configuration.JetNumber), msg);
                result = PrintStatus.Ok;
                //Trace.Message("Send Message: OK");              
                
            }
            catch (Exception ex)
            {
                //Trace.Error("SendPrintCommand - Error: {0}", ex.Message);
                return PrintStatus.GeneralException;
            }

            return result;
        }

        private bool ConnectToPrinter()
        {
            try
            {
                if (this.printerConfig != this.SelectedPrinter.ConfigFile)
                {
                    //Trace.Debug("Verificando Conexion a Impresora: {0}", this.SelectedPrinter.ConfigFile);
                    this.UninitializeImaje();

                    
                    this.chain = new Chain();

                    this.chain.LoadFrom(this.SelectedPrinter.ConfigFile);
                    this.chain.Activate();
                    this.printer = (IS8Imaje)this.chain.Upper;

                    this.printerConfig = this.SelectedPrinter.ConfigFile;
                }

                return true;
            }
            catch (Exception ex)
            {
                //Trace.Error("ConnectToPrinter> Error: {0}", ex.Message);
            }

            return false;
        }
        public void UninitializeImaje()
        {
            //if (this.chain != null)
            //{
            //    this.chain.Deactivate();
            //    if (this.printer != null)
            //    {
            //        Marshal.ReleaseComObject(this.printer);
            //    }

            //    this.printer = null;
            //    this.chain = null;
            //    this.printerConfig = string.Empty;
            //}
        }
    }
}
