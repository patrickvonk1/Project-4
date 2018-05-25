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
        }

        private async void BtnGetRandomJoke_Clicked(object sender, EventArgs e)
        {
            JokeLine randomJokeLine = await App.Database.GetRandomJokeLineAsync();

            if (randomJokeLine != null)
            {
                LblCurrentJoke.Text = "Random Joke: " + randomJokeLine.Text;
                currentJokeLine = randomJokeLine;
            }
            else
            {
                LblCurrentJoke.Text = "There are no jokes available in the database!";
            }
        }
    }
}