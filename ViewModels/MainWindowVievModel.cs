using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DCT_Crypto.Commands;

namespace DCT_Crypto.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public RelayCommand HomeCommand { get; }
        public RelayCommand ListCommand { get; }
        public RelayCommand DetailsCommand { get; }
        public RelayCommand SwitchThemeCommand { get; }

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            CurrentPage = new HomePage();
            HomeCommand = new RelayCommand(_ => NavigateTo(new HomePage()));
            ListCommand = new RelayCommand(_ => NavigateTo(new ConvertPage()));
            DetailsCommand = new RelayCommand(_ => NavigateTo(new DetailsPage()));
            SwitchThemeCommand = new RelayCommand(_ => SwitchTheme());
        }

        private void NavigateTo(Page page)
        {
            CurrentPage = page;
        }

        private void SwitchTheme()
        {
            const string lightTheme = "Themes/LightTheme.xaml";
            const string darkTheme = "Themes/DarkTheme.xaml";

            var currentTheme = Application.Current.Resources.MergedDictionaries
                                  .FirstOrDefault(d => d.Source.ToString().Contains(lightTheme) ||
                                                       d.Source.ToString().Contains(darkTheme));

            if (currentTheme != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(currentTheme);
            }

            string newTheme = currentTheme?.Source.ToString().Contains(lightTheme) == true
                ? darkTheme
                : lightTheme;

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri(newTheme, UriKind.Relative)
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
