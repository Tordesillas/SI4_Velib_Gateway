using System;
using System.Linq;
using System.Windows.Forms;

namespace AppForm
{
    public partial class Form1 : Form
    {
        private Service1Client client;

        /// <summary>
        /// Constructs a Form1 object.
        /// Initializes the client.
        /// Updates the cities available.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            client = new Service1Client();

            foreach (string name in client.GetCitiesName())
            {
                listBox1.Items.Add(name);
            }
        }

        /// <summary>
        /// When a city is selected, displays the station list.
        /// </summary>
        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            foreach (string name in client.GetStationsFromCity(listBox1.SelectedItem.ToString()))
            {
                listBox2.Items.Add(name);
            }
        }

        /// <summary>
        /// When a station is selected, displays the station data.
        /// </summary>
        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            if (listBox2.SelectedItem == null)
            {
                return;
            }

            Int32.TryParse(listBox2.SelectedItem.ToString().Split('-')[0].Split('_')[0].Trim(), out int n);
            listBox3.Items.Add(client.GetStationOfCity(n, listBox1.SelectedItem.ToString()).Split('-').Last().Trim());
        }

        /// <summary>
        /// Displays a pop-up with some help.
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sélectionnez une ville dans la première colonne, puis une station dans la seconde.");
        }
    }
}
