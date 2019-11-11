using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

namespace IdentityAPI.Helper
{
    public class StorageAccountHelper
    {
        private string storageConnectionString;
        private CloudStorageAccount storageAccount;
       // private CloudBlobClient blobClient;
       // private CloudTableClient tableClient;
        private CloudQueueClient queueClient;

        public string StorageConnectionString
        {
            get { return storageConnectionString; }
            set
            {
                this.storageConnectionString = value;
                storageAccount = CloudStorageAccount.Parse(this.storageConnectionString);
            }
        }

        public async Task SendMessageAsync(string messageText, string queueName)
        {
            try
            {
                queueClient = storageAccount.CreateCloudQueueClient();
                var queue = queueClient.GetQueueReference(queueName);
                await queue.CreateIfNotExistsAsync();
                CloudQueueMessage qMessage = new CloudQueueMessage(messageText);
                await queue.AddMessageAsync(qMessage);
            }
            catch(Exception ex) { }
        }
    }
}
