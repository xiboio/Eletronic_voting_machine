using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static urna.Form2;

namespace urna
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nome, senha;
            nome = textBox1.Text;
            senha = textBox2.Text;

            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            if(radioButton1.Checked == true)
            {
                using (MySqlConnection cone = new MySqlConnection(connectionString))
                {

                    cone.Open();

                    string sql = "SELECT 1 FROM ADM WHERE nome = @nome AND senha = @senha";
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                        command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            // Verifica se existe pelo menos 1 linha com aqueles valores
                            if (reader.HasRows)
                            {
                                MessageBox.Show("Login bem-sucedido!");
                                Form4 formcand = new Form4();
                                formcand.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Nome ou senha inválidos. Tente novamente.");
                            }

                        }

                    }

                }
            }

            if (radioButton2.Checked == true)
            {
                using (MySqlConnection cone = new MySqlConnection(connectionString))
                {

                    cone.Open();

                    string sql = "SELECT 1 FROM ADM WHERE nome = @nome AND senha = @senha";
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                        command.Parameters.Add("@senha", MySqlDbType.VarChar).Value = senha;

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            // Verifica se existe pelo menos 1 linha com aqueles valores
                            if (reader.HasRows)
                            {
                                MessageBox.Show("Login bem-sucedido!");
                                Form6 formdel = new Form6();
                                formdel.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Nome ou senha inválidos. Tente novamente.");
                            }

                        }

                    }

                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 formlog = new Form2();
            formlog.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {

                cone.Open();

                string sql_up = "UPDATE votacao SET atividade = @valor";
                using (MySqlCommand command_up = new MySqlCommand(sql_up, cone))
                {
                    command_up.Parameters.AddWithValue("@valor", 1);
                    command_up.ExecuteNonQuery();

                    MessageBox.Show("Votação ativida com sucesso.");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {

                cone.Open();

                string sql_up = "UPDATE votacao SET atividade = @valor";
                using (MySqlCommand command_up = new MySqlCommand(sql_up, cone))
                {
                    command_up.Parameters.AddWithValue("@valor", 0);
                    command_up.ExecuteNonQuery();

                    MessageBox.Show("Votação desativada com sucesso.");
                }
            }
        }
    }
}
