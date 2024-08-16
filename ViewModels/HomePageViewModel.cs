using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DCT_Crypto.Models;
using CryptoApp.Services;

namespace DCT_Crypto.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Asset> TopCryptos { get; set; }
   
        public HomePageViewModel()
        {
            InitializeData();
        }

        private async void InitializeData()
        {
            CoinCapService cryptoDataService = new CoinCapService();
            List<Asset> cryptos = await cryptoDataService.GetCurrencyModels();

            for (int i = 0; i < cryptos.Count; i++)
            {
                int counter = i + 1;
                cryptos[i].Rank = counter.ToString();
            }

            TopCryptos = new ObservableCollection<Asset>(cryptos.Take(10));
            OnPropertyChanged(nameof(TopCryptos));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
