using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LapinCretinsFormes
{
    public interface IUserControlContainer
    {
        void LoadContent(UserControl content);

        void Close();
    }
}
