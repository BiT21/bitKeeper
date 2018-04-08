using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using bitKeeper.Models;
using bitKeeper.Views;
using bitKeeper.Services;
using bitKeeper.Exceptions;

namespace bitKeeper.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private IDataStore<Item> dataStore;

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel(IDataStore<Item> dataStore)
        {
            this.dataStore = dataStore; 
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item);
                await dataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await dataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (WrongEncryptionKeyException wx)
            {
                Debug.WriteLine(wx);
                MessagingCenter.Send(this, "WrongSecret");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}