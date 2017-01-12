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
    public partial class Form2 : Form
    {
        BindingSource FillDisplayGrid = new BindingSource();
        //    List<cellVolTemp>[] Cell = new List<cellVolTemp>[GlobalVar.BpNumberValue];

        //   List<cellVolTemp>[] cell = new List<cellVolTemp>[100];
        
        
        
        List<List<cellVolTemp>> Testcell = new List<List<cellVolTemp>>()
        {
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
           new List<cellVolTemp>(),
        };

        //   BindingList<cellVolTemp>[] BindingCellList = new BindingList<cellVolTemp>[GlobalVar.BpNumberValue];
        BindingList<BindingList<cellVolTemp>> TestBindingCellList = new BindingList<BindingList<cellVolTemp>>()
        {
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),
            new BindingList<cellVolTemp>(),

        };

        //BindingSource[] Bindingcell = new BindingSource[GlobalVar.BpNumberValue];

        BindingSource[,] BindingSourceCell = new BindingSource[GlobalVar.StringNumberValue,GlobalVar.BpNumberValue];
        public Form2() 
        {
            InitializeComponent();
            List<string> Tstr = new List<string>();
            Tstr.Add("1");
            Tstr.Add("2");
            dataGridView1.DataSource = Tstr;




        }

        class Person
        {
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        class cellVolTemp
        {
            public string Voltage { get; set; }
            public string Temperature { get; set; }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            // List<List<cellVolTemp>> ListTTT = new List<List<cellVolTemp>>();
            List<cellVolTemp> cellList = new List<cellVolTemp>();
            cellList.Add(new cellVolTemp { Voltage = "3.7", Temperature = "38" });
            cellList.Add(new cellVolTemp { Voltage = "3.3", Temperature = "48" });
            cellList.Add(new cellVolTemp { Voltage = "3.2", Temperature = "28" });
            //          dataGridView1.DataSource = cellList;
            List<cellVolTemp>[] arraycellList = new List<cellVolTemp>[10];
            for(int i = 0;i < GlobalVar.BpNumberValue;i++)
            {
   //             arraycellList[i] = new List<cellVolTemp>();
            }
            
   //         arraycellList[0].Add(new cellVolTemp { Voltage = "3.7", Temperature = "38" });
             
    //        arraycellList[1].Add(new cellVolTemp { Voltage = "3.7", Temperature = "38" });
            //dataGridView1.DataSource = arraycellList[1];

            List<List<cellVolTemp>> ListExample = new List<List<cellVolTemp>>();

            List<List<cellVolTemp>[]>[] ArrayList = new List<List<cellVolTemp>[]>[100];


            List<cellVolTemp>[] ListBPNumbers = new List<cellVolTemp>[GlobalVar.BpNumberValue];
             
            for(int i = 0;i < GlobalVar.BpNumberValue;i++)
            {
                ListBPNumbers[i] = new List<cellVolTemp>();
                for (int j = 0;j < GlobalVar.CellNumberValue;j++)
                {
                    ListBPNumbers[i].Add(new cellVolTemp { Voltage = i.ToString(), Temperature = j.ToString() });
                }
            }
//            ListBPNumbers[0].Add(new cellVolTemp { Voltage = "3.5", Temperature = "35" });
           // ListBPNumbers[0].Add(new cellVolTemp { Voltage = "3.554", Temperature = "3544" });


            //List<List<cellVolTemp>[]> Listtt = new List<List<cellVolTemp>[]>();
            //Listtt = new List<List<cellVolTemp>[]>();
            //Listtt.Add(ListBPNumbers);

            List<List<cellVolTemp>[]>[] ListStringC = new List<List<cellVolTemp>[]>[GlobalVar.StringNumberValue];  

            for(int i = 0;i < GlobalVar.StringNumberValue;i++)
            {
                ListStringC[i] = new List<List<cellVolTemp>[]>();
            }


           dataGridView1.DataSource = ListStringC[1][0][0];
           
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //        BindingList<BindingList<cellVolTemp>> TestBindingCellList = new BindingList<BindingList<cellVolTemp>>();

            BindingList<cellVolTemp>[] BPBindingCellList = new BindingList<cellVolTemp>[GlobalVar.BpNumberValue];
            for(int i = 0;i < GlobalVar.BpNumberValue;i++)
            {
                BPBindingCellList[i] = new BindingList<cellVolTemp>(ListBPNumbers[i]);
                 
            }



            BindingList<BindingList<cellVolTemp>[]>[] StringBindingCellList= new BindingList<BindingList<cellVolTemp>[]>[GlobalVar.StringNumberValue];


            

            //Testcell[0].Add(new cellVolTemp { Voltage = "3.5", Temperature = "35" });

            //  var cellbindingList[GlobalVar.BpNumberValue];
            //for (int i = 0; i < GlobalVar.BpNumberValue; i++)
            //{
            //    Cell[i] = new List<cellVolTemp>();

            //    BindingCellList[i] = new BindingList<cellVolTemp>(Cell[i]);


            //    Bindingcell[i] = new BindingSource(BindingCellList[i], null);

            //}
            //dataGridView1.DataSource = Bindingcell[0];
            //FillDisplayGrid = Bindingcell[0];
            //BindingCellList[0].Add(new cellVolTemp { Voltage = "5", Temperature = "20" });

            //BindingCellList[1].Add(new cellVolTemp { Voltage = "3445", Temperature = "20" });
            // Cell[0].Add(new cellVolTemp { Voltage = "5", Temperature = "20" });

            //  var cellbindingList = new BindingList<cellVolTemp>(Cell[0]);
            //  var cellsource = new BindingSource(cellbindingList,null);
            //   dataGridView1.DataSource = cellsource;
            //var list = new List<Person>()
            //{
            //    new Person { Name = "Joe", Surname = "sdfs"},
            //    new Person { Name = "Misha",Surname = "sdff" },
            //};
            //var bindingList = new BindingList<Person>(list);
            //var source = new BindingSource(bindingList, null);
            //dataGridView1.DataSource = source;

            //bindingList.Clear();
            //bindingList.Add(new Person { Name = "jimmy", Surname = "kk" });
            //bindingList.Add(new Person { Name = "Jimmy", Surname = "KK" });
            //bindingList[0] = new Person {Name = "sfsd",Surname = "sdfsd" };

            // TestBindingCellList[0].Add();
            for (int i = 0; i < GlobalVar.StringNumberValue; i++)
            {
               // Testcell.Add(new List<cellVolTemp>());
              //  TestBindingCellList.Add(new BindingList<cellVolTemp>());
                
                for (int j = 0;j < GlobalVar.BpNumberValue;j++)
                {
                    
                    
                    //Testcell[i].Add(new cellVolTemp { Voltage = i.ToString(), Temperature = j.ToString() });
                    //TestBindingCellList[i].Add(Testcell[i][j]);
                    //BindingSourceCell[i,j] = new BindingSource(TestBindingCellList[i][j],null);
                }          
            }
            //   Testcell[0].Add(new cellVolTemp { Voltage = "3.5", Temperature = "35" });
            //  Testcell[0][0] = new cellVolTemp { Voltage = "5", Temperature = "20" };
            //  Testcell[1][0] = new cellVolTemp { Voltage = "7", Temperature = "20" };
            //dataGridView1.DataSource = BindingSourceCell[0,1];

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // dataGridView1.DataSource = Bindingcell[1];
        }
    }
}
