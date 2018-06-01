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

namespace Project4App.Droid
{
    [Activity(Label = "Project4App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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

        protected override void OnCreate(Bundle bundle)
        {
            List<string> pickupLineList = new List<string>();
            ReadPickupLineFile(pickupLineList);

            App.pickupLines = new List<string>(pickupLineList);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

