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
    public partial class OSAGO : Form
    {
        public string path = @"Data Source=DESKTOP-LB0TMO2\SQL;Initial Catalog=PP4.1;Integrated Security=True";
        public OSAGO()
        {
            InitializeComponent();
            
        }
        public void LoadOsago()
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT  OSAGO.IdOsago as 'Id осаго', [DataProhojdenia] as 'Дата прохождения', [Stoimosti] as 'Стоимость', [KolichestvoLS] as 'Количество л.с.', [IdTransporta] as 'ID машины' FROM OSAGO", connection);
                DataSet ds2 = new DataSet();
                adapter2.Fill(ds2);
                dataGridView1.DataSource = ds2.Tables[0];

                SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT * FROM Transport", connection);
                DataSet ds3 = new DataSet();
                adapter3.Fill(ds3);
                comboBox1.DataSource = ds3.Tables[0];
                comboBox1.DisplayMember = "Marka";
                comboBox1.ValueMember = "IdTransporta";
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM OSAGO WHERE IdOsago={dataGridView1.CurrentRow.Cells[0].Value}", connection);
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
                LoadOsago();
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadOsago();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                if (textBox1.Text != "" || textBox5.Text != "")
                {
                    connection.Open();
                    int Transport;
                    SqlDataAdapter adapter4 = new SqlDataAdapter($"SELECT IdTransporta FROM Transport WHERE Marka = '{comboBox1.Text}'", connection);
                    DataSet ds4 = new DataSet();
                    adapter4.Fill(ds4);
                    Transport = ds4.Tables[0].Rows[0].Field<int>(ds4.Tables[0].Columns[0]);
                    SqlCommand cmd = new SqlCommand($"INSERT INTO OSAGO (DataProhojdenia, Stoimosti, KolichestvoLS, IdTransporta) VALUES ('{dateTimePicker1.Value}','{textBox1.Text}', '{textBox5.Text}', '{Transport}')", connection);
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
                LoadOsago();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                int tip;
                connection.Open();
                int Transport;
                SqlDataAdapter adapter4 = new SqlDataAdapter($"SELECT IdTransporta FROM Transport WHERE Marka = '{comboBox1.Text}'", connection);
                DataSet ds4 = new DataSet();
                adapter4.Fill(ds4);
                Transport = ds4.Tables[0].Rows[0].Field<int>(ds4.Tables[0].Columns[0]);
                SqlCommand cmd = new SqlCommand($"UPDATE OSAGO SET DataProhojdenia='{dateTimePicker1.Value}', Stoimosti='{textBox5.Text}', KolichestvoLS='{textBox1.Text}', IdTransporta='{Transport}' WHERE IdOsago={dataGridView1.CurrentRow.Cells[0].Value}", connection);
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
                LoadOsago();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
