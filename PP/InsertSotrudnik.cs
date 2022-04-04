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

namespace PP
{
    public partial class InsertSotrudnik : Form
    {
        public string path = @"Data Source=DESKTOP-LB0TMO2\SQL;Initial Catalog=PP4.1;Integrated Security=True";
        public InsertSotrudnik()
        {
            InitializeComponent();
        }

        public void LoadTip()
        {
           
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                if (textBox1.Text != "")
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO Sotrudniki (Familia, Imia, Otchestvo, DataRojdenia, Staj)" 
                        + $"VALUES ('{textBox1.Text}', '{textBox2.Text}', '{textBox4.Text}', '{dateTimePicker1.Value}', {textBox3.Text})", connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Добавлено!");

                }
                else
                {
                    MessageBox.Show("Пустое поле!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void InsertTipTransporta_Load(object sender, EventArgs e)
        {
            LoadTip();
        }
    }
}
