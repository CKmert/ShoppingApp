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
    public partial class LoadingScreen : Form
    {
        private int loadingBarValue; // Variable für den Value der LoadingBar = Timervalue

        public LoadingScreen()  // Konstuktor 
        {
            InitializeComponent();
        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            LoadingbarTimer.Start(); // Starte den Timer beim Öffnen der App
        }

        private void LoadingbarTimer_Tick(object sender, EventArgs e) // Zeitintervall festlegen - Methode
        {
            loadingBarValue += 10; // Erhöhe den Wert um 10 (Passe die Tick rate an)

            lblLoadingProgress.Text = loadingBarValue.ToString() + "%"; // Wert der UI Komponente mit Namen ansprechen
            // Der Label erhält den loadingBarValue Wert (konvertiert)

            if (loadingBarValue >= LoadingProgressbar.Maximum) // Wenn der Wert 100 (Maximum der Bar) erreicht ist stoppe den Timer
            {
                LoadingbarTimer.Stop();
            }

            LoadingProgressbar.Value = loadingBarValue; // Value nimmt den Wert von loadingBarValue an 
        }

        
    }
}
