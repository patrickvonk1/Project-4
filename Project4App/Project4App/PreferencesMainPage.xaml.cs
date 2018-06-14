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
                        App.Current.Resources["backgroundColor"] = new Color(0.61960784313, 0.00392156862, 0.35294117647);
                        App.Current.Resources["lineTextColor"] = new Color(158, 1, 90);
                        break;
                    case AppTheme.Licht:
                        App.Current.Resources["backgroundColor"] = Color.White;
                        App.Current.Resources["lineTextColor"] = Color.Black;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}