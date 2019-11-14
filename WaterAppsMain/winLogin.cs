using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WaterAppsMain
{
   // public WaterAppsService.Connection conn;

    public partial class winLogin : Form
    {
        public winLogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            WaterAppsTransactions.Connection con = new WaterAppsTransactions.Connection();
            con.openConnection();
        }

        private void winLogin_Load(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //conn

        }
    }
}
