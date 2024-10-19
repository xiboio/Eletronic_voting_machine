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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal id = numericUpDown1.Value;
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            try
            {
                using (MySqlConnection cone = new MySqlConnection(connectionString))
                {
                    cone.Open();

                    // Consulta para verificar se o ID existe
                    string checkSql = "SELECT COUNT(*) FROM candidatos WHERE Id_Cand = @id_cand;";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkSql, cone))
                    {
                        checkCommand.Parameters.Add("@id_cand", MySqlDbType.VarChar).Value = id;
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // Verifica se o ID existe
                        if (count == 0)
                        {
                            MessageBox.Show("ID inválido. Candidato não encontrado.");
                            return;
                        }
                    }

                    // Se o ID existe, executa a exclusão
                    string sql = "DELETE FROM candidatos WHERE Id_Cand = @id_cand;";
                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        command.Parameters.Add("@id_cand", MySqlDbType.VarChar).Value = id;
                        command.ExecuteNonQuery();

                        MessageBox.Show("Candidato excluído com sucesso");
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
            Form2 formlog = new Form2();
            formlog.Show();
            this.Hide();
        }
    }
}
