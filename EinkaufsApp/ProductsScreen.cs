﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EinkaufsApp
{
    public partial class ProductsScreen : Form
    {
        // Connection String - Verbindung zur Datenbank aufbauen (Verbindungszeichenfolge
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\onepi\OneDrive\Dokumente\EinkaufsApp.mdf;Integrated Security = True; Connect Timeout = 30");

        // Product Key vom Element was als letztes ausgewählt wurde
        private int lastSelectedProductKey;

        public ProductsScreen() //Konstuktor zum initialsieren aller Methoden 
        {
            // Startpunkt - Form wird geladen
            InitializeComponent();
            
            // Datenbank laden
            ShowProducts();
        }

        


        // Methoden zum Bearbeiten der Produkte (Speichern, Bearbeiten, Felder leeren & Löschen)
        private void btnProductSave_Click(object sender, EventArgs e)
        {
            // If-Abfrage zum Überprüfen ob alle Felder ausfegüllt worden sind
            if(textBoxProductName.Text == "" 
                || textBoxProductBrand.Text == ""
                || comboBoxProductCategory.Text == "" 
                || textBoxProductPrice.Text == "")
            {
                MessageBox.Show("Bitte alle Felder ausfühlen");

                return; // Code hört hier auf wenn kein Inhalt
            }

            // Werte in Datensatz umwandeln
            string productName = textBoxProductName.Text;  // Speicher Produktname in eine Variable
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            // Eingabe in Datenbank Objekt umwandeln 
            // Query mit dem Format wird an Methode Execute query übergeben und ausgeführt
            string query = string.Format("insert into Products values('{0}','{1}','{2}','{3}')", productName, productBrand, productCategory, productPrice);
            ExecuteQuery(query);

            // Felder leeren
            ClearAllFields();

            // Aktualisierte Datenbank
            ShowProducts();
        }


        // Methode zum Bearbeiten eines Datenbank-Objektes
        private void btnProductEdit_Click(object sender, EventArgs e)
        {

            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return; // Brich Code ab
            }

            string productName = textBoxProductName.Text;  
            string productBrand = textBoxProductBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("update Products set Name='{0}', Brand = '{1}', Category = '{2}', Price = '{3}' where Id={4}"
                , productName, productBrand, productCategory, productPrice, lastSelectedProductKey);
            ExecuteQuery(query);

            // Aktualisierte Datenbank
            ShowProducts();
        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            //Felder leeren
            ClearAllFields();
            
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            // Wenn kein Element ausgewählt also lastSelectedPK == 0 
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return; // Brich Code ab
            }



            // Query zum Löschen eines Datenbankobjektes anhand der ID
            string query = string.Format("delete from Products where Id={0}; ", lastSelectedProductKey);
            ExecuteQuery(query);

            // Felder des gelöschten Objektes leeren 
            ClearAllFields();

            // Aktualisierte Datenbank
            ShowProducts();
            
        }

        private void ExecuteQuery(string query)
        {
            // Eingabe in Datenbank Objekt umwandeln 
            databaseConnection.Open();
           
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);  // Was soll ausgeführt werden, wo ?
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();

        }

        // Methode zum Zeigen der Produkte:
        private void ShowProducts()
        {
            // Datenbank laden
            databaseConnection.Open();

            string query = "select * from Products";  // Querybefehl um alle Produkte zu erhalten

            // Über den Adapter kann der Befehl auf unsere Datenbank angewandt werden
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            // Produkte nach Selectbefehl in einen Dataset reinpacken
            var dataSet = new DataSet();

            sqlDataAdapter.Fill(dataSet); // Befühle mit Fillmethode mit den gewählten Produkten 

            // Definiere die Bindung zwische DatagridView mit Dataset für Darstellung
            productsDGV.DataSource = dataSet.Tables[0];   // Tabelle auswählen , da nur eins existiert = [0]

            productsDGV.Columns[0].Visible = false; // ID Attribut verstecken 

            databaseConnection.Close();
        }

        // Felder nach Eintrag leeren
        private void ClearAllFields()
        {
            textBoxProductName.Text = "";
            textBoxProductBrand.Text = "";
            textBoxProductPrice.Text = "";
            comboBoxProductCategory.SelectedIndex = 0;
            comboBoxProductCategory.SelectedItem = null;
        }


        // Durch CellContent Click Event kann genau auf das zu löschende Element geklickt werden
        private void productsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // SelectedRows ist die Zeile die ausgewählt wird [0], Zelle [1] = Name, VALUE = Wert und konvertieren 
            textBoxProductName.Text = productsDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxProductBrand.Text = productsDGV.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxProductCategory.Text = productsDGV.SelectedRows[0].Cells[3].Value.ToString();
            textBoxProductPrice.Text = productsDGV.SelectedRows[0].Cells[4].Value.ToString();

            // ID der zu löschenden Elements
            lastSelectedProductKey = (int)productsDGV.SelectedRows[0].Cells[0].Value;

        }
    }
}
