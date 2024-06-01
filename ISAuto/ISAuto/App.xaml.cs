using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ISAuto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
     
        private App()
        {
            DirectoryResourse();
        }


        private void DirectoryResourse()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string sourceDirectory = System.IO.Path.GetFullPath(@"..\..\..\Resourse");

            string destinationDirectory = @"C:\AutoResourse";

            if (FoldersDiffer(sourceDirectory, destinationDirectory))
                DeleteDirectory(destinationDirectory);

            if (Directory.Exists(destinationDirectory))
                return;
            if (Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
                CopyDirectory(sourceDirectory, destinationDirectory);
            }
        }
        private bool FoldersDiffer(string folder1Path, string folder2Path)
        {
            DirectoryInfo dir1 = new DirectoryInfo(folder1Path);
            DirectoryInfo dir2 = new DirectoryInfo(folder2Path);


            var files1 = dir1.GetFiles().Select(f => f.Name).ToList();
            var files2 = dir2.GetFiles().Select(f => f.Name).ToList();

            dir1.GetDirectories().ToList().ForEach(d => files1 = files1.Concat(d.GetFiles().Select(f => f.Name).ToList()).ToList());
            dir2.GetDirectories().ToList().ForEach(d => files2 = files2.Concat(d.GetFiles().Select(f => f.Name).ToList()).ToList());


            if (files1.Count != files2.Count)
                return true; 
            

            foreach (var fileName in files1)
            {
                if (!files2.Contains(fileName))
                    return true; 
            }
            return false;
        }

        private void DeleteDirectory(string directoryPath)
        {

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subDirectory in directoryInfo.GetDirectories())
            {
                subDirectory.Delete(true);
            }

            Directory.Delete(directoryPath);
        }


        private void CopyDirectory(string sourceDir, string destinationDir)
        {
            foreach (string dirPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourceDir, destinationDir));
            }

            foreach (string newPath in Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourceDir, destinationDir), true);
            }
        }
    }

}
