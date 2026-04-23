using System;
using System.Data;
using System.Data.SQLite;
using System.Text.Json;
using System.Windows.Forms;
using System.IO;

namespace Lab1
{
    public class Form1 : Form
    {
        private DataGridView dataGridView1 = new DataGridView();
        private DataGridView dataGridView2 = new DataGridView();

        private SQLiteConnection conn;
        private Config config;

        public Form1()
        {
            this.Text = "Master Detail";
            this.Width = 800;
            this.Height = 600;

            dataGridView1.Dock = DockStyle.Top;
            dataGridView1.Height = 250;

            dataGridView2.Dock = DockStyle.Fill;

            this.Controls.Add(dataGridView2);
            this.Controls.Add(dataGridView1);

            dataGridView1.DataError += (s, e) => { e.ThrowException = false; };
            dataGridView2.DataError += (s, e) => { e.ThrowException = false; };

            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadConfig();
                DatabaseInitializer.Initialize();

                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.db");
                conn = new SQLiteConnection($"Data Source={dbPath}");

                LoadMasterData();

                dataGridView1.SelectionChanged += MasterSelectionChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadConfig()
        {
            var json = File.ReadAllText("config.json");
            config = JsonSerializer.Deserialize<Config>(json);
        }

        void LoadMasterData()
        {
            var adapter = new SQLiteDataAdapter(config.Master.Query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void MasterSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            var id = dataGridView1.CurrentRow.Cells[config.Master.IdColumn].Value;

            var cmd = new SQLiteCommand(config.Detail.Query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            var adapter = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView2.DataSource = dt;
        }
    }
}