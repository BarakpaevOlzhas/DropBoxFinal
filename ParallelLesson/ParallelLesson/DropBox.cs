using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLesson
{
    public class DropBox
    {
        private DropboxClient dbx;
        private string currentDirectory;
        private string prevDirectory;

        public string CurrentDirectory
        {
            get { return currentDirectory; }

            set
            {
                prevDirectory = currentDirectory;
                currentDirectory = value;
            }
        }

        public string PrevDirectory
        {
            get { return prevDirectory; }
        }

        public DropBox()
        {
            dbx = new DropboxClient("hUx4pJ7GjEAAAAAAAAAAEBWlj7mviqCytysAEMRwgqHVB15-0qCkgf9UPiBSlFoj");
            currentDirectory = "";
        }

        public async Task<string> Run()
        {
            var full = await dbx.Users.GetCurrentAccountAsync();

            return full.Name.DisplayName;
        }

        public async Task<List<string>> ListRootFolder(string folder = "")
        {
            var list = await dbx.Files.ListFolderAsync(folder);

            var allFiles = new List<string>();
            foreach (var item in list.Entries.Where(i => i.IsFile))
            {
                allFiles.Add(item.Name);
            }

            return allFiles;
        }

        public async Task Upload(string fileName, string folder, string file)
        {
            using (var mem = new MemoryStream(File.ReadAllBytes(file)))
            {
                var updated = await dbx.Files.UploadAsync(
                 folder + "/" + fileName,
                 WriteMode.Overwrite.Instance,
                 body: mem);
            }            
        }
    }
}