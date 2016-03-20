using System.Windows;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Logique d'interaction pour LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow(string chargingReason = "Chargement . . .", string title = "Chargement...")
        {
            InitializeComponent();
            ChargingReasonTextBlock.Text = chargingReason;
            WindowTitle.Text = title;
        }
    }
}
