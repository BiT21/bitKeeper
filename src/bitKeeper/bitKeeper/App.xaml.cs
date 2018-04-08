using System;
using Unity;

using bitKeeper.Views;
using Xamarin.Forms;
using BiT21.FileService.IService;
using BiT21.FileService.Service;
using BiT21.EncryptDecryptLib.IService;
using BiT21.EncryptDecryptLib.Service;
using bitKeeper.Services;
using bitKeeper.Models;
using bitKeeper.ViewModels;

namespace bitKeeper
{
	public partial class App : Application
	{
        public IUnityContainer Container;

		public App ()
		{
			InitializeComponent();

            AppStyles();

            SetGlobalStyles();

            Container = new UnityContainer();

            var secret = new Secret();
            Container.RegisterInstance(secret);

            Container.RegisterSingleton<IFileService, FileServiceImplementation>();
            Container.RegisterSingleton<IEncryptDecrypt, EncryptDecrypt>();
            Container.RegisterType<IDataStore<Item>, DataStoreService>();

            MessagingCenter.Subscribe<LogonViewModel, Secret>(this, "LogonValid",  (obj, secretParam) =>
            {
                secret.SetSecret(secretParam);
                MainPage = new MainPage();
            });

            MessagingCenter.Subscribe<ItemsViewModel>(this, "WrongSecret", (obj) =>
            {
                var v =  Container.Resolve<LogonView>();
                (v.BindingContext as LogonViewModel).ErrorString = "Wrong Decryption Key. Please try again.";

                MainPage = v;
            });

            MainPage = Container.Resolve<LogonView>();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private void AppStyles()
        {
            Resources = Resources ?? new ResourceDictionary();
            
            Resources.Add("Primary",Color.FromHex("2196F3"));
            Resources.Add("PrimaryDark", Color.FromHex("1976D2"));
            Resources.Add("Accent", Color.FromHex("96d1ff"));
            Resources.Add("LightBackgroundColor", Color.FromHex("FAFAFA"));
            Resources.Add("DarkBackgroundColor", Color.FromHex("C0C0C0"));
            Resources.Add("MediumGrayTextColor", Color.FromHex("4d4d4d"));
            Resources.Add("LightTextColor", Color.FromHex("999999"));
        }

        private void SetGlobalStyles()
        {
            //Ref Device Styles : https://developer.xamarin.com/guides/xamarin-forms/user-interface/styles/device/

            Resources = Resources ?? new ResourceDictionary();


            //Colors
            var ColorBitGray = Color.FromHex("413E3E"); //Resources.Add("ColorBitGray", ColorBitGray);
            var ColorBitYellow = Color.FromHex("FCB124"); //Resources.Add("ColorBitYellow", ColorBitYellow);
            var ColorWhite = Color.White;
            var ColorBlack = Color.Black;

            //SubStyles
            var VisualElementStyleYellow = new Style(typeof(VisualElement))
            {
                Setters =
                {
                    new Setter{Property = Label.BackgroundColorProperty, Value = ColorBitYellow }
                }
            };
            var VisualElementStyleGray = new Style(typeof(VisualElement))
            {
                Setters =
                {
                    new Setter{Property = VisualElement.BackgroundColorProperty, Value = ColorBitGray }
                }
            };
            var VisualElementStyleWhite = new Style(typeof(VisualElement))
            {
                Setters =
                {
                    new Setter{Property = VisualElement.BackgroundColorProperty,Value = ColorWhite }
                }
            };

            var LabelStyleColorGray = new Style(typeof(Label))
            {
                BaseResourceKey = Device.Styles.ListItemDetailTextStyleKey,
                Setters =
                {
                   new Setter{Property = Label.TextColorProperty, Value = ColorBitGray }
                }

            }; //Resources.Add("StyleColorGrayYellow", StyleColorGrayYellow);

            var LabelStyleColorYellow = new Style(typeof(Label))
            {
                BaseResourceKey = Device.Styles.ListItemDetailTextStyleKey,
                Setters =
                {
                    new Setter{Property = Label.TextColorProperty, Value = ColorBitYellow }
                }

            }; //Resources.Add("StyleColorYellowGray", StyleColorYellowGray);
            var LabelStyleColorBlack = new Style(typeof(Label))
            {
                BaseResourceKey = Device.Styles.ListItemDetailTextStyleKey,
                Setters =
                {
                    new Setter{Property = Label.TextColorProperty, Value = ColorBlack }
                }
            };

            var CellStyleBlack = new Style(typeof(TextCell))
            {
                Setters =
                {
                    new Setter{Property=TextCell.TextColorProperty, Value = ColorBlack},
                    new Setter{Property=TextCell.DetailColorProperty, Value = ColorBlack}
                }
            };

            var CellStyleBlackYellow = new Style(typeof(TextCell))
            {
                Setters =
                {
                    new Setter{Property=TextCell.TextColorProperty, Value = ColorBlack},
                    new Setter{Property=TextCell.DetailColorProperty, Value = ColorBitYellow}
                }
            };
            var LabelStyleBitSubtitle = new Style(typeof(Label))
            {
                BaseResourceKey = Device.Styles.SubtitleStyleKey,
                //BasedOn = StyleColorGrayYellow
                Setters =
                {
                    new Setter{Property = Label.TextColorProperty, Value = ColorBitGray },
                }
            };
            var LabelStyleBitSamll = new Style(typeof(Label))
            {
                BaseResourceKey = Device.Styles.CaptionStyleKey,
                Setters =
                {
                    new Setter{Property = Label.TextColorProperty, Value = ColorBitGray },
                }
            };

            var StackLayoutStyleAppMenu = new Style(typeof(StackLayout))
            {
                BasedOn = VisualElementStyleGray
            };

            var NavigationPageStyle = new Style(typeof(NavigationPage))
            {
                Setters =
                {
                    new Setter{Property = NavigationPage.BarBackgroundColorProperty, Value = ColorBitYellow },
                    new Setter{Property = NavigationPage.BarTextColorProperty, Value = ColorBitGray}
                }
            };

            var ListViewStyleItems = new Style(typeof(ListView))
            {
                BasedOn = VisualElementStyleWhite,
                Setters =
                {
                    new Setter{Property = ListView.SeparatorColorProperty, Value = ColorBitYellow},
                    new Setter{Property = ListView.SeparatorVisibilityProperty, Value = true}
                }
            };

            //Add the required styles.
            Resources.Add("VisualElementStyleWhite", VisualElementStyleWhite);
            Resources.Add("VisualElementStyleGray", VisualElementStyleGray);
            Resources.Add("VisualElementStyleYellow", VisualElementStyleYellow);
            Resources.Add("LabelStyleAppTitle", LabelStyleBitSubtitle);
            Resources.Add("LabelStyleAppMenuItems", LabelStyleColorYellow);
            Resources.Add("LabelStyleBitSamll", LabelStyleBitSamll);

            Resources.Add("CellStyleBlackYellow", CellStyleBlackYellow);
            Resources.Add("CellStyleBlack", CellStyleBlack);

            Resources.Add("NavigationPageStyle", NavigationPageStyle);

            Resources.Add("ListViewStyleItems", ListViewStyleItems);

            Resources.Add("ColorBitYellow", ColorBitYellow);
            Resources.Add("ColorBitGray", ColorBitGray);
        }

        private void SetConstants()
        {
            Resources = Resources ?? new ResourceDictionary();

            Resources.Add("TextBit21Web", "BiT21.eu");
            Resources.Add("TextAppName", "BiT Keeper");
        }
    }
}
