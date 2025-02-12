using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EinkaufsApp
{
    public partial class MainMenuScreen : Form
    {
        public MainMenuScreen()
        {
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // Neue Instanz vom ProductScreen erstellen damit dieser angezeigt werden kann
            ProductsScreen productsScreen = new ProductsScreen();   
            productsScreen.Show();
            // Schließe MainMenuScreen mit this, da wir uns in der MainMenuScreen Klasse befinden
            this.Hide();
        }
    }
}
