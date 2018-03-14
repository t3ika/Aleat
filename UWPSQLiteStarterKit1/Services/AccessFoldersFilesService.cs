using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Services.Interfaces;
using Windows.ApplicationModel;
using Windows.Storage;

namespace UWPSQLiteStarterKit1.Services
{
    public class AccessFoldersFilesService : IAccessFoldersFilesService
    {
        const String FilePathDb = @"Assets\Database\ExamsDB.db";
        const String FolderBase = "Bases";
        const String ExtensionDb = ".db";
        public async Task<List<string>> ListBasesPath()
        {
            List<string> listBasesNamePath = new List<string>();

            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(FilePathDb);

            //If in the folder's application the repository "Bases" doesn't exist, it will be created
            await ApplicationData.Current.LocalFolder.CreateFolderAsync(FolderBase, CreationCollisionOption.OpenIfExists);

            StorageFolder store = await ApplicationData.Current.LocalFolder.GetFolderAsync(FolderBase);

            //Get the files of the folder's application
            IReadOnlyList<StorageFile> localFiles = await store.GetFilesAsync();


            //Copy the database if it doesn't exist in the repository of the application
            if (localFiles.Count >= 1)
            {

                foreach (StorageFile item in localFiles)
                {
                    if (!item.FileType.Equals(ExtensionDb))
                    {
                        //Copy the database in the repository of the application
                        await file.CopyAsync(store, file.Name, NameCollisionOption.ReplaceExisting);
                    }
                }
            }
            else
            {
                //Copy the database in the repository of the application
                await file.CopyAsync(store, file.Name, NameCollisionOption.ReplaceExisting);
            }


            foreach (StorageFile item in await store.GetFilesAsync())
            {
                if (item.FileType.Equals(ExtensionDb))
                {
                    listBasesNamePath.Add(item.Path);
                }
            }

            return listBasesNamePath;
        }
    }
}
