using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using DCT_Crypto.Commands;
using DCT_Crypto.ViewModels;

namespace DCT_Crypto
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

    }

}
