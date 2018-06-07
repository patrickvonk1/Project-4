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
        JokeLine currentJokeLine;
        public JokeLineMainPage ()
		{
			InitializeComponent ();

            ToolbarItem toolbarItem = new ToolbarItem() { Text = "+" };

            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new JokeLineCreatorPage() { BindingContext = new JokeLine() });
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
            string jokeLineType = "";
            if (JokeLineTypePicker != null && JokeLineTypePicker.SelectedItem != null)
            {
                jokeLineType = (string)JokeLineTypePicker.SelectedItem;
            }

            JokeLine filteredJokeLine = await App.Database.GetJokeLineByFilter(jokeLineType);

            if (filteredJokeLine != null)
            {
                LblCurrentJoke.Text = "Random Joke: " + filteredJokeLine.Text;
                currentJokeLine = filteredJokeLine;
            }
            else
            {
                LblCurrentJoke.Text = "There are no jokes available in the database!";
            }
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (currentJokeLine == null)
            {
                return;
            }

            await Navigation.PushAsync(new JokeLineCreatorPage() { BindingContext = currentJokeLine });
            LblCurrentJoke.Text = "No joke yet!";
            currentJokeLine = null;
        }

        private async void FavouriteButton_Clicked(object sender, EventArgs e)
        {
            if (currentJokeLine == null || currentJokeLine.IsFavourited)
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

            LblCurrentJoke.Text = "No joke yet!";

            await App.Database.DeleteJokeLineAsync(currentJokeLine);
            currentJokeLine = null;
        }
    }
}