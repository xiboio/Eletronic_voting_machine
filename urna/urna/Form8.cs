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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cpf = textBox1.Text;
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            try
            {
                using (MySqlConnection cone = new MySqlConnection(connectionString))
                {
                    cone.Open();

                    // Consulta para verificar se o CPF existe
                    string checkSql = "SELECT COUNT(*) FROM eleitores WHERE Cpf = @cpf;";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkSql, cone))
                    {
                        checkCommand.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cpf;
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // Verifica se o CPF existe
                        if (count == 0)
                        {
                            MessageBox.Show("Inválido. Candidato não encontrado.");
                            return;
                        }
                    }

                    // Se o ID existe, executa a exclusão
                    string sql = "DELETE FROM eleitores WHERE Cpf = @cpf;";
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@cpf", MySqlDbType.VarChar).Value = cpf;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Eleitor excluído com sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 formadm = new Form5();
            formadm.Show();
            this.Hide();
        }
    }
}
