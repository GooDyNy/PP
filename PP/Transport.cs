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
    public partial class Transport : Form
    {
        public string path = @"Data Source=DESKTOP-LB0TMO2\SQL;Initial Catalog=PP4.1;Integrated Security=True";

        public Transport()
        {
            InitializeComponent();
            LoadTransport();
        }
       
        public void LoadTransport()
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT  TipMachini.IdTipaMachini as 'ID вида машины', [TipMachini] as 'Вид машины' FROM TipMachini", connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                
                dataGridView1.DataSource = ds.Tables[0];
                SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT  Transport.IdTransporta as 'ID машины', [NomerMachini] as 'Номер машины', [Marka] as 'Марка', [Gruzopodemnost] as 'Грузопдъемность', [DataTechosmotra] as 'Дата техосмотра', [IdTipaMachini] as 'Вид машины' FROM Transport", connection);
                DataSet ds2 = new DataSet();
                adapter2.Fill(ds2);
                dataGridView2.DataSource = ds2.Tables[0];

                SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT * FROM TipMachini", connection);
                DataSet ds3 = new DataSet();
                adapter3.Fill(ds3);
                comboBox1.DataSource = ds3.Tables[0];
                comboBox1.DisplayMember = "TipMachini";
                comboBox1.ValueMember = "IdTipaMachini";

                SqlDataAdapter adapter4 = new SqlDataAdapter("SELECT  TechObslujivanie.IdTechOB as 'ID техообслуживания', [Technick] as 'Техник', [VidUslugi] as 'Вид услуги', [DataObslujivania] as 'Дата обслуживания', [Cena] as 'Цена', [IdTransporta] as 'Машина' FROM TechObslujivanie", connection);
                DataSet ds4 = new DataSet();
                adapter4.Fill(ds4);
                dataGridView3.DataSource = ds4.Tables[0];

                SqlDataAdapter adapter5 = new SqlDataAdapter("SELECT * FROM Sotrudniki", connection);
                DataSet ds5 = new DataSet();
                adapter5.Fill(ds5);
                comboBox3.DataSource = ds5.Tables[0];
                comboBox3.DisplayMember = "Familia";
                comboBox3.ValueMember = "IdSotrudnika";

                SqlDataAdapter adapter6 = new SqlDataAdapter("SELECT * FROM Transport", connection);
                DataSet ds6 = new DataSet();
                adapter6.Fill(ds6);
                comboBox2.DataSource = ds6.Tables[0];
                comboBox2.DisplayMember = "Marka";
                comboBox2.ValueMember = "IdTransporta";

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                if (textBox1.Text != "")
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand($"INSERT INTO TipMachini (TipMachini) VALUES ('{textBox1.Text}')", connection);
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
                LoadTransport();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            LoadTransport();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE TipMachini SET TipMachini='{textBox1.Text}' WHERE IdTipaMachini={dataGridView1.CurrentRow.Cells[0].Value}", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Изменено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                LoadTransport();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM TipMachini WHERE IdTipaMachini={dataGridView1.CurrentRow.Cells[0].Value}", connection);
                //  EXECUTE DeleteGroup 2
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
                LoadTransport();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM Transport WHERE IdTransporta={dataGridView2.CurrentRow.Cells[0].Value}", connection);
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
                LoadTransport();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             SqlConnection connection = new SqlConnection(this.path);
            try
            {
                int tip;
                connection.Open();
                SqlDataAdapter adapter4 = new SqlDataAdapter($"SELECT IdTipaMachini FROM TipMachini where TipMachini = '{comboBox1.Text}'", connection);
                DataSet ds4 = new DataSet();
                adapter4.Fill(ds4);
                tip = ds4.Tables[0].Rows[0].Field<int>(ds4.Tables[0].Columns[0]);
                SqlCommand cmd = new SqlCommand($"UPDATE Transport SET NomerMachini='{textBox5.Text}', Marka='{textBox3.Text}', Gruzopodemnost='{textBox4.Text}', DataTechosmotra='{dateTimePicker1.Value}', IdTipaMachini={tip}  WHERE IdTransporta={dataGridView2.CurrentRow.Cells[0].Value}", connection);
                cmd.ExecuteNonQuery();
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
                LoadTransport();
            }
        }

        public void LoadTip()
        {
            SqlConnection connection = new SqlConnection(path);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TipMachini", connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.DisplayMember = "TipMachini";
                comboBox1.ValueMember = "IdTipaMachini";
                int i = 0;
                foreach (DataRow ns in ds.Tables[0].Rows)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i].Field<String>(ds.Tables[0].Columns[0]));
                    i++;
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


        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
               
                    connection.Open();
                    int Tip;
                    SqlDataAdapter adapter4 = new SqlDataAdapter($"SELECT IdTipaMachini FROM TipMachini WHERE TipMachini = '{comboBox1.Text}'", connection);
                    DataSet ds4 = new DataSet();
                    adapter4.Fill(ds4);
                    Tip = ds4.Tables[0].Rows[0].Field<int>(ds4.Tables[0].Columns[0]);
                    SqlCommand cmd = new SqlCommand($"INSERT INTO Transport (NomerMachini, Marka, Gruzopodemnost, DataTechosmotra, IdTipaMachini)"
                        + $"VALUES ('{textBox5.Text}', '{textBox3.Text}', '{textBox4.Text}', '{dateTimePicker1.Value}', '{Tip}')", connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Добавлено!");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                LoadTransport();
            }
        }

        private void Form3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            textBox5.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand($"DELETE FROM TechObslujivanie WHERE IdTechOb={dataGridView3.CurrentRow.Cells[0].Value}", connection);
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
                LoadTransport();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                
                    connection.Open();
                    int Transport, Sotr;
                    SqlDataAdapter adapter4 = new SqlDataAdapter($"SELECT IdTransporta FROM Transport WHERE Marka = '{comboBox2.Text}'", connection);
                    DataSet ds4 = new DataSet();
                    adapter4.Fill(ds4);
                    Transport = ds4.Tables[0].Rows[0].Field<int>(ds4.Tables[0].Columns[0]);
                    SqlDataAdapter adapter3 = new SqlDataAdapter($"SELECT IdSotrudnika FROM Sotrudniki WHERE Familia = '{comboBox3.Text}'", connection);
                    DataSet ds3 = new DataSet();
                    adapter3.Fill(ds3);
                    Sotr = ds3.Tables[0].Rows[0].Field<int>(ds3.Tables[0].Columns[0]);
                    SqlCommand cmd = new SqlCommand($"INSERT INTO TechObslujivanie (Technick, VidUslugi, DataObslujivania, Cena, IdTransporta)" +
                    $" VALUES ('{Sotr}','{textBox7.Text}', '{dateTimePicker2.Value}', '{textBox6.Text}', '{Transport}')", connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Добавлено!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                LoadTransport();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(this.path);
            try
            {
                connection.Open();
                int Transport, Sotr;
                SqlDataAdapter adapter4 = new SqlDataAdapter($"SELECT IdTransporta FROM Transport WHERE Marka = '{comboBox3.Text}'", connection);
                DataSet ds4 = new DataSet();
                adapter4.Fill(ds4);
                Transport = ds4.Tables[0].Rows[0].Field<int>(ds4.Tables[0].Columns[0]);
                SqlDataAdapter adapter3 = new SqlDataAdapter($"SELECT IdSotrudnika FROM Sotrudniki WHERE Familia = '{comboBox2.Text}'", connection);
                DataSet ds3 = new DataSet();
                adapter3.Fill(ds3);
                Sotr = ds3.Tables[0].Rows[0].Field<int>(ds3.Tables[0].Columns[0]);
                SqlCommand cmd = new SqlCommand($"UPDATE TechObslujivanie SET Technick='{Sotr}', VidUslugi='{textBox7.Text}', DataObslujivania='{dateTimePicker2.Value}', Cena='{textBox6.Text}', IdTransporta={Transport} WHERE IdTechOb={dataGridView3.CurrentRow.Cells[0].Value} ", connection);
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
                LoadTransport();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

}
