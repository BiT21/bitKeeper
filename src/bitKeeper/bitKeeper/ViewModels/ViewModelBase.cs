using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Prism.Logging;

namespace bitKeeper.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        private ILoggerFacade logger;


        //Properties
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        //ctor
        public ViewModelBase(INavigationService navigationService, ILoggerFacade logger)
        {
            NavigationService = navigationService;
            this.logger = logger;

            Title = GetType().Name;
        }

        //Commands
        private DelegateCommand<string> _navigateCommand;
        public DelegateCommand<string> NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(
                async (parameter) =>
                {
                    log($"Navigating to -> {parameter}");
                    await NavigationService.NavigateAsync(parameter);

                    //TODO:Add Command.Execute code here.          
                },
                (parameter) =>
                {
                    //TODO: Add Command.CanExecute code here
                    return true;
                }));

        void ExecuteCommandName(string parameter)
        {

        }

        bool CanExecuteCommandName(string parameter)
        {
            return true;
        }

        //INavigationAware
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        //IDestructible
        public virtual void Destroy()
        {
            
        }

        //Log
        void log (string message)
        {
            logger.Log(message, Category.Info, Priority.Medium);
        }
    }    
}
