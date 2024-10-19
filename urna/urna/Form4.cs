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

namespace urna
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            int idade, num_vt;
            string partido, nome, cargo;
            byte[] foto = null; // Para carregar uma imagem

            nome = textBox3.Text;
            partido = textBox2.Text;
            cargo = textBox1.Text;
            idade = (int)numericUpDown1.Value;
            num_vt = (int)numericUpDown2.Value;


            // Verifica se não tem nada vazio ou inválido
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(partido) || string.IsNullOrWhiteSpace(cargo) || idade < 18 || num_vt <= 0)
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente ou Você não é maior de 18 anos");
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foto = System.IO.File.ReadAllBytes(openFileDialog.FileName);//lê todos os bytes do arquivo especificado
                }
                else
                {
                    MessageBox.Show("Nenhuma imagem selecionada.");
                    return;
                }
            }

            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {
                try
                {
                    cone.Open();

                    string sql = "INSERT INTO candidatos (Num_Voto, Nome, Idade, Partido, Cargo, Foto) VALUES (@Num_Voto, @Nome, @Idade, @Partido, @Cargo, @Foto)";

                    // Esta parte é onde executa o insert
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@Num_Voto", MySqlDbType.Int32).Value = num_vt;
                        command.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = nome;
                        command.Parameters.Add("@Idade", MySqlDbType.Int32).Value = idade;
                        command.Parameters.Add("@Partido", MySqlDbType.VarChar).Value = partido;
                        command.Parameters.Add("@Cargo", MySqlDbType.VarChar).Value = cargo;
                        command.Parameters.Add("@Foto", MySqlDbType.LongBlob).Value = foto;

                        command.ExecuteNonQuery();
                        MessageBox.Show("Cadastro realizado com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 formlog = new Form2();
            formlog.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            byte[] foto = null; // Para carregar uma imagem

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foto = System.IO.File.ReadAllBytes(openFileDialog.FileName);//lê todos os bytes do arquivo especificado

                    pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    MessageBox.Show("Nenhuma imagem selecionada.");
                    return;
                }
            }
        }
    }
}
