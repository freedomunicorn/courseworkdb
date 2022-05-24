using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            DisplayCustomers();
            label7.Text = Login.empname;
        }
        SqlConnection con = new SqlConnection(@"Data Source=(local);Initial Catalog=pets;Integrated Security=True;Pooling=False");
        int key = 0;
        private void DisplayCustomers()
        {
            try
            {
                con.Open();
                string Query = " select *" +
                               " from CustomerTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, con);
                SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                CustomerDGV.DataSource = ds.Tables[0];
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла проблема ==>" + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void Clear()
        {
            CustomerName.Text = "";
            CustomerPhone.Text = "";
            CustomerAddress.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CustomerName.Text == "")
            {
                MessageBox.Show("Пожалуйста, добавьте имя");
                return;
            }
            if (CustomerPhone.Text == "")
            {
                MessageBox.Show("Пожалуйста, добавьте номер телефона");
                return;
            }
            if (CustomerAddress.Text == "")
            {
                MessageBox.Show("Пожалуйста, добавьте адрес");
                return;
            }
            else if (CustomerName.Text != "" && CustomerPhone.Text != "" && CustomerAddress.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName,CustAdd,CustPhone) values(@CN,@CA,@CP)", con);
                    cmd.Parameters.AddWithValue("@CN", CustomerName.Text);
                    cmd.Parameters.AddWithValue("@CA", CustomerAddress.Text);
                    cmd.Parameters.AddWithValue("@CP", CustomerPhone.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Клиент : " + CustomerName.Text + " Добавлен");
                    con.Close();
                    DisplayCustomers();
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла проблема ==>" + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            CustomerName.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustomerAddress.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustomerPhone.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Вы должны выбрать Клиента");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId = @CustId", con);  
                    cmd.Parameters.AddWithValue("@CustId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Клиент удален");
                    con.Close();
                    DisplayCustomers();
                    key = 0;
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла проблема ==>" + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (CustomerName.Text == "")
            {
                MessageBox.Show("Пожалуйста, добавьте имя");
                return;
            }
            if (CustomerPhone.Text == "")
            {
                MessageBox.Show("Пожалуйста, добавьте номер телефона");
                return;
            }
            if (CustomerAddress.Text == "")
            {
                MessageBox.Show("Пожалуйста, добавьте адрес");
                return;
            }
            else if (CustomerName.Text != "" && CustomerPhone.Text != "" && CustomerAddress.Text != "")
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set" +
                                                    " CustName = @CN ," +
                                                    " CustAdd = @CA  ," +
                                                    " CustPhone = @CP " +
                                                    " where CustId =@CustId ", con); 
                    cmd.Parameters.AddWithValue("@CN", CustomerName.Text);
                    cmd.Parameters.AddWithValue("@CA", CustomerAddress.Text);
                    cmd.Parameters.AddWithValue("@CP", CustomerPhone.Text);
                    cmd.Parameters.AddWithValue("@CustId", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Сотрудник обновлен");
                    con.Close();
                    DisplayCustomers();
                    key = 0;
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникла проблема ==>" + ex.Message);
                }
                finally
                {              
                    con.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Home obj = new Home();
            obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings obj = new Billings();
            obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
