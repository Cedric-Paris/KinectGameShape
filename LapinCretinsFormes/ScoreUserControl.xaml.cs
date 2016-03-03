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
    /// Logique d'interaction pour ScoreUserControl.xaml
    /// </summary>
    public partial class ScoreUserControl : UserControl
    {
        private MainWindow windowContainer;

        public ScoreUserControl(MainWindow container)
        {
            InitializeComponent();
            windowContainer = container;
        }
    }
}
