using Sem_SO_Project.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sem_SO_Project
{
    public partial class AllProcessStats : Form
    {
        public List<Process> lss = new List<Process>();
        public AllProcessStats(List<Process> lista)
        {
            lss = lista;
            InitializeComponent();

            this.FinallP.DataSource = lista;
            FinallP.Columns[1].Visible = false;
            FinallP.Columns[2].Visible = false;
            FinallP.Columns[3].Visible = false;
            FinallP.Columns[4].Visible = false;
            FinallP.Columns[5].Visible = false;
            FinallP.Columns[6].Visible = false;
            FinallP.Columns[7].Visible = false;
        }
    }
}
