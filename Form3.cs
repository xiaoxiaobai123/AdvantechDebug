using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvantechDebug
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public class VolTemp
        {
            public string Vol{ set; get;} 
            public string Temp{set;get;}
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            List<VolTemp> Vt = new List<VolTemp>();
            Vt.Add(new VolTemp { Vol = "3.5", Temp = "36" });
            Vt.Add(new VolTemp { Vol = "3.4", Temp = "37" });
            Vt.Add(new VolTemp { Vol = "3.2", Temp = "36" });
            Vt.Add(new VolTemp { Vol = "3.1", Temp = "37" });

            Vt[0] = new VolTemp { Vol = "2.5", Temp = "36" };

            List<VolTemp> Vv = new List<VolTemp>();
            Vv.Add(new VolTemp { Vol = "2.5", Temp = "26" });
            Vv.Add(new VolTemp { Vol = "2.4", Temp = "27" });
            //          dataGridView1.DataSource = Vt;


            List<VolTemp>[] Vtt = new List<VolTemp>[10];

            for(int i = 0;i < 10;i++)
            {
                Vtt[i] = new List<VolTemp>();
            }
            Vtt[0].Add(new VolTemp { Vol = "3.5", Temp = "36" });
            Vtt[0].Add(new VolTemp { Vol = "3.6", Temp = "37" });
            Vtt[0].Add(new VolTemp { Vol = "3.7", Temp = "30" });
            Vtt[1].Add(new VolTemp { Vol = "3.8", Temp = "38" });
            Vtt[1].Add(new VolTemp { Vol = "3.9", Temp = "39" });
            Vtt[1].Add(new VolTemp { Vol = "3.8", Temp = "38" });
            Vtt[1].Add(new VolTemp { Vol = "3.9", Temp = "39" });
            //            dataGridView1.DataSource = Vtt;

            List<List<VolTemp>> ListV = new List<List<VolTemp>>();

            ListV.Add(Vt);
            ListV.Add(Vv);
   //         dataGridView1.DataSource = ListV[0];           /*区分了BP*/


            List<List<VolTemp>>[] ArrayList = new List<List<VolTemp>>[10];
            for(int i = 0;i < 10;i++)
            {
                ArrayList[i] = new List<List<VolTemp>>();
            }

            //ArrayList[0].Add(Vt);  /*string1BP1*/
            //ArrayList[0].Add(Vv);  /*string1BP2*/
            //ArrayList[1].Add(Vt);  /*string2BP1*/
            dataGridView1.DataSource = ArrayList[0][0];
            //       dataGridView1.DataSource = ArrayList[0][1];


            List<List<VolTemp>[]>[] LList = new List<List<VolTemp>[]>[10];
            for (int i = 0; i < 10; i++)
            {
                LList[i] = new List<List<VolTemp>[]>();
            }
            LList[0].Add(Vtt) ;
            
            dataGridView1.DataSource = LList[0][0][1];

            BindingList<VolTemp> xBB = new BindingList<VolTemp>(Vt);
 
            BindingList<BindingList<VolTemp>>[] BB = new BindingList<BindingList<VolTemp>>[10];
            for(int i = 0;i < 10;i++)
            {
                BB[i].Add(xBB);
            }

            
            //BindingList<VolTemp>[] BPList = new BindingList<VolTemp>[GlobalVar.StringNumberValue];
            //for(int i = 0;i < GlobalVar.StringNumberValue; i++)
            //{
            //    BPList[i] = new BindingList<VolTemp>();
            //    for(int j = 0;j < GlobalVar.BpNumberValue;j++)
            //    {
            //        BPList[i].Add(BPList[i][]);
            //    }
            //}

            //BindingList<BindingList<VolTemp>[]>[] StringBPList = new BindingList<BindingList<VolTemp>[]>[GlobalVar.StringNumberValue];

            //for(int i = 0;i < GlobalVar.StringNumberValue;i++)
            //{
                
            //}
        }
    }
}
