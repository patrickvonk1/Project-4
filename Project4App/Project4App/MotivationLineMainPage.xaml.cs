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
	public partial class MotivationLineMainPage : ContentPage
	{
        MotivationLine currentMotivationLine;
        public MotivationLineMainPage ()
		{
			InitializeComponent ();
            ToolbarItem toolbarItem = new ToolbarItem() { Text = "+" };
            ToolbarItem toolbarItem2 = new ToolbarItem() { Text = "?" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MotivationLineCreatorPage() { BindingContext = new MotivationLine() });
            };

            toolbarItem2.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new HelpPage() { });
            };

            ToolbarItems.Add(toolbarItem);      ToolbarItems.Add(toolbarItem2);

            var profileTapRecognizer = new TapGestureRecognizer
            {
                TappedCallback = async (v, o) => {
                    await TappedImage();
                },

                NumberOfTapsRequired = 1
            };
            
            image.GestureRecognizers.Add(profileTapRecognizer);
        }

        private async Task TappedImage()
        {
            string motivationLineType = "";
            if (MotivationLineTypePicker != null && MotivationLineTypePicker.SelectedItem != null)
            {
                motivationLineType = (string)MotivationLineTypePicker.SelectedItem;
            }

            MotivationLine filteredMotivationLine = await App.Database.GetMotivationLineByFilter(motivationLineType);

            if (filteredMotivationLine != null)
            {
                LblCurrentMotivationLine.Text = filteredMotivationLine.Text;
                currentMotivationLine = filteredMotivationLine;
            }
            else
            {
                LblCurrentMotivationLine.Text = "Er bestaan geen motivatie zinnen in de database!";
            }
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentMotivationLine == null)
            {
                return;
            }

            await Navigation.PushAsync(new MotivationLineCreatorPage() { BindingContext = currentMotivationLine });
            LblCurrentMotivationLine.Text = "No motivation line yet!";
            currentMotivationLine = null;
        }

        private async void FavouriteButton_Clicked(object sender, EventArgs e)
        {
            if (currentMotivationLine == null || currentMotivationLine.IsFavourited)
            {
                return;
            }

            currentMotivationLine.IsFavourited = true;
            await App.Database.SaveMotivationLineAsync(currentMotivationLine);
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            if (currentMotivationLine == null)
            {
                return;
            }

            LblCurrentMotivationLine.Text = "No motivation line yet!";

            await App.Database.DeleteMotivationLineAsync(currentMotivationLine);
            currentMotivationLine = null;
        }
    }
}