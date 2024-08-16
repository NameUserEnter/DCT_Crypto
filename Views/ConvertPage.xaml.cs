using System.Windows.Controls;
using DCT_Crypto.ViewModels;

namespace DCT_Crypto
{
    public partial class ConvertPage : Page
    {
        public ConvertPage()
        {
            InitializeComponent();

            DataContext = new ConvertViewModel();
        }

    }
}
