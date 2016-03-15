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
    /// Logique d'interaction pour InstructionsUserControl.xaml
    /// </summary>
    public partial class InstructionsUserControl : UserControl
    {
        private IUserControlContainer windowContainer;
        private GameManager gameManager;

        public InstructionsUserControl(IUserControlContainer container, GameManager gameManager)
        {
            InitializeComponent();
            this.gameManager = gameManager;
            windowContainer = container;
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new MainMenuUserControl(windowContainer, gameManager));
        }

        private void GameButtonClick(object sender, RoutedEventArgs e)
        {
            windowContainer.LoadContent(new GameUserControl(windowContainer, gameManager));
        }
    }
}
