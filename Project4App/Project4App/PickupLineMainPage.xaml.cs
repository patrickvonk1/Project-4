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
	public partial class PickupLineMainPage : ContentPage
	{
        PickupLine currentPickupLine;

		public PickupLineMainPage ()
		{
			InitializeComponent ();

            ToolbarItem toolbarItem = new ToolbarItem() { Text = "+" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new PickupLineCreatorPage() { BindingContext = new PickupLine() });
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
            PickupLine randomPickupLine = await App.Database.GetRandomPickupLineAsync();

            if (randomPickupLine != null)
            {
                LblCurrentPickupLine.Text = "Random PickupLine: " + randomPickupLine.Text;
                currentPickupLine = randomPickupLine;
            }
            else
            {
                LblCurrentPickupLine.Text = "There are no pickuplines available in the database!";
            }
        }
    }
}