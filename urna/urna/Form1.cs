using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using static urna.Form2;

namespace urna
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        string Num_Cand;
        

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }
        private void Button11_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";
            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {
                cone.Open();

                try
                {
                    string cpfUsuario = UsuarioLogado.CPF;
                    int branco = 0;

                    DialogResult resultado = MessageBox.Show("Tem certeza de que deseja votar Branco?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {

                        string sql_update = "UPDATE eleitores SET voto = @Num_Voto WHERE cpf = @cpf";
                        using (MySqlCommand command_in = new MySqlCommand(sql_update, cone))
                        {
                            command_in.Parameters.AddWithValue("@Num_Voto", branco);
                            command_in.Parameters.AddWithValue("@cpf", cpfUsuario);
                            command_in.ExecuteNonQuery(); // Insere o voto no banco de dados

                            MessageBox.Show("Seu voto foi computado!");
                            Form2 formlog = new Form2();
                            formlog.Show();
                            this.Hide();
                        }

                        
                        
                    }

                }
                            
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro: " + ex.Message);
                    }
            }

                
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label4.Text = "";
            label6.Text = "";
            label7.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void Button13_Click(object sender, EventArgs e)
        {

            string numeroCandidato = textBox1.Text;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, digite um número de candidato.");
                return;
            }

            
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";
            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {
                try
                {
                    cone.Open();

                    string cpfUsuario = UsuarioLogado.CPF;

                    if (textBox1.Text == "00")
                    {
                        int nulo = 00;

                        DialogResult resultad = MessageBox.Show("Tem certeza de que deseja votar nulo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        string sql_update = "UPDATE eleitores SET voto = @Num_Voto WHERE cpf = @cpf";
                        using (MySqlCommand command_in = new MySqlCommand(sql_update, cone))
                        {
                            command_in.Parameters.AddWithValue("@Num_Voto", nulo);
                            command_in.Parameters.AddWithValue("@cpf", cpfUsuario);
                            command_in.ExecuteNonQuery(); // Insere o voto no banco de dados

                            MessageBox.Show("Seu voto foi computado!");
                            Form2 formlog = new Form2();
                            formlog.Show();
                            this.Close();
                            return;
                        }
                    }

                    DialogResult resultado = MessageBox.Show("Tem certeza de que deseja votar neste candidato?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        

                        // Consulta SQL para buscar as informações do candidato
                        string sql = "SELECT Nome, Partido, Cargo, Num_Voto FROM candidatos WHERE Num_Voto = @Num_Voto";
                        using (MySqlCommand command = new MySqlCommand(sql, cone))
                        {
                            command.Parameters.AddWithValue("@Num_Voto", numeroCandidato);

                            // Executa a consulta para verificar se o candidato existe
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {

                                
                                if (reader.HasRows) // Se o candidato foi encontrado
                                {
                                    reader.Read(); // Lê a linha do candidato
                                                   
                                    reader.Close();
                                    // Consulta SQL para registrar o voto
                                    string sql_update = "UPDATE eleitores SET voto = @Num_Voto WHERE cpf = @cpf";
                                    using (MySqlCommand command_up = new MySqlCommand(sql_update, cone))
                                    {
                                        command_up.Parameters.AddWithValue("@Num_Voto", numeroCandidato);
                                        command_up.Parameters.AddWithValue("@cpf", cpfUsuario);
                                        command_up.ExecuteNonQuery(); // Insere o voto no banco de dados

                                        MessageBox.Show("Seu voto foi computado!");
                                        Form2 formlog = new Form2();
                                        formlog.Show();
                                        this.Hide();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Candidato não encontrado!");
                                }
                            }
                        }
                    }



                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro: " + ex.Message);
                }
            }

            // Limpa o número do candidato digitado para a próxima tentativa
            Num_Cand = "";
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

            string numeroCandidato = textBox1.Text;

            // Verifica se o número não está vazio
            if (string.IsNullOrWhiteSpace(numeroCandidato))
            {
                return; // Retorna se o número estiver vazio
            }

            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";
            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {
                try
                {
                    cone.Open();

                    // Consulta SQL para buscar as informações do candidato
                    string sql = "SELECT Nome, Partido, Cargo, Num_Voto, Foto FROM candidatos WHERE Num_Voto = @Num_Voto";
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.AddWithValue("@Num_Voto", numeroCandidato);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Verifica se há dados disponíveis
                            if (reader.Read())
                            {
                                // Preenche as labels com as informações do candidato
                                label6.Text = reader["Nome"].ToString();
                                label7.Text = reader["Partido"].ToString();
                                label4.Text = reader["Cargo"].ToString();
                                

                                // Verifica se a foto está presente e a carrega
                                if (reader["Foto"] != DBNull.Value)
                                {
                                    byte[] fotoBytes = (byte[])reader["Foto"];
                                    using (MemoryStream ms = new MemoryStream(fotoBytes))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Ajusta o modo de exibição da imagem
                                    }
                                }
                            }
                            else
                            {
                                // Limpa as labels se nenhum candidato for encontrado
                                label6.Text = "";
                                label7.Text = "";
                                label4.Text = "";
                                pictureBox1.Image = null; // Limpa a imagem se não houver candidato
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

        private void Button12_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label4.Text = "";
            label6.Text = "";
            label7.Text = "";
            pictureBox1.Image = null;
        }
    }
    }



    

