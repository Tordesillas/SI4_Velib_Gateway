using System;
using System.Linq;
using System.Threading.Tasks;
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
        private async void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedItem = listBox1.SelectedItem.ToString();
            string[] stationNames = await Task.Run(() => client.GetStationsFromCity(selectedItem));

            listBox2.Items.Clear();
            listBox3.Items.Clear();

            foreach (string name in stationNames)
            {
                listBox2.Items.Add(name);
            }
        }

        /// <summary>
        /// When a station is selected, displays the station data.
        /// </summary>
        private async void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            Int32.TryParse(listBox2.SelectedItem.ToString().Split('-')[0].Split('_')[0].Trim(), out int n);
            string selectedItem = listBox1.SelectedItem.ToString();
            string answer = await Task.Run(() => client.GetStationOfCity(n, selectedItem).Split('-').Last().Trim());

            listBox3.Items.Clear();
            if (listBox2.SelectedItem == null)
            {
                return;
            }

            listBox3.Items.Add(answer);
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
