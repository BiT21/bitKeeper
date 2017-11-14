using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace bitKeeper.ViewModels
{
	public class KeyFormPageViewModel : ViewModelBase
	{
        public KeyFormPageViewModel(Prism.Navigation.INavigationService navigationService, Prism.Logging.ILoggerFacade logger) 
            : base(navigationService, logger)
        {

        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Title = $"Key[{parameters["id"]}]";
        }
    }
}
