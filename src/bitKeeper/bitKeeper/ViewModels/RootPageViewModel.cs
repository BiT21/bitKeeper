using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bitKeeper.ViewModels
{
	public class RootPageViewModel : ViewModelBase
	{
        public RootPageViewModel(Prism.Navigation.INavigationService navigationService, Prism.Logging.ILoggerFacade logger)
            : base(navigationService, logger)
        {

        }
	}
}
