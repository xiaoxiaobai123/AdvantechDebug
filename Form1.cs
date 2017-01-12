using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace AdvantechDebug
{
    public partial class Form1 : Form
    {
        AdvCANIO Device = new AdvCANIO();
        Thread CanReceiveThread = null; 
        ThreadStart CanReceiveThreadFunc;

 

        List<VolTemp>[] ListVolTemp = new List<VolTemp>[GlobalVar.BpNumberValue];
        BindingList<VolTemp>[] BindingListVolTemp = new BindingList<VolTemp>[GlobalVar.BpNumberValue] ;
        BindingSource[] Bsource = new BindingSource[GlobalVar.BpNumberValue] ;
        float[] cellvol = new float[GlobalVar.CellNumberValue];
        float[] celltemp = new float[GlobalVar.CellNumberValue];

        public delegate void myDelegate(String aString);
        public void ListDelegate(String aString)
        {
            ShowStatus.Items.Insert(0, aString);

        }
        public void ButtonDelegate(String aString)
        {
            CanPortControlButton.Text = aString;
        }
        public Form1()
        {
            InitializeComponent();

            for (int j = 0; j < GlobalVar.BpNumberValue; j++)
            {
                ListVolTemp[j] = new List<VolTemp>();
                for (int i = 0; i < GlobalVar.CellNumberValue; i++)
                {
                    ListVolTemp[j].Add(new VolTemp { Vol = "NotUpdate", Temp = "NotUpdate" });
                }
            }

            for(int j = 0;j < GlobalVar.BpNumberValue;j++)
            {
                BindingListVolTemp[j] = new BindingList<VolTemp>(ListVolTemp[j]);
                Bsource[j] = new BindingSource(BindingListVolTemp[j], null);
            }
 
           

            dataGridView1.DataSource = Bsource[0];

            for (int i = 1; i <= GlobalVar.StringNumberValue; i++)
            {
                comboBoxStringNumber.Items.Add("string" + i.ToString());
            }
            comboBoxStringNumber.SelectedIndex = 0;

            for (int i = 1; i <= GlobalVar.BpNumberValue; i++)
            {
                comboBoxBpNumber.Items.Add("bp" + i.ToString());
            }
            comboBoxBpNumber.SelectedIndex = 0;
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Open usbcan port*/
           
            string CanPortName;
            uint BaudRateValue = 0;
            UInt32 ReadTimeOutValue = 0;
            UInt32 WriteTimeOutValue = 0;
            int nRet = 0;
            uint dwMaskCode = 0;
            uint dwAccCode = 0;

            AdvCan.CanStatusPar_t CanStatus = new AdvCan.CanStatusPar_t();     
            CanPortName = GlobalVar.CanPort;                                       //Get the CAN port name

            if(CanPortControlButton.Text == "Start")
            {
                ShowStatus.Items.Clear();
                CanPortControlButton.Text = "Stop";
                

            
             #region
             /**********************************************************************************************
            *  NOTE: acCanOpen Usage
            * 
            *	  Description:
            *		 Open can port by name, and indicate the max send and receive Frame number each time.
            * 
            *    acCanOpen arguments:
            *		  PortName			         - port name
            *		  synchronization	         - TRUE, synchronization ; FALSE, asynchronous
            *		  MsgNumberOfReadBuffer	   - The max frames number to read each time
            *		  MsgNumberOfWriteBuffer	- The max frames number to write each time
            * 
            *    When open port, user must pass the value of 'MsgNumberOfReadBuffer' and 'MsgNumberOfWriteBuffer' 
            *    auguments to indicate the max sent and received packages number of each time.
            *    In this example, we send 100 CAN frames by default
            *    User can change the value of 'nMsgCount' to send different frames each time in this examples.
            **********************************************************************************************/
            nRet = Device.acCanOpen(GlobalVar.CanPort, false, GlobalVar.MaxMsgCount, GlobalVar.MaxMsgCount);                               //Open CAN port
            if (nRet < 0)
            {
                    //MessageBox.Show("Failed to open the CAN port, please check the CAN port name!");
                    ShowStatus.Items.Add("Failed to open the CAN port, please check the CAN port name!");
                   return;
            }

            nRet = Device.acEnterResetMode();                     //Enter reset mode          
            if (nRet < 0)
            {
                MessageBox.Show("Failed to stop opertion!");
                Device.acCanClose();
                return;
            }

            BaudRateValue = (uint)Convert.ToInt32(GlobalVar.CanBaudrate);        //Get baud rate
            nRet = Device.acSetBaud(BaudRateValue);                               //Set Baud Rate
            if (nRet < 0)
            {
                MessageBox.Show("Failed to set baud!");
                Device.acCanClose();
                return;
            }
            try
            {
                WriteTimeOutValue = Convert.ToUInt32(GlobalVar.CanWriteTimeOut);     //Get write timeout
            }
            catch (Exception)
            {          
                MessageBox.Show("Invalid Write TimeOut value!");
                Device.acCanClose();                
                return;
            }

            try
            {
                ReadTimeOutValue = Convert.ToUInt32(GlobalVar.CanReadTimeOut);        //Get read timeout
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Read TimeOut value!");
                Device.acCanClose();
                return;
            }

            nRet = Device.acSetTimeOut(ReadTimeOutValue, WriteTimeOutValue);      //Set timeout
            if (nRet < 0)
            {
                MessageBox.Show("Failed to set Timeout!");
                Device.acCanClose();
                return;
            }

            nRet = Device.acClearRxFifo();                                        //Clear receive fifo of driver
            if (nRet < 0)
            {            
                Device.acCanClose();
                return;
            }
            

            nRet = Device.acEnterWorkMode();                                     //Enter work mode
            if (nRet < 0)
            {
                MessageBox.Show("Failed to restart operation!");
                Device.acCanClose();
                return;
            }

                ShowStatus.Items.Add("Start successfully!");
            //nRet = Device.acGetStatus(ref CanStatus);                       //Get status
            //if (nRet < 0)
            //{
            //    MessageBox.Show("Failed to get current status!");
            //    Device.acCanClose();
            //    return;
            //}
            #endregion

            CanReceiveThreadFunc = new ThreadStart(CanRecThreadMethod);
            CanReceiveThread = new Thread(CanReceiveThreadFunc);
            CanReceiveThread.Priority = ThreadPriority.Normal;
            CanReceiveThread.Start();
            }
            else
            {
                GlobalVar.CanReceiveThreadFlag = false;
                Thread.Sleep(100);
                Device.acCanClose();
                CanPortControlButton.Text = "Start";
                   
            }
        }


        private void CanRecThreadMethod()
        {
          
            string ReceiveStatus = " ";
            int nRet;
            uint nReadCount = GlobalVar.MaxMsgCount;
            uint pulNumberofRead = 0;
            uint ReceiveIndex = 0;

            myDelegate SetList = new myDelegate(ListDelegate);
            myDelegate SetButton = new myDelegate(ButtonDelegate);
            AdvCan.canmsg_t[] msgRead = new AdvCan.canmsg_t[GlobalVar.MaxMsgCount];
            for(int i = 0;i < GlobalVar.MaxMsgCount;i++)
            {
                msgRead[i].data = new byte[8];
            }

            ReceiveIndex = 0;
            GlobalVar.CanReceiveThreadFlag = true;
            while(GlobalVar.CanReceiveThreadFlag)
            {
                nRet = Device.acCanRead(msgRead, nReadCount, ref pulNumberofRead);
                if(nRet == AdvCANIO.TIME_OUT)
                {
                    ReceiveStatus = "Package ";
                    ReceiveStatus += "receiving timeout!";
                    ShowStatus.Invoke(SetList, ReceiveStatus);//Package receiving timeout
                }
                else if(nRet == AdvCANIO.OPERATION_ERROR)
                {
                    ReceiveStatus = "Package ";
                    ReceiveStatus += " error!";
                    ShowStatus.Invoke(SetList, ReceiveStatus);
                }
                else
                {
                    for(int j = 0;j < pulNumberofRead;j++)
                    {
                        ReceiveStatus = "Package";
                        ReceiveStatus += Convert.ToString(ReceiveIndex + j + 1) + " is ";
                        if(msgRead[j].id == AdvCan.ERRORID)
                        {
                            ReceiveStatus += "a RTR package!";
                            ShowStatus.Invoke(SetList, ReceiveStatus);
                        }
                        else
                        {
                            if ((msgRead[j].flags & AdvCan.MSG_RTR) > 0)
                            {
                                ReceiveStatus += "a RTR packge";

                            }
                            else
                            {
                                AnalyseMessage(msgRead);
                                for (int i = 0; i < msgRead[j].length; i++)
                                {
                                    ReceiveStatus += msgRead[j].data[i].ToString();
                                    ReceiveStatus += " ";
                                    
                                }
                            }
                        }
                        ShowStatus.Invoke(SetList, ReceiveStatus);
                    }                    
                }
                ReceiveIndex += pulNumberofRead;
                Thread.Sleep(0);
            }
            ShowStatus.Invoke(SetList, "Stop Read!");
        }

        void AnalyseMessage(AdvCan.canmsg_t[] msgRead)
        {
            byte MsgID =(byte)(msgRead[0].id & 0xFF);
            int BPID = (int)(msgRead[0].id >> 8) & 0x3F;
            int StringID = (int)(msgRead[0].id >> 14) & 0x0F;

            switch (MsgID)
            {
                case (byte)StringMessageID.TargetVol:
                    textBox1.Invoke(new MethodInvoker(delegate { textBox1.Text = (msgRead[0].data[0] * 256 + msgRead[0].data[1]).ToString(); }));
                    break;
                
                case (byte)StringMessageID.SelectedBP1_4Vol:                    
                case (byte)StringMessageID.SelectedBP5_8Vol:                                       
                case (byte)StringMessageID.SelectedBP9_12Vol:                                       
                case (byte)StringMessageID.SelectedBP13_16Vol:
                    VoltageCal(msgRead);
                    break;
                case (byte)StringMessageID.SelectedBP1_4Temp:
                case (byte)StringMessageID.SelectedBP5_8Temp:
                case (byte)StringMessageID.SelectedBP9_12Temp:
                case (byte)StringMessageID.SelectedBP13_16Temp:
                    TempCal(msgRead);
                    break;
            }
        }

        void VoltageCal(AdvCan.canmsg_t[] msgRead)
        {
            byte MsgID = (byte)(msgRead[0].id & 0xFF);
            int BPID = (int)(msgRead[0].id >> 8) & 0x3F;
            int index = (int)(MsgID - StringMessageID.SelectedBP1_4Vol);
            for (int i = 0; i < 4; i++)
            {
                cellvol[i + index * 4] = (float)(msgRead[0].data[2 * i] * 256 + msgRead[0].data[2 * i + 1]) / 1000;
                BindingListVolTemp[BPID - 1][i + index * 4] = new VolTemp { Vol = cellvol[i + index * 4].ToString(), Temp = celltemp[i + index * 4].ToString() };
            }
        }

        void TempCal(AdvCan.canmsg_t[] msgRead)
        {
            byte MsgID = (byte)(msgRead[0].id & 0xFF);
            int BPID = (int)(msgRead[0].id >> 8) & 0x3F;
            int index = (int)(MsgID - StringMessageID.SelectedBP1_4Vol);
            for (int i = 0; i < 4; i++)
            {
                celltemp[i + index * 4] = (float)(msgRead[0].data[2 * i] * 256 + msgRead[0].data[2 * i + 1]) / 100;
                BindingListVolTemp[BPID - 1][i + index * 4] = new VolTemp { Vol = cellvol[i + index * 4].ToString(), Temp = celltemp[i + index * 4].ToString() };
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVar.CanReceiveThreadFlag = false;
            Device.acCanClose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Bp information";
            label2.Text = "String information";
            updateDataGrid();
        }

        void updateDataGrid()
        {             
            ArrayList list = new ArrayList();

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = "cell" + (i + 1).ToString();
            }
            //dataGridView1.Rows.Add(list.ToArray());

            dataGridView1.RowHeadersWidth = 70 ;

           
            dataGridView1.AutoSize = true;
          //  dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
           // dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;

            dataGridView1.BorderStyle = BorderStyle.None;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AdvCan.canmsg_t[] msgWrite = new AdvCan.canmsg_t[GlobalVar.MaxMsgCount];
            uint pulNumberofWritten = 1;
            msgWrite[0].flags = AdvCan.MSG_EXT;
            msgWrite[0].cob = 0;
            msgWrite[0].id = 0X800A2;
            msgWrite[0].length = 0;
             Device.acCanWrite(msgWrite, 1, ref pulNumberofWritten); //Send frames

        }

        private void comboBoxBpNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedBPid = comboBoxBpNumber.SelectedIndex;
            dataGridView1.DataSource = Bsource[selectedBPid];
        }
    }
}

