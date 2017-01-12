using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvantechDebug
{
    class GlobalVariables
    {
        private GlobalVariables()
        {

        }
        

    }
    public static class GlobalVar
    {
     
        public static int StringNumberValue
        {
            set; get;
        } = CanSettings.Default.StringNumbers;

        public static int BpNumberValue
        {
            set; get;
        } = CanSettings.Default.BpNumbers;

        public static int CellNumberValue
        {
            set; get;
        } = CanSettings.Default.CellNumbers;
        public static int SelectedStringNumberValue
        {
            set;get;
        }
        public static int SelectedBpNumberValue
        {
            set;get;
        }
        public static string CanPort
        {
            set; get;
        } = CanSettings.Default.CANPort;

        public static string CanBaudrate
        {
            set; get;
        } = CanSettings.Default.CANBaudrate;

        public static string CanAcceptanceFilterMode
        {
            set; get;
        } = CanSettings.Default.CanAcceptanceFilterMode;

        public static string CanAcceptanceFilterMask
        {
            set; get;
        } = CanSettings.Default.CanAcceptanceMask;

        public static string CanAcceptanceCode
        {
            set; get;
        } = CanSettings.Default.CanAcceptanceCode;

        public static string CanWriteTimeOut
        {
            set;get;
        }

        public static string CanReadTimeOut
        {
            set;get;
        }

        public static uint MaxMsgCount
        {
            set; get;
        } = Convert.ToUInt32(CanSettings.Default.CanMaxMsgCount);

        public static bool CanReceiveThreadFlag
        {
            set; get;
        } = false;


    }

    public class VolTemp
    {
        public  string Vol { set; get; }
        public  string Temp { set; get; }
    }





}
