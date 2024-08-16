using CryptoApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DCT_Crypto.Commands;
using DCT_Crypto.Models;
using DCT_Crypto.ViewModels.Services;

namespace DCT_Crypto.ViewModels
{
    public class ConvertViewModel : INotifyPropertyChanged
    {
        private Asset fromCrypto;
        private Asset toCrypto;
        private double amount;
        private double result;
        private List<Asset> allCryptos;
        public RelayCommand ConvertCommand { get; }
        private readonly ConversionService _conversionService;
        public Asset FromCrypto
        {
            get { return fromCrypto; }
            set
            {
                fromCrypto = value;
                OnPropertyChanged();
            }
        }

        public Asset ToCrypto
        {
            get { return toCrypto; }
            set
            {
                toCrypto = value;
                OnPropertyChanged();
            }
        }

        public double Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        public List<Asset> AllCryptos
        {
            get { return allCryptos; }
            set
            {
                allCryptos = value;
                OnPropertyChanged();
            }
        }

        public ConvertViewModel()
        {
            _conversionService = new ConversionService();

            ConvertCommand = new RelayCommand(async _ => await Convert());

            FetchAllCryptos();
        }

        private async void FetchAllCryptos()
        {
            try
            {
                CoinCapService requestService = new CoinCapService();
                AllCryptos = await requestService.GetCurrencyModels();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cryptocurrencies: {ex.Message}");
            }
        }

        private async Task Convert()
        {
            Result = await _conversionService.ConvertAsync(FromCrypto, ToCrypto, Amount);
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
