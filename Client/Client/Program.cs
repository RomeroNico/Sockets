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
            SendCompleteMessage = 0X02,
            ResetFaults = 0x03
        }
        static void Main(string[] args)
        {
            Client c = new Client(args[0], Int32.Parse(args[1]));
            string msg;
            byte[] printMessage;
            byte[] telegram;

            c.Start();
            Console.Write("Send telegram ");
            msg = args[2];
            printMessage = Encoding.ASCII.GetBytes(msg);
            telegram = BuildTelegram(ImajeS8Command.SendCompleteMessage, printMessage);
            c.Send(telegram);
            Console.Write("Send telegram finish ");
            Console.Write("Wait for an answer");
            c.WaitForAnswer(7000); // wait 7 seconds
            Console.Write("Wait for an answer finished");
            c.Close();
        }

        static byte[] BuildTelegram(ImajeS8Command command, byte[] printMessage)
        {
            switch (command)
            {
                case ImajeS8Command.InitializationENQ:
                    return InitializationENQ();
                case ImajeS8Command.ResetFaults:
                    return ResetFaults();
                case ImajeS8Command.SendCompleteMessage:
                    return SendCompleteMessage(printMessage);

            }
            return null;
        }

        static byte[] ResetFaults()
        {
            byte[] telegram = new byte[5000]; //telegram
            int a = 0;

            //TODO Completar telegram[a++]

            //Identifier
            telegram[a++] = (byte)0x3C;

            //Length
            telegram[a++] = (byte)0x00;
            telegram[a++] = (byte)0x00;

            //Checksum
            telegram[a++] = (byte)0x3C;



            byte[] finalTelegram = new byte[a];
            for (int i = 0; i < a; i++)
            {
                finalTelegram[i] = telegram[i];
            }
            return finalTelegram;
        }

        static byte[] InitializationENQ() {
            byte[] telegram = new byte[5000]; //telegram
            int a = 0;

            //TODO Completar telegram[a++]

            //ENQ
            telegram[a++] = (byte)0x05;

            byte[] finalTelegram = new byte[a];
            for (int i = 0; i < a; i++)
            {
                finalTelegram[i] = telegram[i];
            }
            return finalTelegram;
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



            // symbol generator 56

            telegram[a++] = (byte)56;



            // bolderization 1

            telegram[a++] = (byte)1;



            // text delimiter

            telegram[a++] = (byte)16;



            for (int i = 0; i < printMessage.Length; i++)

                telegram[a++] = printMessage[i];



            // text delimiter

            telegram[a++] = (byte)16;



            // bolderization 1

            telegram[a++] = (byte)1;



            // symbol generator 56

            telegram[a++] = (byte)56;



            // position of first block

            telegram[a++] = (byte)128;

            telegram[a++] = (byte)1;



            // End of message delimiter

            telegram[a++] = (byte)13;



            // CheckSum

            telegram[a++] = (byte)CalcChecksum(telegram);





            // data block length ( 2 bytes ) len - ID - self - checksum

            msgLen = a - 4;

            telegram[1] = (byte)(msgLen >> 8);

            telegram[2] = (byte)(msgLen);


            byte[] finalTelegram = new byte[a];
            for (int i = 0; i < a; i++)
            {
                finalTelegram[i] = telegram[i];
            }
            return finalTelegram;

        }


        //static byte[] SendCompleteMessage(byte[] printMessage)
        //{
        //    byte[] telegram = new byte[5000]; //telegram
        //    int a = 0;

        //    //Example msg

        //    //Identifier
        //    telegram[a++] = (byte)0x57;

        //    //Length
        //    telegram[a++] = (byte)0x00;
        //    telegram[a++] = (byte)0x63;

        //    //Head 1
        //    telegram[a++] = (byte)0x01;

        //    //Data.StructureIndicator
        //    telegram[a++] = (byte)0xC0;
        //    telegram[a++] = (byte)0x00;

        //    //Data.GeneralParameters
        //    telegram[a++] = (byte)0x10;

        //    //Data.MultitopTrigger
        //    telegram[a++] = (byte)0x00;

        //    //Data.ObjectTopFilter
        //    telegram[a++] = (byte)0x01;

        //    //Data.TachoDivision
        //    telegram[a++] = (byte)0x05;

        //    //Data.ForwardMargin
        //    telegram[a++] = (byte)0x00;
        //    telegram[a++] = (byte)0x10;

        //    //Data.ReturnMargin
        //    telegram[a++] = (byte)0x00;
        //    telegram[a++] = (byte)0x03;

        //    //Data.Interval
        //    telegram[a++] = (byte)0x00;
        //    telegram[a++] = (byte)0x02;

        //    //Data.PrintingSpeed
        //    telegram[a++] = (byte)0x01;
        //    telegram[a++] = (byte)0x00;

        //    //Data.Reserved
        //    telegram[a++] = (byte)0x00;
        //    telegram[a++] = (byte)0x00;

        //    //Text.FirstLineIdentifier
        //    telegram[a++] = (byte)0x0A;

        //    //Text.PositionOfFirstBlock
        //    telegram[a++] = (byte)0x80;
        //    telegram[a++] = (byte)0x01;

        //    //Text.CharacterGenerator056
        //    telegram[a++] = (byte)0x38;

        //    //Text.Expresion1
        //    telegram[a++] = (byte)0x01;

        //    //Text.TextDelimiter
        //    telegram[a++] = (byte)0x10;

        //    //Text.P
        //    telegram[a++] = (byte)0x50;

        //    //Text.R
        //    telegram[a++] = (byte)0x52;

        //    //Text.O
        //    telegram[a++] = (byte)0x4F;

        //    //Text.D
        //    telegram[a++] = (byte)0x44;

        //    //Text.U
        //    telegram[a++] = (byte)0x55;

        //    //Text.I
        //    telegram[a++] = (byte)0x49;

        //    //Text.T
        //    telegram[a++] = (byte)0x54;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.L
        //    telegram[a++] = (byte)0x4C;

        //    //Text.E
        //    telegram[a++] = (byte)0x45;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.AutodatingDelimiter
        //    telegram[a++] = (byte)0x1A;

        //    //Text.DayOfMonth(30)
        //    telegram[a++] = (byte)0x49;
        //    telegram[a++] = (byte)0x4A;

        //    //Text.Separator
        //    telegram[a++] = (byte)0x6E;

        //    //Text.Month(09)
        //    telegram[a++] = (byte)0x50;
        //    telegram[a++] = (byte)0x51;

        //    //Text.Separator
        //    telegram[a++] = (byte)0x6E;

        //    //Text.Year(00)
        //    telegram[a++] = (byte)0x55;
        //    telegram[a++] = (byte)0x56;

        //    //Text.AutodatingDelimiter
        //    telegram[a++] = (byte)0x1A;

        //    //Text.Delimiter
        //    telegram[a++] = (byte)0x10;

        //    //Text.Expresion1
        //    telegram[a++] = (byte)0x01;

        //    //Text.CharacterGenerator056
        //    telegram[a++] = (byte)0x38;

        //    //Text.PositionOfFirstBlock
        //    telegram[a++] = (byte)0x80;
        //    telegram[a++] = (byte)0x01;

        //    //Text.PositionOfSecondBlock
        //    telegram[a++] = (byte)0x80;
        //    telegram[a++] = (byte)0x01;

        //    //Text.CharacterGenerator052
        //    telegram[a++] = (byte)0x34;

        //    //Text.Expresion2
        //    telegram[a++] = (byte)0x02;

        //    //Text.TextDelimiter
        //    telegram[a++] = (byte)0x10;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.P
        //    telegram[a++] = (byte)0x50;

        //    //Text.0
        //    telegram[a++] = (byte)0x4F;

        //    //Text.I
        //    telegram[a++] = (byte)0x49;

        //    //Text.D
        //    telegram[a++] = (byte)0x44;

        //    //Text.S
        //    telegram[a++] = (byte)0x53;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.2
        //    telegram[a++] = (byte)0x32;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.K
        //    telegram[a++] = (byte)0x4B;

        //    //Text.G
        //    telegram[a++] = (byte)0x47;

        //    //Text.TextDelimiter
        //    telegram[a++] = (byte)0x10;

        //    //Text.Expresion2
        //    telegram[a++] = (byte)0x02;

        //    //Text.CharacterGenerator052
        //    telegram[a++] = (byte)0x34;

        //    //Text.PositionOfSecondBlock
        //    telegram[a++] = (byte)0x80;
        //    telegram[a++] = (byte)0x01;

        //    //Text.SecondLineIdentifier
        //    telegram[a++] = (byte)0x0A;

        //    //Text.PositionFirstBlock
        //    telegram[a++] = (byte)0x80;
        //    telegram[a++] = (byte)0x0A;

        //    //Text.CharacterGenerator052
        //    telegram[a++] = (byte)0x34;

        //    //Text.Expresion1
        //    telegram[a++] = (byte)0x01;

        //    //Text.TextDelimiter
        //    telegram[a++] = (byte)0x10;

        //    //Text.TabulationDelimiter
        //    telegram[a++] = (byte)0x1E;

        //    //Text.NumberOfFreames(240)
        //    telegram[a++] = (byte)0xF0;

        //    //Text.TabulationDelimiter
        //    telegram[a++] = (byte)0x1E;

        //    //Text.M
        //    telegram[a++] = (byte)0x4D;

        //    //Text.A
        //    telegram[a++] = (byte)0x41;

        //    //Text.D
        //    telegram[a++] = (byte)0x44;

        //    //Text.E
        //    telegram[a++] = (byte)0x45;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.I
        //    telegram[a++] = (byte)0x49;

        //    //Text.N
        //    telegram[a++] = (byte)0x4E;

        //    //Text.Space
        //    telegram[a++] = (byte)0x20;

        //    //Text.F
        //    telegram[a++] = (byte)0x46;

        //    //Text.R
        //    telegram[a++] = (byte)0x52;

        //    //Text.A
        //    telegram[a++] = (byte)0x41;

        //    //Text.N
        //    telegram[a++] = (byte)0x4E;

        //    //Text.C
        //    telegram[a++] = (byte)0x43;

        //    //Text.E
        //    telegram[a++] = (byte)0x45;

        //    //Text.TextDelimiter
        //    telegram[a++] = (byte)0x10;

        //    //Text.Expresion1
        //    telegram[a++] = (byte)0x01;

        //    //Text.CharacterGenerator052
        //    telegram[a++] = (byte)0x34;

        //    //Text.PositionOfSecondBlock
        //    telegram[a++] = (byte)0x80;
        //    telegram[a++] = (byte)0x0A;

        //    //Text.EndOfMessageDelimiter
        //    telegram[a++] = (byte)0x0D;

        //    //Text.Checksum
        //    telegram[a++] = (byte)0x0D;



        //    //End Example msg



        //    byte[] finalTelegram = new byte[a];
        //    for (int i = 0; i < a; i++)
        //    {
        //        finalTelegram[i] = telegram[i];
        //    }
        //    //Console.WriteLine("bytes {0}", a);
        //    return finalTelegram;
        //}

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
