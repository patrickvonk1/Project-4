using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.IO;
using Project4App;
using System.Collections.Generic;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

namespace Project4App.Droid
{
    [Activity(Label = "Project4App", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity

    {
        private void ReadPickupLineFile(List<string> pickupLineList)
        {
            // Read the contents of our asset
            string content;
            AssetManager assets = this.Assets;

            using (StreamReader reader = new StreamReader(assets.Open("PickupLineFile.txt")))
            {
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();
                    pickupLineList.Add(line);
                }
            }
        }

        private void ReadMotivationLineFile(List<string> motivationLineList)
        {
            // Read the contents of our asset
            string content;
            AssetManager assets = this.Assets;

            using (StreamReader reader = new StreamReader(assets.Open("MotivationLineFile.txt")))
            {
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();
                    motivationLineList.Add(line);
                }
            }
        }

        private void ReadJokeLineFile(List<string> jokeLineList)
        {
            // Read the contents of our asset
            string content;
            AssetManager assets = this.Assets;

            using (StreamReader reader = new StreamReader(assets.Open("JokeLineFile.txt")))
            {
                while (reader.Peek() > 0)
                {
                    string line = reader.ReadLine();
                    jokeLineList.Add(line);
                }
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            List<string> pickupLineList = new List<string>();
            ReadPickupLineFile(pickupLineList);

            List<string> motivationLineList = new List<string>();
            ReadMotivationLineFile(motivationLineList);

            List<string> jokeLineList = new List<string>();
            ReadJokeLineFile(jokeLineList);

            App.pickupLines = new List<string>(pickupLineList);
            App.motivationLines = new List<string>(motivationLineList);
            App.jokeLines = new List<string>(jokeLineList);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public static Context context;
        protected override void OnResume()
        {
            context = this;
            base.OnResume();
        }

        public static void ChangeStatusBarColor(Color color)
        {
            FormsAppCompatActivity c = MainActivity.context as FormsAppCompatActivity;
            c.SetStatusBarColor(color);
        }
    }
}

