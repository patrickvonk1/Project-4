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
            ToolbarItem toolbarItem2 = new ToolbarItem() { Text = "?" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new PickupLineCreatorPage() { BindingContext = new PickupLine() });
            };

            toolbarItem2.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new HelpPage() {});
            };

            ToolbarItems.Add(toolbarItem);
            ToolbarItems.Add(toolbarItem2);

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
            Picker picker = this.FindByName<Picker>("PickUpLineTypePicker");
            string pickupLineType = "";

            if (picker != null && picker.SelectedItem != null)
            {
                pickupLineType = (string)picker.SelectedItem;
            }

            PickupLine filteredPickupLine = await App.Database.GetPickupLineByFilter(pickupLineType);

            if (filteredPickupLine != null)
            {
                LblCurrentPickupLine.Text = filteredPickupLine.Text;
                currentPickupLine = filteredPickupLine;
            }
            else
            {
                LblCurrentPickupLine.Text = "Er bestaan geen openingszinnen in de database!";
            }
        }
        
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentPickupLine == null)
            {
                return;
            }

            await Navigation.PushAsync(new PickupLineCreatorPage() { BindingContext = currentPickupLine });
            LblCurrentPickupLine.Text = "No pickup line yet!";
            currentPickupLine = null;
        }
        
        private async void FavouriteButton_Clicked(object sender, EventArgs e)
        {
            if (currentPickupLine == null || currentPickupLine.IsFavourited)
            {
                return;
            }

            currentPickupLine.IsFavourited = true;
            await App.Database.SavePickupLineAsync(currentPickupLine);
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            if (currentPickupLine == null)
            {
                return;
            }

            LblCurrentPickupLine.Text = "No pickup line yet!";

            await App.Database.DeletePickupLineAsync(currentPickupLine);
            currentPickupLine = null;
        }

          
    }
}