using System;
using System.Collections.Generic;
using System.Text;


namespace Client
{


    public class ImajeS8DriverConfiguration
    {
        public const int cMessageDirectionNormal                    = 0;
        public const int cMessageDirectionReverse                   = 1;

        public const int cHorizontalDirectionNormal                 = 0;
        public const int cHorizontalDirectionReverse                = 1;
        
        public const int cVerticalDirectionNormal                   = 0;
        public const int cVerticalDirectionReverse                  = 1;
        
        public const int cTachoModeNo                               = 0;
        public const int cTachoModeYes                              = 1;
        
        public const int cManualTriggerNo                           = 0;
        public const int cManualTriggerYes                          = 1;
        
        public const int cTriggerModeObject                         = 0;
        public const int cTriggerModeRepetitive                     = 1;
        
        public const int cMarginUnitMilimeters                      = 0;
        public const int cMarginUnitFrameht                         = 1;
        
        public const int cDINModeNo                                 = 0;
        public const int cDINModeYes                                = 1;

        /*
         * 1 = Jet #1
         * 2 = Jet #2
         */
        public int ImajeHead
        {
            get { return 1; }
            set { }
        }

        public int MessageDirection
        {
            get { return cMessageDirectionNormal; }
            set { }
        }

        public int HorizontalDirection
        {
            get { return cHorizontalDirectionNormal; }
            set { }
        }

        public int VerticalDirection
        {
            get { return cVerticalDirectionNormal; }
            set { }
        }

        public int TachoMode
        {
            get { return cTachoModeYes; }
            set { }
        }

        public int ManualTrigger
        {
            get { return cManualTriggerNo; }
            set { }
        }

        public int TriggerMode
        {
            get { return cTriggerModeObject; }
            set { }
        }
        public int MarginUnit
        {
            get { return cMarginUnitMilimeters; }
            set { }
        }
        public int DINMode
        {
            get { return cDINModeNo; }
            set { }
        }

        public int MultiTopValue
        {
            get { return 0; } // 0 .. 255
            set { }
        }
        public int ObjectTopFilter
        {
            get { return 1; } // 01 to 10
            set { }
        }

        public int TachoDivision
        {
            get { return 5; } // 001 to 255
            set { }
        }

        public int ForwardMargin
        {
            get { return 16; } // 0001 to 9999 mm
            set { }
        }

        public int ReturnMargin
        {
            get { return 3; } // 0001 to 9999 mm
            set { }
        }

        public int Interval
        {
            get { return 2; } // 0001 to 9999 mm
            set { }
        }
        public int PrintingSpeed
        {
            get { return 256; } // 0001 to 9999 mm/s
            set { }
        }

        public int AlgorithmNumber
        {
            get { return 0; } // see list of fonts
            set { }
        }
    }
}
