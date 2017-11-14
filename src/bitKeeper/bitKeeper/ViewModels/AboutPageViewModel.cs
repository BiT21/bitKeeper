using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bitKeeper.ViewModels
{
	public class AboutPageViewModel : ViewModelBase
	{
        public AboutPageViewModel(Prism.Navigation.INavigationService navigationService, Prism.Logging.ILoggerFacade logger) 
            : base(navigationService, logger)
        {

        }
	}
}
