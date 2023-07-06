using System;
using System.Globalization;
using System.Text;
//using Tenaris.AutoAr.Facu.Manager.Marking.Shared.Model;
//using Tenaris.AutoAr.Facu.Manager.Marking.Shared.PrinterConfiguration;
//using Tenaris.Library.Log;

namespace ImajeTester
{
    public class Tools
    {
        /// <summary>
        /// Metodo para obtener el mensaje a imprimir
        /// </summary>
        /// <returns>
        /// Mensaje procesdo.
        /// </returns>
        public static string ParseMask(string mask, Balance balanceAbierto, Font font, string leftSymbolText, string rightSymbolText)
        {
            if (mask == null)
            {
                return string.Empty;
            }

            var message = mask;
            var now = DateTime.Now;
            var calendar = CultureInfo.InvariantCulture.Calendar;
            var trimestre = (int)((calendar.GetMonth(now) - 1) / 3) + 1;

            message = message.Replace("MM.YY", now.ToString("MM.yy", DateTimeFormatInfo.InvariantInfo));
            message = message.Replace("YY.MM", now.ToString("yy.MM", DateTimeFormatInfo.InvariantInfo));

            message = message.Replace("MM/YY", now.ToString("MM/yy", DateTimeFormatInfo.InvariantInfo));
            message = message.Replace("YY/MM", now.ToString("yy/MM", DateTimeFormatInfo.InvariantInfo));

            message = message.Replace("MM-YY", now.ToString("MM-yy", DateTimeFormatInfo.InvariantInfo));
            message = message.Replace("YY-MM", now.ToString("yy-MM", DateTimeFormatInfo.InvariantInfo));

            //API 5CRA
            message = message.Replace("YMM", now.ToString("yMM", DateTimeFormatInfo.InvariantInfo));

            message = message.Replace("HH.MM", now.ToString("HH:mm", DateTimeFormatInfo.InvariantInfo));

            message = message.Replace("Y/T", now.Year.ToString().Substring(3, 1) + "/" + trimestre);
            message = message.Replace("@", string.Format("{0}", font != null ? font.APISymbol : string.Empty));
            message = message.Replace("$AYB$", string.Empty);
            message = message.Replace("#A+B", string.Empty);
            message = message.Replace("#A", string.Empty);
            message = message.Replace("#B", string.Empty);

            if (balanceAbierto != null)
            {
               
               message = message.Replace("HNXXXXX", "HN " + balanceAbierto.Colada);
               message = message.Replace("CCCCCC", balanceAbierto.Ciclo.ToString());

                if (balanceAbierto.Lote > 0)
                {
                    message = message.Replace("LXXXXX", "L" + balanceAbierto.Lote);
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                if (!string.IsNullOrEmpty(leftSymbolText) || !string.IsNullOrEmpty(rightSymbolText))
                {
                    message = ParseSymbolsInMessage(message, leftSymbolText, rightSymbolText);
                }
                else
                {
                    message = JustifyTwoLines(message);
                }
            }

            return message;
        }

        /// <summary>
        /// Agrega los Símbolos de los Extremos y Centra el Mensaje en Relación 35 / 65
        /// </summary>
        /// <param name="message">Mensaje sin Símbolos</param>
        /// <param name="leftSymbolText">Símbolo Extremo Izquierdo</param>
        /// <param name="rightSymbolText">Símbolo Extremo Derecho</param>
        /// <returns>Devuelve el mensahe con los extremos y centrados.</returns>
        public static string ParseSymbolsInMessage(string message, string leftSymbolText, string rightSymbolText)
        {
            //var appConfig = (AppConfiguration)ConfigurationManager.GetSection("appConfiguration");

            var messageMaxLength = 32;  

            var minSeparaionSpaces = 4; // Separación mínima entre mensaje y símbolos de los extremos.  4                     
            var defaultLeftSpaces = 12 - leftSymbolText.Length * 2; // 10 + 2 - (Symbol * 2)            12
            var defaultRightSpaces = 22 - rightSymbolText.Length * 2; // 20 + 2 - (Symbol * 2)          22
            //Trace.Debug("ParseSymbolsInMessage - leftSymbolText.Length:{0}, defaultLeftSpaces:{1}", leftSymbolText.Length, defaultLeftSpaces);
            //Trace.Debug("ParseSymbolsInMessage - rightSymbolText.Length:{0}, defaultRightSpaces:{1}", rightSymbolText.Length, defaultRightSpaces);
           
            var leftMaxLength = (messageMaxLength / 2) + defaultLeftSpaces;
            var rightMaxLength = (messageMaxLength / 2) + defaultRightSpaces;
            //Trace.Debug("ParseSymbolsInMessage - leftMaxLength:{0}", leftMaxLength);
            //Trace.Debug("ParseSymbolsInMessage - rightMaxLength:{0}", rightMaxLength);

            var firstLineMessage = message.IndexOf(Environment.NewLine) > 0 ? message.Substring(0, message.IndexOf(Environment.NewLine)).Trim() : message.Trim();

            var secondLineMessage = string.Empty;

            if (message.IndexOf(Environment.NewLine) > 0)
            {
                secondLineMessage = message.Remove(0, message.IndexOf(Environment.NewLine)).Replace(Environment.NewLine, string.Empty).Trim();
            }

            var result = new StringBuilder(firstLineMessage);

            int leftSpaces = 0;
            var rightSpaces = 0;

            var messageLength = firstLineMessage.Length;

            // Símbolos en ambos extremos
            if (!string.IsNullOrEmpty(leftSymbolText) && !string.IsNullOrEmpty(rightSymbolText))
            {
                //Trace.Debug("ParseSymbolsInMessage - leftSymbolText:[{0}], rightSymbolText:[{1}]", leftSymbolText, rightSymbolText);

                // Longitud normal
                if (messageLength <= messageMaxLength)
                {
                    decimal percentMessageLeftLenth = (messageMaxLength - messageLength) * 0.35m;   //.35m
                    leftSpaces = leftMaxLength - (messageMaxLength / 2) + (int)Math.Floor(percentMessageLeftLenth);
                    //Trace.Debug("ParseSymbolsInMessage - Longitud normal - leftSpaces:{0}", leftSpaces);

                    decimal percentMessageRightLenth = (messageMaxLength - messageLength) * 0.65m;  //.65m
                    rightSpaces = rightMaxLength - (messageMaxLength / 2) + (int)Math.Ceiling(percentMessageRightLenth);
                    //Trace.Debug("ParseSymbolsInMessage - Longitud normal - rightSpaces:{0}", rightSpaces);
                } // Longitud excedida
                else if (messageLength > messageMaxLength + defaultLeftSpaces + defaultRightSpaces - leftSymbolText.Length * 2 - rightSymbolText.Length * 2 - minSeparaionSpaces * 2)
                {
                    rightSpaces = leftSpaces = minSeparaionSpaces;
                } // Longitud ajustada
                else
                {
                    decimal percentMessageLeftLenth = (messageLength - messageMaxLength) * 0.35m;
                    leftSpaces = leftMaxLength - (messageMaxLength / 2) - (int)Math.Floor(percentMessageLeftLenth);
                    //Trace.Debug("ParseSymbolsInMessage - Longitud ajustada - leftSpaces:{0}", leftSpaces);

                    decimal percentMessageRightLenth = (messageLength - messageMaxLength) * 0.65m;
                    rightSpaces = rightMaxLength - (messageMaxLength / 2) - (int)Math.Ceiling(percentMessageRightLenth);
                    //Trace.Debug("ParseSymbolsInMessage - Longitud ajustada - rightSpaces:{0}", rightSpaces);
                }
                //Trace.Debug("ParseSymbolsInMessage - leftSpaces:[{0}], rightSpaces:[{1}]", leftSpaces, rightSpaces);
            } // Sólo simbolo izquierdo
            else if (!string.IsNullOrEmpty(leftSymbolText))
            {
                leftSpaces = defaultLeftSpaces;
                rightSpaces = 0;
            } // Sólo símbolo derecho
            else if (!string.IsNullOrEmpty(rightSymbolText))
            {
                leftSpaces = 0;
                defaultRightSpaces = (defaultRightSpaces + defaultLeftSpaces) - 1;

                if (messageLength <= messageMaxLength)
                {
                    rightSpaces = defaultRightSpaces + (messageMaxLength - messageLength);
                }
                else if (messageLength > messageMaxLength + defaultRightSpaces - rightSymbolText.Length * 2 - minSeparaionSpaces)
                {
                    rightSpaces = minSeparaionSpaces;
                }
                else
                {
                    rightSpaces = defaultRightSpaces - (messageLength - messageMaxLength);
                }
            }
            else
            {
                rightSpaces = leftSpaces = 0;
            }
            //Trace.Debug("ParseSymbolsInMessage - Construcción de la primera línea");
            //Trace.Debug("ParseSymbolsInMessage - leftSpaces:{0} - rightSpaces:{1} ", leftSpaces, rightSpaces);
            result.Insert(0, !string.IsNullOrEmpty(leftSymbolText) ? leftSymbolText + new string(' ', leftSpaces) : string.Empty);           
            //Trace.Debug("ParseSymbolsInMessage - primera linea:[{0}] - longitud parcial:{1}  ", result.ToString(), result.ToString().Length);
            var longParcial1 = result.ToString().Length;
            result.Append(!string.IsNullOrEmpty(rightSymbolText) ? new string(' ', rightSpaces/2) + rightSymbolText + new string(' ', rightSpaces / 2) : string.Empty);
            //Trace.Debug("ParseSymbolsInMessage - primera linea:[{0}] - longitud total:{1}  ", result.ToString(), result.ToString().Length);

            // Construcción de la seguanda línea en base a la primera
            //Trace.Debug("ParseSymbolsInMessage - Construcción de la seguanda línea en base a la primera");
            if (!string.IsNullOrEmpty(secondLineMessage))
            {
                var secondLineBuilder = new StringBuilder(secondLineMessage);

                var indexFirstLine = result.ToString().IndexOf(firstLineMessage);
                var lengthFirstLine = result.ToString().Length;
                //Trace.Debug("ParseSymbolsInMessage - indexFirstLine:{0} - lengthFirstLine:{1}", indexFirstLine, lengthFirstLine);

                leftSpaces = indexFirstLine - leftSymbolText.Length;
                //Trace.Debug("ParseSymbolsInMessage - leftSpaces:{0} ", leftSpaces);

                secondLineBuilder.Insert(0, !string.IsNullOrEmpty(leftSymbolText) ? leftSymbolText + new string(' ', leftSpaces) : string.Empty);
                //Trace.Debug("ParseSymbolsInMessage - segunda linea:[{0}] - longitud parcial:{1}  ", secondLineBuilder.ToString(), secondLineBuilder.ToString().Length);
                var longParcial2 = secondLineBuilder.ToString().Length;
                if (longParcial2 < longParcial1)
                {
                    //Trace.Debug(" segunda linea ms corta que la primerra, completo");
                    var delta = longParcial1 - longParcial2;
                    secondLineBuilder.Append(new string(' ', delta));
                    rightSpaces = lengthFirstLine - secondLineBuilder.ToString().Length - rightSymbolText.Length;
                }
                else if (longParcial1 < longParcial2)
                {
                    //Trace.Debug(" primerra  linea ms corta que la segunda .... ");
                    var delta = longParcial2 - longParcial1;
                    rightSpaces = lengthFirstLine - secondLineBuilder.ToString().Length - rightSymbolText.Length - delta;
                }                
                //Trace.Debug("ParseSymbolsInMessage - rightSpaces:{0} ", rightSpaces);

                secondLineBuilder.Append(!string.IsNullOrEmpty(rightSymbolText) && rightSpaces > 0 ? new string(' ', rightSpaces/2) + rightSymbolText + new string(' ', rightSpaces/2) : string.Empty);
                //Trace.Debug("ParseSymbolsInMessage - segunda linea:[{0}] - longitud total:{1}  ", secondLineBuilder.ToString(), secondLineBuilder.ToString().Length);

                result.Append(Environment.NewLine);

                result.Append(secondLineBuilder);
            }
            //Trace.Debug("result:[{0}]",result.ToString());
            return result.ToString();
        }

        public static string RemoveSymbols(string message, string leftSymbolText, string rightSymbolText)
        {
            var result = message;

            if (!string.IsNullOrEmpty(leftSymbolText))
            {
                result = result.Remove(result.IndexOf(leftSymbolText), leftSymbolText.Length);

                var newLineIndex = message.IndexOf(Environment.NewLine);

                if (newLineIndex > -1)
                {
                    result = result.Remove(result.IndexOf(leftSymbolText, newLineIndex), leftSymbolText.Length);
                }
            }

            if (!string.IsNullOrEmpty(rightSymbolText))
            {
                result = result.Remove(result.LastIndexOf(rightSymbolText), rightSymbolText.Length);

                var newLineIndex = message.IndexOf(Environment.NewLine);

                if (newLineIndex > -1)
                {
                    var lastIndex = result.Substring(0, newLineIndex).LastIndexOf(rightSymbolText);

                    result = result.Remove(lastIndex, rightSymbolText.Length);
                }
            }

            return result;
        }

        public static string JustifyTwoLines(string message)
        {
            if (message.IndexOf(Environment.NewLine) > 0)
            {
                var firstLineMessage = message.Substring(0, message.IndexOf(Environment.NewLine)).Trim();
                var secondLineMessage = message.Remove(0, message.IndexOf(Environment.NewLine)).Replace(Environment.NewLine, string.Empty).Trim();

                var result = new StringBuilder(firstLineMessage);

                var firstLineLength = firstLineMessage.Length;
                var secondLineLength = secondLineMessage.Length;

                if (firstLineLength > secondLineLength)
                {
                    result.Append(Environment.NewLine);
                    result.Append(secondLineMessage);
                    result.Append(new string(' ', firstLineLength - secondLineLength));
                }
                else if (firstLineLength < secondLineLength)
                {
                    result.Append(new string(' ', secondLineLength - firstLineLength));
                    result.Append(Environment.NewLine);
                    result.Append(secondLineMessage);
                }
                else
                {
                    result.Append(Environment.NewLine);
                    result.Append(secondLineMessage);
                }

                return result.ToString();
            }
            else
            {
                return message;
            }
        }
    }
}
