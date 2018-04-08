using bitKeeper.Models;
using bitKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bitKeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogonView : ContentPage
	{
        public LogonView (LogonViewModel viewModel)
		{
			InitializeComponent ();

            BindingContext = viewModel;
        }
    }
}