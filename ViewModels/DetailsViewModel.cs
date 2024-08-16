using CryptoApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCT_Crypto.Models;

namespace DCT_Crypto
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        private Asset selectedCrypto;
        private ObservableCollection<Asset> cryptos;
        private readonly CoinCapService coinCapService;

        public ObservableCollection<Asset> Cryptos
        {
            get { return cryptos; }
            set
            {
                cryptos = value;
                OnPropertyChanged();
            }
        }

        public Asset SelectedCrypto
        {
            get { return selectedCrypto; }
            set
            {
                selectedCrypto = value;
                OnPropertyChanged();
            }
        }

        public DetailsViewModel()
        {
            coinCapService = new CoinCapService();
            GetCryptosAsync();
        }

        private async void GetCryptosAsync()
        {
            Cryptos = new ObservableCollection<Asset>(await coinCapService.GetCurrencyModels());
            OnPropertyChanged(nameof(Cryptos));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
