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
    public partial class Form4 : Form
    {
        public string path = @"Data Source=DESKTOP-LB0TMO2\SQL;Initial Catalog=PP4.1;Integrated Security=True";
        public Form4()
        {
            InitializeComponent();
            LoadSotrudniki();
        }
        public void LoadSotrudniki()
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT  Sotrudniki.IdSotrudnika as 'ID машины', [Familia] as 'Фамилия', [Imia] as 'Имя', [Otchestvo] as 'Отчество', [DataRojdenia] as 'Дата рождения', [Staj] as 'Стаж' FROM Sotrudniki", connection);
                 DataSet ds2 = new DataSet();
                 adapter2.Fill(ds2);
                 dataGridView2.DataSource = ds2.Tables[0];
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

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM Sotrudniki WHERE IdSotrudnika={dataGridView2.CurrentRow.Cells[0].Value}", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Удалено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                LoadSotrudniki();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateSotrudnik frm = new UpdateSotrudnik();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertSotrudnik frm = new InsertSotrudnik();
            frm.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
