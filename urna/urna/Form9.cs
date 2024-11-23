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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }


        private void CarregarResultados()
        {
            string connectionString = "Server=localhost; uid=root; pwd=; database=bolsonaro";

            try
            {
                using (MySqlConnection cone = new MySqlConnection(connectionString))
                {
                    cone.Open();

                    // Consulta SQL para contar os votos por candidato
                    string sql = @"
                SELECT c.Nome AS 'Candidato', c.Partido AS 'Partido', COUNT(e.voto) AS 'Votos'
                FROM candidatos c
                LEFT JOIN eleitores e ON c.Num_Voto = e.voto
                GROUP BY c.Num_Voto, c.Nome, c.Partido
                ORDER BY 'Votos' DESC";

                    using (MySqlCommand command = new MySqlCommand(sql, cone))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Vincula os dados ao DataGridView
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar resultados: " + ex.Message);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form9_Load(object sender, EventArgs e)
        {
            CarregarResultados();
        }
    }
}
