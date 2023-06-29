using System;
using System.Text;

namespace Client
{
    class Program
    {

        static ImajeS8DriverConfiguration _config = new ImajeS8DriverConfiguration();
        public enum ASCII : byte
        {
            NULL = 0x00, SOH = 0x01, STX = 0x02, ETX = 0x03, EOT = 0x04, ENQ = 0x05, ACK = 0x06, BELL = 0x07,
            BS = 0x08, HT = 0x09, LF = 0x0A, VT = 0x0B, FF = 0x0C, CR = 0x0D, SO = 0x0E, SI = 0x0F, DC1 = 0x11,
            DC2 = 0x12, DC3 = 0x13, DC4 = 0x14, NAK = 0x15, SYN = 0x16, ETB = 0x17, CAN = 0x18, EM = 0x19,
            SUB = 0x1A, ESC = 0x1B, FS = 0x1C, GS = 0x1D, RS = 0x1E, US = 0x1F, SP = 0x20, DEL = 0x7F
        }

        public enum ImajeS8Command : byte
        {
            InitializationENQ = 0x01,
            SendCompleteMessage = 0X02
        }
        static void Main(string[] args)
        {
            Client c = new Client("localhost", 6400);
            string msg;
            byte[] printMessage;
            byte[] telegram;

            c.Start();
            while (true)
            {
                Console.Write(">>> ");
                msg = Console.ReadLine();
                printMessage = Encoding.ASCII.GetBytes(msg);
                telegram = BuildTelegram(ImajeS8Command.SendCompleteMessage, printMessage);
                c.Send(telegram);
            }
        }

        static byte[] BuildTelegram(ImajeS8Command command, byte[] printMessage)
        {
            switch (command)
            {
                case ImajeS8Command.SendCompleteMessage:
                    return SendCompleteMessage(printMessage);

            }
            return null;
        }

        static byte[] SendCompleteMessage(byte[] printMessage)
        {
            byte[] telegram = new byte[5000]; //telegram
            int a = 0;
            int msgLen = 0;
            int value = 0;

            // identifier ( 1 byte )
            telegram[a++] = (byte)0x57;

            // lenght is at the end

            // head (1 or 2 )
            telegram[a++] = (byte)_config.ImajeHead;

            // structure indicator (2 bytes)
            value = 0;

            value = value | 128; // General parameters presence (bit 7)
            value = value | 64; // messageText presence (bit 6)
            telegram[a++] = (byte)value;
            telegram[a++] = (byte)0;

            // General parameters ( 14 bytes )
            /// byte 1. On off parameters
            value = 0;
            if (_config.MessageDirection == ImajeS8DriverConfiguration.cMessageDirectionReverse)
            {
                value = value | 128;
            }
            if (_config.HorizontalDirection == ImajeS8DriverConfiguration.cHorizontalDirectionReverse)
            {
                value = value | 64;
            }
            if (_config.VerticalDirection == ImajeS8DriverConfiguration.cVerticalDirectionReverse)
            {
                value = value | 32;
            }
            if (_config.TachoMode == ImajeS8DriverConfiguration.cTachoModeYes)
            {
                value = value | 16;
            }
            if (_config.ManualTrigger == ImajeS8DriverConfiguration.cManualTriggerYes)
            {
                value = value | 8;
            }
            if (_config.TriggerMode == ImajeS8DriverConfiguration.cTriggerModeRepetitive)
            {
                value = value | 4;
            }
            if (_config.MarginUnit == ImajeS8DriverConfiguration.cMarginUnitFrameht)
            {
                value = value | 2;
            }
            if (_config.DINMode == ImajeS8DriverConfiguration.cDINModeYes)
            {
                value = value | 1;
            }
            telegram[a++] = (byte)value;

            /// byte 2. Multi Top value
            telegram[a++] = (byte)_config.MultiTopValue;

            /// byte 3. Object top filter
            telegram[a++] = (byte)_config.ObjectTopFilter;

            /// byte 4. Tacho division
            telegram[a++] = (byte)_config.TachoDivision;

            /// byte 5 & 6. Forward margin
            telegram[a++] = (byte)(_config.ForwardMargin >> 8);
            telegram[a++] = (byte)_config.ForwardMargin;

            /// byte 7 & 8. Return margin
            telegram[a++] = (byte)(_config.ReturnMargin >> 8);
            telegram[a++] = (byte)_config.ReturnMargin;

            /// byte 9 & 10. Interval
            telegram[a++] = (byte)(_config.Interval >> 8);
            telegram[a++] = (byte)_config.Interval;

            /// byte 11 & 12. PrintingSpeed
            telegram[a++] = (byte)(_config.PrintingSpeed >> 8);
            telegram[a++] = (byte)_config.PrintingSpeed;

            /// byte 13 & 14. Algorithm number
            telegram[a++] = (byte)(_config.AlgorithmNumber >> 8);
            telegram[a++] = (byte)_config.AlgorithmNumber;

            // First line identifier
            telegram[a++] = (byte)ASCII.LF;

            // position of first block
            telegram[a++] = (byte)128;
            telegram[a++] = (byte)1;

            // character generator 56
            telegram[a++] = (byte)56;

            // expansion 1
            telegram[a++] = (byte)1;

            // text delimiter
            telegram[a++] = (byte)16;

            for (int i = 0; i < printMessage.Length; i++)
                telegram[a++] = printMessage[i];

            // text delimiter
            telegram[a++] = (byte)16;

            // End of message delimiter
            telegram[a++] = (byte)13;

            // CheckSum
            telegram[a++] = (byte)CalcChecksum(telegram);


            // data block length ( 2 bytes ) len - ID - self - checksum
            msgLen = a - 4;
            telegram[1] = (byte)(msgLen >> 8);
            telegram[2] = (byte)(msgLen);
            return telegram;
        }

        // TODO
        static int CalcChecksum(byte[] data)
        {
            int result = 0;
            for (int i = 0; i < data.Length; i++)
                result = result ^ data[i];
            return result;
        }
    }
}
