using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public static string empname;
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btlReset_Click(object sender, EventArgs e)
        {
            Nametxt.Text = "";
            Passwordtxt.Text = "";
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (local); Initial Catalog = pets; Integrated Security = True; Pooling = False");
            SqlDataAdapter sqa = new SqlDataAdapter("Select count(*) from EmployeeTbl where EmpName = '"+Nametxt.Text+ "'and EmpPass ='"+Passwordtxt.Text+"'",con);
           DataTable dt = new DataTable();
            sqa.Fill(dt);
                if (dt.Rows[0][0].ToString() =="1")
                {
                    empname = Nametxt.Text;
                    Home obj = new Home();
                    obj.Show();
                    this.Hide();                   
                }
            else
            {
                MessageBox.Show("Такого пользователя не существует.");
            }
        }
    } 
}
