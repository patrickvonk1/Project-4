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

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MotivationLineCreatorPage() { BindingContext = new MotivationLine() });
            };

            ToolbarItems.Add(toolbarItem);

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
            MotivationLine randomMotivationLine = await App.Database.GetRandomMotivationLineAsync();

            if (randomMotivationLine != null)
            {
                LblCurrentMotivationLine.Text = "Random MotivationLine: " + randomMotivationLine.Text;
                currentMotivationLine = randomMotivationLine;
            }
            else
            {
                LblCurrentMotivationLine.Text = "There are no motivationlines available in the database!";
            }
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentMotivationLine == null)
            {
                return;
            }

            //Edit the pickupline (Maybe go to the page where you can create a pickupline and update it there)
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