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
        public static Picker AppThemePickerVar;
        public static Picker AppGenderPickerVar;

        public PreferencesMainPage ()
		{
			InitializeComponent ();

            ToolbarItem toolbarItem = new ToolbarItem() { Text = "?" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new HelpPage() { });
            };

            ToolbarItems.Add(toolbarItem);

            AppThemePickerVar = AppThemePicker;
            AppGenderPickerVar = AttractedGenderPicker;
        }

        public static async void ChangeAttractedGender(AttractedGender newAttractedGender)
        {
            App.Preferences.AttractedGender = newAttractedGender;
            await App.Database.SavePreferencesAsync(App.Preferences);

            AppGenderPickerVar.SelectedItem = newAttractedGender.ToString();
        }

        public static async void ChangeTheme(AppTheme newAppTheme)
        {
            switch (newAppTheme)
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

            App.Preferences.AppTheme = newAppTheme;
            await App.Database.SavePreferencesAsync(App.Preferences);

            AppThemePickerVar.SelectedItem = newAppTheme.ToString();
        }

        private void AppThemePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppTheme appTheme;
            if (Enum.TryParse((string)AppThemePicker.SelectedItem, out appTheme))
            {
                ChangeTheme(appTheme);
            }
        }

        private void AttractedGenderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttractedGender attractedGender;
            if (Enum.TryParse((string)AttractedGenderPicker.SelectedItem, out attractedGender))
            {
                ChangeAttractedGender(attractedGender);
            }
        }
    }
}