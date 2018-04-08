using bitKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bitKeeper.ViewModels
{
    public class LogonViewModel : BaseViewModel
    {
        public LogonViewModel()
        {
            Title = "Logon";

            LogonCommand = new Command(() => 
            {
                var secret = new Secret(Password);

                if (!secret.IsValid())
                {
                    ErrorString = "Please type a secret";
                }
                else
                {
                    MessagingCenter.Send(this, "LogonValid", secret);
                }
            });
        }
        public Command LogonCommand { get; }

        string _Password = string.Empty;
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }

        string _ErrorString = string.Empty;
        public string ErrorString
        {
            get { return _ErrorString; }
            set
            {
                SetProperty(ref _ErrorString, value, nameof(ErrorString), () =>
                {
                    OnPropertyChanged(nameof(IsErrorVisible));
                });
            }
        }
        
        public bool IsErrorVisible {
            //get => !string.IsNullOrWhiteSpace(ErrorString); 
            get => true;
        }

    }
}
