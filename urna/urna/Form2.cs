using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MySql.Data.MySqlClient;


namespace urna
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static class UsuarioLogado
        {
            public static string CPF { get; set; }// Forma mais fácil de jogar um valor para outra tela
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            string cpf = textBox1.Text;
            string senha = textBox2.Text;
            

            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {
                try
                {
                    cone.Open();



                    // Pega o CPF digitado pelo usuário na TextBox
                    string cpfDigitado = textBox1.Text.Trim();

                    
                    string sql_ss = "SELECT 1 FROM eleitores WHERE cpf = @cpf AND voto IS NOT NULL";
                    using (MySqlCommand comand = new MySqlCommand(sql_ss, cone))
                    {
                        // Adiciona o parâmetro CPF antes de executar a consulta
                        comand.Parameters.AddWithValue("@cpf", cpfDigitado);

                        
                        using (MySqlDataReader reader = comand.ExecuteReader())
                        {
                            if (reader.Read()) // Se houver resultado, significa que o usuário já votou
                            {
                                MessageBox.Show("Você já votou.");
                                return; 
                            }
                        }
                    }


                    // Verifica se existe algum eleitor com este Cpf e Senha
                    string sql = "SELECT 1 FROM eleitores WHERE Cpf = @Cpf AND Senha = @Senha";
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cpf;
                        command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = senha;

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {

                            
                            // Verifica se existe pelo menos 1 linha com aqueles valores
                            if (reader.HasRows)
                            {
                                MessageBox.Show("Login bem-sucedido!");
                                UsuarioLogado.CPF = textBox1.Text;
                                Form1 formurna = new Form1();
                                formurna.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("CPF ou senha inválidos. Tente novamente.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro: " + ex.Message);
                }
            }


        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Form3 formcad = new Form3();
                formcad.Show();
                this.Hide();
            }

            if (radioButton2.Checked == true)
            {
                Form5 formadm = new Form5();
                formadm.Show();
                this.Hide();
            }
            
            
        }

        private void Label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Font = new Font(label3.Font.FontFamily, 11);
        }

        private void Label3_MouseLeave(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font.FontFamily, 10);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
