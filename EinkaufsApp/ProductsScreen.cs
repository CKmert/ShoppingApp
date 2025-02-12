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
    public partial class ProductsScreen : Form
    {
        public ProductsScreen()
        {
            InitializeComponent();
        }

        // Methoden zum Bearbeiten der Produkte (Speichern, Bearbeiten, Felder leeren & Löschen)
        private void btnProductSave_Click(object sender, EventArgs e)
        {
            string productName = textBoxProductName.Text;  // Speicher Produktname in eine Variable

            // Werte in eine Datenbank speichern
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
