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
        Random random = new Random();
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

            if (currentPickupLine != null && filteredPickupLine != null)
            {
                if (filteredPickupLine.Text.ToLower() == currentPickupLine.Text.ToLower())
                {
                    var allPickupLines = await App.Database.GetPickupLinesAsync();
                    var otherPickupLines = allPickupLines.Where(p => p.Text.ToLower() != filteredPickupLine.Text.ToLower()).ToList();

                    if (otherPickupLines != null && otherPickupLines.Count > 0)
                    {
                        if (otherPickupLines.Count() == 1)
                        {
                            filteredPickupLine = otherPickupLines[0];
                        }
                        else
                        {
                            filteredPickupLine = otherPickupLines[random.Next(0, otherPickupLines.Count)];
                        }
                    }
                }
            }

            if (filteredPickupLine != null)
            {
                LblCurrentPickupLine.Text = filteredPickupLine.Text;
                currentPickupLine = filteredPickupLine;
            }
            else
            {
                LblCurrentPickupLine.Text = "Geen openingszinnen gevonden!";
            }
        }
        
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentPickupLine == null)
            {
                return;
            }

            await Navigation.PushAsync(new PickupLineCreatorPage() { BindingContext = currentPickupLine });
            LblCurrentPickupLine.Text = "Druk op het hartje!";
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

            LblCurrentPickupLine.Text = "Druk op het hartje!";

            await App.Database.DeletePickupLineAsync(currentPickupLine);
            currentPickupLine = null;
        }

          
    }
}