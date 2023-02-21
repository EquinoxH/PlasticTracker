using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Plastic_Tracker
{
    /// <summary>
    /// Interaction logic for PlasticPanel.xaml
    /// </summary>
    public partial class PlasticPanel : UserControl
    {
        public PlasticPanel() {
            InitializeComponent();
        }

        public PlasticPanel(Plastic plastic) {
            InitializeComponent();
            showPlastic(plastic);
        }

        // Objects & Variables
        private string plasticName;

        // Custom Events

        public event EventHandler PlasticUpdated;

        // Events

        private void minusClicked(object sender, MouseButtonEventArgs e) {
            int? amount = GetIntWindow.getInt();
            if(amount != null) {
                PlasticManager.removeAmountOfPlastic(plasticName, (int)amount);
                PlasticUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private void setClicked(object sender, MouseButtonEventArgs e) {
            int? amount = GetIntWindow.getInt();
            if(amount != null) {
                PlasticManager.setAmountOfPlastic(plasticName, (int)amount);
                PlasticUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        // Public Functions

        public void showPlastic(Plastic plastic) {
            plasticName = plastic.name;
            nameLabel.Text = plastic.name;
            amountLabel.Text = plastic.remaining.ToString();
            sampleBorder.Background = new SolidColorBrush(plastic.colour);
        }
    }
}
