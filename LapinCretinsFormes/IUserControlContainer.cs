using System.Windows.Controls;

namespace LapinCretinsFormes
{
    public interface IUserControlContainer
    {
        void LoadContent(UserControl content);
        void Close();
    }
}
