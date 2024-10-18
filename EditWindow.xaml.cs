using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace Dolgozat
{
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
