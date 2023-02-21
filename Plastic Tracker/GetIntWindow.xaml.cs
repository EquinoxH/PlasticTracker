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
    /// Interaction logic for GetIntWindow.xaml
    /// </summary>
    public partial class GetIntWindow : Window
    {
        public GetIntWindow() {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            amountBox.Focus();
        }

        private void keyUp(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter) processAmountBox();
        }

        private void confirmClicked(object sender, MouseButtonEventArgs e) {
            processAmountBox();
        }

        private void cancelClicked(object sender, MouseButtonEventArgs e) {
            result = null;
            Close();
        }

        // Private Functions

        private void processAmountBox() {
            if (int.TryParse(amountBox.Text, out int amount)) {
                result = amount;
                Close();
            }
            else {
                MessageBox.Show("Please enter a number in the amount box.");
            }
        }

        // Return Functions

        private int? result;
        private int? getResult() { return result; }
        public static int? getInt() {
            GetIntWindow window = new GetIntWindow();
            window.ShowDialog();
            return window.getResult();
        }
    }
}
