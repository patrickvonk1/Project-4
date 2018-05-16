using System.IO;
using Xamarin.Forms;
using Project4App.Droid;

[assembly: Dependency(typeof(LocalFileHelper))]
namespace Project4App.Droid
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}