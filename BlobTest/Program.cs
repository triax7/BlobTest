using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
namespace BlobTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessAsync().GetAwaiter().GetResult();
            Console.ReadKey();
        }

        private static async Task ProcessAsync()
        {
            string storageConnectionString = "***REMOVED***";
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = cloudBlobClient
                    .GetContainerReference("emoteimagescontainer");
                string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localFileName = "blob_test.png";
                string sourceFile = Path.Combine(localPath, localFileName);
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                using (FileStream fs = File.OpenRead(sourceFile))
                {
                    await cloudBlockBlob.UploadFromStreamAsync(fs);
                }
            }
            else
            {
                Console.WriteLine("Press any key to exit the application.");
                Console.ReadLine();
            }
        }
        
    }
}
