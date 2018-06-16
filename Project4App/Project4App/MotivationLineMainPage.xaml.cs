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
        Random random = new Random();
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

            if (currentMotivationLine != null && filteredMotivationLine != null)
            {
                if (filteredMotivationLine.Text.ToLower() == currentMotivationLine.Text.ToLower())
                {
                    var allMotivationLines = await App.Database.GetMotivationLinesAsync();
                    var otherMotivationLines = allMotivationLines.Where(p => p.Text.ToLower() != filteredMotivationLine.Text.ToLower()).ToList();

                    if (otherMotivationLines != null && otherMotivationLines.Count > 0)
                    {
                        if (otherMotivationLines.Count() == 1)
                        {
                            filteredMotivationLine = otherMotivationLines[0];
                        }
                        else
                        {
                            filteredMotivationLine = otherMotivationLines[random.Next(0, otherMotivationLines.Count)];
                        }
                    }
                }
            }

            if (filteredMotivationLine != null)
            {
                LblCurrentMotivationLine.Text = filteredMotivationLine.Text;
                currentMotivationLine = filteredMotivationLine;
            }
            else
            {
                LblCurrentMotivationLine.Text = "Geen motivatie zinnen gevonden!!";
            }
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentMotivationLine == null)
            {
                return;
            }

            await Navigation.PushAsync(new MotivationLineCreatorPage() { BindingContext = currentMotivationLine });
            LblCurrentMotivationLine.Text = "Druk op het mannetje!";
            currentMotivationLine = null;
        }

        private async void FavouriteButton_Clicked(object sender, EventArgs e)
        {
            if (currentMotivationLine == null || currentMotivationLine.IsFavourited)
            {
                return;
            }

            var allMotivationLines = await App.Database.GetMotivationLinesAsync();
            int favouritedLinesWithSameTextCount = allMotivationLines.Count(p => p.Text.ToLower() == currentMotivationLine.Text.ToLower() && p.IsFavourited);

            if (favouritedLinesWithSameTextCount > 1)
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

            LblCurrentMotivationLine.Text = "Druk op het mannetje!";

            await App.Database.DeleteMotivationLineAsync(currentMotivationLine);
            currentMotivationLine = null;
        }
    }
}