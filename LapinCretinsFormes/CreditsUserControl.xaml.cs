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

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour CreditsUserControl.xaml
    /// </summary>
    public partial class CreditsUserControl : UserControl
    {
        private MainWindow windowContainer;

        public CreditsUserControl(MainWindow container)
        {
            InitializeComponent();
            windowContainer = container;
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer));
        }
    }
}
