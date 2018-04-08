using BiT21.EncryptDecryptLib.IService;
using bitKeeper.Models;
using BiT21.FileService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using bitKeeper.Exceptions;

namespace bitKeeper.Services
{
    public class DataStoreService : IDataStore<Item>
    {
        private IFileService fileService;
        private IEncryptDecrypt encryptDecrypt;
        private Secret secret;
        const string FILENAME = "Keys";

        public DataStoreService(IFileService fileService, IEncryptDecrypt encryptDecrypt, Secret secret )
        {
            this.fileService = fileService;
            fileService.SandboxTag = "bitKeeper";

            this.encryptDecrypt = encryptDecrypt;

            this.secret = secret;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            var items = await ReadAll();
            item.Id = Guid.NewGuid().ToString();
            items.Add(item);
            await SaveAll(items);

            return true;
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var items = await ReadAll();
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            await SaveAll(items);

            return true;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var items = await ReadAll();
            return (items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await ReadAll();            
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var items = await ReadAll();

            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            await SaveAll(items);

            return true;
        }

        //Private 
        private async Task<List<Item>> ReadAll()
        {
            var dataE =  await fileService.ReadTextFileAsync(FILENAME);

            return string.IsNullOrEmpty(dataE) ? new List<Item>() : await Decrypt(dataE);
            
        }
        private async Task SaveAll(List<Item> items)
        {
            if (items == null)
                await fileService.DeleteFileAsync(FILENAME);
            else
            {
                var dataE = await Encrypt(items);
                await fileService.SaveTextFileAsync(dataE,FILENAME);
            }
        }
        private async Task<string> Encrypt(List<Item> items)
        {
            try
            {
                string str = JsonConvert.SerializeObject(items);

                return await encryptDecrypt.EncryptStringAsync(secret.Password, str);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<List<Item>> Decrypt(string data)
        {
            try
            {
                var str = await encryptDecrypt.DecryptStringAsync(secret.Password, data);

                return JsonConvert.DeserializeObject<List<Item>>(str);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw new WrongEncryptionKeyException("Error trying to Decrypt Data",ex);
            }
        }
    }
}
