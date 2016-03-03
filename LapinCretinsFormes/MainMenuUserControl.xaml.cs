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
    /// Logique d'interaction pour MainMenuUserControl.xaml
    /// </summary>
    public partial class MainMenuUserControl : UserControl
    {
        private MainWindow windowContainer;

        public MainMenuUserControl(MainWindow container)
        {
            InitializeComponent();
            windowContainer = container;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.Close();
        }

        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new InstructionsUserControl(windowContainer));
        }

        private void CreditsButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new CreditsUserControl(windowContainer));
        }
    }
}
