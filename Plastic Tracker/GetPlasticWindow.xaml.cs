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
using System.Windows.Shapes;

namespace Plastic_Tracker
{
    /// <summary>
    /// Interaction logic for GetPlasticWindow.xaml
    /// </summary>
    public partial class GetPlasticWindow : Window
    {
        public GetPlasticWindow() {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
        }

        // Objects & Variables
        private Color colour;

        // Events

        private void changeClicked(object sender, MouseButtonEventArgs e) {
            System.Windows.Forms.ColorDialog colourPicker = new System.Windows.Forms.ColorDialog();
            if(colourPicker.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                colour = Color.FromRgb(colourPicker.Color.R, colourPicker.Color.G, colourPicker.Color.B);
                sampleBorder.Background = new  SolidColorBrush(colour);
            }
        }

        private void confirmClicked(object sender, MouseButtonEventArgs e) {
            if (string.IsNullOrEmpty(nameBox.Text)) {
                MessageBox.Show("Please enter a name first.");
                return;
            }

            if(colour == null) {
                MessageBox.Show("Please select a colour first.");
                return;
            }

            if (!int.TryParse(amountBox.Text, out int amount)) {
                MessageBox.Show("Please enter a number in the amount box.");
                return;
            }

            result = new Plastic() {
                name = nameBox.Text,
                colour = colour,
                remaining = int.Parse(amountBox.Text)
            };
            Close();
        }

        private void cancelClicked(object sender, MouseButtonEventArgs e) {
            result = null;
            Close();
        }

        // Return Functions

        private Plastic result;
        private Plastic getResult() { return result; }
        public static Plastic getPlastic() {
            GetPlasticWindow window = new GetPlasticWindow();
            window.ShowDialog();
            return window.getResult();
        }
    }
}
