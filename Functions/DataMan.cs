using Sem_SO_Project.Class;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Sem_SO_Project.Functions
{
    public class DataMan
    {
        Processwindow wp; //= new Processwindow();
        //public List<Process> ls = new List<Process>();
        public List<Process>[] ls = new List<Process>[100];
        public List<Process> ls2 = new List<Process>();        
        public List<Process> ls3 = new List<Process>();
        public List<Process> ls4 = new List<Process>();
        public List<Process> ls5 = new List<Process>();
        public System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();

        string id, op, name, tmpt;
        public int cantProcess, timing, TT = 0, TR = 0,  controlP, sh1= 0, sh2 = 0, numlot, lotIND, reloj = 0, kd = 1, globalCantProcss = 0, globalCantProcss2 = 0,index3;
        public int num, iTTR, index2= 3, values;
        public static int h;
        string value, TE, Ftime;
        

        public void uno (Processwindow pw)
        {
            wp = pw;
        }

        public void IniProcess(List<Process>[] pro)
        {
            //ls = pro;
            ls2 = new List<Process>();
            ls4 = new List<Process>();
            ls5 = new List<Process>();
            ls3 = pro[0];
            //ls5 = pro[0];
            set4Process();
            //cantProcess = ls[0].Count;
            globalCantProcss = pro[0].Count;
            //globalCantProcss2 = globalCantProcss;
            //controlP = cantProcess;   
            controlP = globalCantProcss;
            index3 = globalCantProcss;
            wp.LoteEjec.DataSource = ls[0];
            setGridProperties_LoteEjec();

            arrayUse();
            wp.Show();
            EjecProcess();
 
        }

        public void set4Process()
        {
            ls[0] = new List<Process>();
            for (int i = 0; i <= 3; i++)
            {
                Process pr3 = new Process();

                pr3.IDs = ls3[i].IDs;
                pr3.Nombre = ls3[i].Nombre;
                pr3.TE = ls3[i].TE;
                pr3.OP = ls3[i].OP;
                pr3.TT = ls3[i].TT;
                pr3.TR = ls3[i].TR;
                pr3.TLL = 0;
                pr3.Flag = "0";
                ls[0].Add(pr3);

            }

            index2 = 3;
            
        }

        public void setGridProperties_LoteEjec()
        {
            foreach (DataGridViewBand band in wp.LoteEjec.Columns)
            {
                band.ReadOnly = true;
            }

            wp.LoteEjec.Columns[0].Width = 70;
            wp.LoteEjec.Columns[1].Width = 70;
            wp.LoteEjec.Columns[2].Width = 70;
            wp.LoteEjec.Columns[3].Width = 70;

            for(int i = 4; i <= 15; i++)
            {
                wp.LoteEjec.Columns[i].Visible = false;
            }

        }

        public void setGridProperties_Bloqueados()
        {
            foreach (DataGridViewBand band in wp.Bloqueado.Columns)
            {
                band.ReadOnly = true;
            }

            wp.Bloqueado.Columns[1].Visible = false;
            wp.Bloqueado.Columns[2].Visible = false;
            wp.Bloqueado.Columns[3].Visible = false;
            wp.Bloqueado.Columns[4].Visible = false;
            wp.Bloqueado.Columns[5].Visible = false;
            wp.Bloqueado.Columns[6].Visible = false;

        }

        public void arrayUse()
        {
            for(int i= 0; i <= 100; i++)
            {
                if(ls[i] == null)
                {
                    break;
                }
                else
                {
                    numlot++;
                    continue;
                }
            }
        }

        public bool CheckIFNum(string str)
        {

            int n;
            bool isNum = int.TryParse(str, out n);
            num = n;
            h = num;
            return isNum;

        }

        public bool CheckIFNum2(string str)
        {

            int n;
            bool isNum = int.TryParse(str, out n);
            iTTR = n;
            return isNum;

        }

        public void SendToFinal(int a, bool error)
        {
            id = wp.LoteEjec.Rows[a].Cells["IDs"].Value.ToString();
            op = wp.textBox3.Text = wp.LoteEjec.Rows[a].Cells["OP"].Value.ToString();
            name = wp.LoteEjec.Rows[a].Cells["Nombre"].Value.ToString();
            tmpt = wp.LoteEjec.Rows[a].Cells["TE"].Value.ToString();
            EvaOp(op,id,true,error);                      

        }

        public void Interrupt()
        {

            GetCurrentProcess(sh1);

            Process pr2 = new Process();

            pr2.IDs = id;
            pr2.Nombre = name;
            pr2.TE = tmpt;
            pr2.OP = op;
            pr2.TT = TT;
            pr2.TR = TR;
            pr2.Flag = "1";
            pr2.BLOQ = 8;

            ls2.Add(pr2);
            wp.Bloqueado.DataSource = null;
            wp.Bloqueado.Rows.Clear();
            wp.Bloqueado.Refresh();
            wp.Bloqueado.DataSource = ls2;
            setGridProperties_Bloqueados();

            EliminateProcess(false);
            ReturnBloqProcess();
            AddNewProcess();

            TT = 0;
            TR = 0;
        }

        public void ReturnBloqProcess()
        {
            int PblockCant = ls2.Count();

            if (PblockCant > 0)
            {
                
                for (int i = 1; i <= PblockCant; i++)
                {
                   
                    if (ls2[i - 1].BLOQ > 0)
                    {
                        ls2[i - 1].BLOQ--;
                        wp.Bloqueado.Refresh();
                    }
                    else if(ls2[i-1].BLOQ == 0 && ls[0].Count() < 4)
                    {
                        Process pr4 = new Process();
                        pr4.IDs = ls2[i - 1].IDs;
                        pr4.Nombre = ls2[i - 1].Nombre;
                        pr4.TE = ls2[i - 1].TE;
                        pr4.OP = ls2[i - 1].OP;
                        pr4.TT = ls2[i - 1].TT;
                        pr4.TR = ls2[i - 1].TR;
                        pr4.Flag = "1";
                        ls[0].Add(pr4);

                        ls2.RemoveAt(i - 1);
                        wp.Bloqueado.DataSource = null;
                        wp.Bloqueado.Rows.Clear();
                        wp.Bloqueado.Refresh();
                        wp.Bloqueado.DataSource = ls2;
                        setGridProperties_Bloqueados();

                        wp.LoteEjec.DataSource = null;
                        wp.LoteEjec.Rows.Clear();
                        wp.LoteEjec.Refresh();
                        wp.LoteEjec.DataSource = ls[0];
                        setGridProperties_LoteEjec();

                        PblockCant = ls2.Count();
                        i--;

                    }

                }
            }
        }

        public void GetCurrentProcess(int index)
        {
            id = wp.LoteEjec.Rows[index].Cells["IDs"].Value.ToString();
            op = wp.LoteEjec.Rows[index].Cells["OP"].Value.ToString();
            name = wp.LoteEjec.Rows[index].Cells["Nombre"].Value.ToString();
            tmpt = wp.LoteEjec.Rows[index].Cells["TE"].Value.ToString();
        }

        public void AddNewProcess()
        {
            int SABE = ls3.Count();
            int count = wp.LoteEjec.RowCount;
            int count2 = wp.Bloqueado.RowCount;
            int cunt3 = count + count2;

            if(cunt3 < 4)
            {
                index2++;
                if (index2 <= index3 - 1)
                {
                    Process pr3 = new Process();
                    pr3.IDs = ls3[index2].IDs;
                    pr3.Nombre = ls3[index2].Nombre;
                    pr3.TE = ls3[index2].TE;
                    pr3.OP = ls3[index2].OP;
                    pr3.TT = ls3[index2].TT;
                    pr3.TR = ls3[index2].TR;
                    pr3.TLL = reloj;
                    pr3.Flag = "0";
                    ls[0].Add(pr3);
                    wp.LoteEjec.DataSource = null;
                    wp.LoteEjec.Rows.Clear();
                    wp.LoteEjec.Refresh();
                    wp.LoteEjec.DataSource = ls[0];
                    setGridProperties_LoteEjec();
                }
            }
 
        }

        public void EliminateProcess(bool Ekey)
        {
            int count = wp.LoteEjec.RowCount;
            int count2 = wp.Bloqueado.RowCount;

            if (count == 1 ) {

                if (count2 == 0)
                {
                    if (Ekey == true)
                    {
                        controlP--;
                    }
                    SendToFinal(sh1, false);

                    t.Stop();

                    ls[lotIND].RemoveAt(sh1);
                    wp.LoteEjec.DataSource = null;
                    wp.LoteEjec.Rows.Clear();
                    wp.LoteEjec.Refresh();
                    wp.LoteEjec.DataSource = ls[lotIND];
                    setGridProperties_LoteEjec();

                    TT = 0;
                    TR = 0;


                    wp.textBox7.Text = "0";
                    MessageBox.Show("El proceso termino");

                    //int count = ls5.Count();
                    AllProcessStats alp = new AllProcessStats(ls5);
                    alp.Show();
                }

            }
            else
            {
                if (Ekey == true)
                {
                    controlP--;
                }
                ls[lotIND].RemoveAt(sh1);
                wp.LoteEjec.DataSource = null;
                wp.LoteEjec.Rows.Clear();
                wp.LoteEjec.Refresh();
                wp.LoteEjec.DataSource = ls[lotIND];
                setGridProperties_LoteEjec();

                TT = 0;
                TR = 0;
            }
        }

        public bool EvaOp(string op, string id, bool active, bool error)
        {
            try
            {
                string math = op;
                value = new DataTable().Compute(math, null).ToString();
                if (String.IsNullOrEmpty(value))
                {
                    return false;
                }
                else
                {   
                    if (active == true)
                    {
                        if (error == false)
                        {
                            wp.LoteFinal.Rows.Add(id, op, value);
                            Process pr5 = new Process();
                            int.TryParse(TE, out values);

                            ls4 = ls[0];
                            pr5 = ls4[0];
                            pr5.Value = value;
                            pr5.TF = reloj;      
                            pr5.TS = values;
                            pr5.TRS = pr5.TF - pr5.TLL;
                            pr5.TRA = pr5.TRS - pr5.TS;
                            
                            ls5.Add(pr5);
                            return true;
                        }
                        else if (error == true)
                        {
                            Process pr5 = new Process();
                            wp.LoteFinal.Rows.Add(id, op, "ERROR");

                            ls4 = ls[0];
                            pr5 = ls4[0];
                            pr5.Value = "ERROR";
                            pr5.TF = reloj;
                            pr5.TS = TT;
                            pr5.TRS = pr5.TF - pr5.TLL;
                            pr5.TRA = pr5.TRS - pr5.TS;
                            ls5.Add(pr5);

                            return true;
                        }
                        
                    }

                    return true;                

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("ID: " + id + "\nLa operación ingresada no es valida.");
                return false;
            }
         
        }

        public void incTT()
        {
            if (TT <= num)
            {
                
                wp.textBox5.Text = TT.ToString();
                wp.textBox6.Text = TR.ToString();
                wp.textBox7.Text = controlP.ToString();
                wp.textBox8.Text = reloj.ToString();
                ReturnBloqProcess();

                reloj++;
                TT++;
                TR = h-TT;               

            }
            else if (TT >= num)
            {
                controlP--;

                if (controlP == 0)
                {
                    
                        SendToFinal(sh1, false); 
                        t.Stop();
                        wp.textBox7.Text = "0";
                        MessageBox.Show("El proceso termino");

                        int count = ls5.Count();
                        AllProcessStats alp = new AllProcessStats(ls5);
                        alp.Show();


                }
                else if (controlP > 0)
                {
                    SendToFinal(sh1, false);
                    EliminateProcess(false);
                    ReturnBloqProcess();
                    AddNewProcess();
                    //globalCantProcss2--;
                    //sh1++;
                    TT = 0;
                    TR = 0;
                }

            }

        }
        
        private void setProcess(int a)
        {
            int count = wp.LoteEjec.RowCount;

            if (count == 0)
            {

            }
            else
            {
                string flg;

                CheckIFNum(wp.LoteEjec.Rows[a].Cells["TE"].Value.ToString());
                timing = num;
                wp.textBox1.Text = wp.LoteEjec.Rows[a].Cells["IDs"].Value.ToString();
                wp.textBox2.Text = wp.LoteEjec.Rows[a].Cells["Nombre"].Value.ToString();
                wp.textBox3.Text = wp.LoteEjec.Rows[a].Cells["OP"].Value.ToString();
                wp.textBox4.Text = wp.LoteEjec.Rows[a].Cells["TE"].Value.ToString();
                TE = wp.LoteEjec.Rows[a].Cells["TE"].Value.ToString();

                Ftime = wp.LoteEjec.Rows[a].Cells["FirstTime"].Value.ToString();

                if (Ftime == "False")
                {
                    wp.LoteEjec.Rows[a].Cells["TES"].Value = reloj.ToString();
                    wp.LoteEjec.Rows[a].Cells["FirstTime"].Value = true;
                }

                flg = wp.LoteEjec.Rows[a].Cells["Flag"].Value.ToString();

                if (flg == "1")
                {
                    CheckIFNum2(wp.LoteEjec.Rows[a].Cells["TT"].Value.ToString());
                    TT = iTTR;
                    CheckIFNum2(wp.LoteEjec.Rows[a].Cells["TR"].Value.ToString());
                    TR = iTTR;
                    wp.LoteEjec.Rows[a].Cells["Flag"].Value = "0";
                }
            }
        }

        public void timer_Tick(object sender, EventArgs e)
        {

            setProcess(sh1);
            incTT();
           
        }

        public  void geTiming()
        {
           
             t.Interval = 1000; // specify interval time as you want
             t.Tick += new EventHandler(timer_Tick);
             t.Start();
        }

        public  void EjecProcess()
        {

            geTiming();

        }

    }  

}
