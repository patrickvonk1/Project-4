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
	public partial class JokeLineMainPage : ContentPage
	{
        Random random = new Random();
        JokeLine currentJokeLine;
        public JokeLineMainPage ()
		{
			InitializeComponent ();

            ToolbarItem toolbarItem = new ToolbarItem() { Text = "+" };
            ToolbarItem toolbarItem2 = new ToolbarItem() { Text = "?" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new JokeLineCreatorPage() { BindingContext = new JokeLine() });
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
            string jokeLineType = "";
            if (JokeLineTypePicker != null && JokeLineTypePicker.SelectedItem != null)
            {
                jokeLineType = (string)JokeLineTypePicker.SelectedItem;
            }

            JokeLine filteredJokeLine = await App.Database.GetJokeLineByFilter(jokeLineType);

            if (currentJokeLine != null && filteredJokeLine != null)
            {
                if (filteredJokeLine.Text.ToLower() == currentJokeLine.Text.ToLower())
                {
                    var allJokeLines = await App.Database.GetJokeLinesAsync();
                    var otherJokeLines = allJokeLines.Where(p => p.Text.ToLower() != filteredJokeLine.Text.ToLower()).ToList();

                    if (otherJokeLines != null && otherJokeLines.Count > 0)
                    {
                        if (otherJokeLines.Count() == 1)
                        {
                            filteredJokeLine = otherJokeLines[0];
                        }
                        else
                        {
                            filteredJokeLine = otherJokeLines[random.Next(0, otherJokeLines.Count)];
                        }
                    }
                }
            }

            if (filteredJokeLine != null)
            {
                LblCurrentJoke.Text = filteredJokeLine.Text;
                currentJokeLine = filteredJokeLine;
            }
            else
            {
                LblCurrentJoke.Text = "Geen grappen gevonden!";
            }
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentJokeLine == null)
            {
                return;
            }

            await Navigation.PushAsync(new JokeLineCreatorPage() { BindingContext = currentJokeLine });
            LblCurrentJoke.Text = "Druk op de Smiley!";
            currentJokeLine = null;
        }

        private async void FavouriteButton_Clicked(object sender, EventArgs e)
        {
            if (currentJokeLine == null || currentJokeLine.IsFavourited)
            {
                return;
            }

            var allJokeLines = await App.Database.GetJokeLinesAsync();
            int favouritedLinesWithSameTextCount = allJokeLines.Count(p => p.Text.ToLower() == currentJokeLine.Text.ToLower() && p.IsFavourited);

            if (favouritedLinesWithSameTextCount > 1)
            {
                return;
            }

            currentJokeLine.IsFavourited = true;
            await App.Database.SaveJokeLineAsync(currentJokeLine);
        }

        private async void RemoveButton_Clicked(object sender, EventArgs e)
        {
            if (currentJokeLine == null)
            {
                return;
            }

            LblCurrentJoke.Text = "Druk op de Smiley!";

            await App.Database.DeleteJokeLineAsync(currentJokeLine);
            currentJokeLine = null;
        }
    }
}