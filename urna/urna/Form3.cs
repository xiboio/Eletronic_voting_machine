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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 formlog = new Form2();
            formlog.Show();
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            int idade;
            string senha, nome, cpf;

            nome = textBox1.Text;
            senha = textBox2.Text;
            cpf = textBox3.Text;
            idade = (int)numericUpDown1.Value;

            // Verifica se não tem nada vazio
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || idade <= 0 || idade < 16)
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente ou Você não é maior de 16 anos");
                return;
            }

            using (MySqlConnection cone = new MySqlConnection(connectionString))
            {
                try
                {
                    cone.Open();

                    string sql = "INSERT INTO eleitores (Cpf, Idade, Senha, Nome) VALUES (@Cpf, @Idade, @Senha, @Nome)";

                    // Esta parte é onde executa o insert
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@Cpf", MySqlDbType.VarChar).Value = cpf;
                        command.Parameters.Add("@Idade", MySqlDbType.Int32).Value = idade;
                        command.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = senha;
                        command.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = nome;

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
