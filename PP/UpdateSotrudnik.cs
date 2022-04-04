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
    public partial class UpdateSotrudnik : Form
    {
        public string path = @"Data Source=DESKTOP-LB0TMO2\SQL;Initial Catalog=PP4.1;Integrated Security=True";
        public UpdateSotrudnik()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                int tipmach;
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE Sotrudniki SET Familia='{textBox1.Text}', Imia='{textBox2.Text}',  Otchestvo='{textBox4.Text}', DataRojdenia='{dateTimePicker1.Value}', Staj={textBox3.Text} ", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Обновлено!");

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
    }
}
