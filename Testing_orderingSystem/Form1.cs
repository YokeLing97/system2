using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Testing_orderingSystem
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\Documents\testint_menu.mdf;Integrated Security=True;Connect Timeout=30");

        public object Messagebox { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           display_data();
        }

        public void display_data()
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "select * from Menu";
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            SqlDataAdapter data = new SqlDataAdapter(command);
            data.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
        }

        void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if((textBox_Name.Text.Trim() == string.Empty) || (textBox_Price.Text.Trim() == string.Empty) || (textBox_Description.Text.Trim() == string.Empty))
            {
                MessageBox.Show("Please Provide Details!!");
            }
            else
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "Insert into Menu values('" + textBox_Name.Text + "','" + textBox_Price.Text + "','" + textBox_Description.Text + "')";
                command.ExecuteNonQuery();
                connection.Close();
                display_data();
                MessageBox.Show("Record Inserted SUCCESSFULLY!");
                ClearAllText(this);
            }
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if(textBox_Name.Text != "")
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "delete from Menu where name = '" + textBox_Name.Text + "'";
                command.ExecuteNonQuery();
                connection.Close();
                display_data();
                MessageBox.Show("Record Deleted SUCCESSFULLY!");
                ClearAllText(this);
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (textBox_UName.Text != "" && textBox_UPrice.Text != "" && textBox_UDescription.Text != "")
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "update Menu set name='" + textBox_UName.Text + "', price='" + textBox_UPrice.Text + "', description='" + textBox_UDescription.Text + "' where name='" + textBox_Name.Text + "'";//, price= '" +textBox_Price.Text+ "' description= '" +textBox_Description.Text+ "'";
                command.ExecuteNonQuery();
                connection.Close();
                display_data();
                MessageBox.Show("Record Updated SUCCESSFULLY!");
                ClearAllText(this);
            }

            else
            {
                MessageBox.Show("Please Provide Details Information!");
            }
        }

        //private void button_Search_Click(object sender, EventArgs e)
        //{
           
            /*
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * from Menu where name= '" + textBox_Search.Text + "'";
            command.ExecuteNonQuery();
            DataTable table = new DataTable();
            SqlDataAdapter data = new SqlDataAdapter(command);
            data.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();*/
        //}

        private void button_Display_Click(object sender, EventArgs e)
        {
            display_data();
            textBox_Name.Text = "";
            textBox_UName.Text = "";
            textBox_Price.Text = "";
            textBox_UPrice.Text = "";
            textBox_Description.Text = "";
            textBox_UDescription.Text = "";
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox_Name.Text = row.Cells["Name"].Value.ToString();
                textBox_Price.Text = row.Cells["Price"].Value.ToString();
                textBox_Description.Text = row.Cells["Description"].Value.ToString();
            }
        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * from Menu where name LIKE '%'+@name+'%'";
            command.Parameters.AddWithValue("@name", textBox_Search.Text);
            SqlDataAdapter data = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            data.Fill(set);
            dataGridView1.DataSource = set.Tables[0];
            connection.Close();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to exit?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            { }



        }
    }

}
