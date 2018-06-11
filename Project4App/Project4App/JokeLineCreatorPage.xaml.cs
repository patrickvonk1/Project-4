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
	public partial class JokeLineCreatorPage : ContentPage
	{
		public JokeLineCreatorPage ()
		{
			InitializeComponent ();
		}

        private async void Save_Clicked(object sender, EventArgs e)
        {
            JokeLine jokeLineItem = (JokeLine)BindingContext;

            jokeLineItem.JokeLineType = (JokeLineType)Enum.Parse(typeof(JokeLineType), (string)JokeLineTypePicker.SelectedItem);

            await App.Database.SaveJokeLineAsync(jokeLineItem);
            await Navigation.PopAsync();
        }

        private async void Cancle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}