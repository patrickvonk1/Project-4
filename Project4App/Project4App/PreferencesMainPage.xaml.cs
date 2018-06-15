using DatabaseAssembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project4App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreferencesMainPage : ContentPage
	{
		public PreferencesMainPage ()
		{
			InitializeComponent ();
		}

        private async void SavePreferencesButton_Clicked(object sender, EventArgs e)
        {
            if (AppThemePicker.SelectedItem == null || AttractedGenderPicker.SelectedItem == null)
            {
                return;//The user must fill in all fields
            }

            Preferences preferences = new Preferences();

            if (AppThemePicker.SelectedItem != null)
            {
                preferences.AppTheme = (AppTheme)Enum.Parse(typeof(AppTheme), (string)AppThemePicker.SelectedItem);
            }

            if (AttractedGenderPicker.SelectedItem != null)
            {
                preferences.AttractedGender = (AttractedGender)Enum.Parse(typeof(AttractedGender), (string)AttractedGenderPicker.SelectedItem);
            }

            await App.Database.SavePreferencesAsync(preferences);
        }

        private void AppThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppTheme appTheme;
            if (Enum.TryParse((string)AppThemePicker.SelectedItem, out appTheme))
            {
                switch (appTheme)
                {
                    case AppTheme.Donker:                    
                        App.Current.Resources["backgroundColor"] = Color.FromHex("#212121");
                        App.Current.Resources["lineTextColor"] = Color.White;
                        App.Current.Resources["tabBarColor"] = Color.FromHex("#464448");
                        break;
                    case AppTheme.Licht:
                        App.Current.Resources["backgroundColor"] = Color.FromHex("#B2DFDB");
                        App.Current.Resources["lineTextColor"] = Color.Black;
                        App.Current.Resources["tabBarColor"] = Color.FromHex("#E0F2F1");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}