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
	public partial class MotivationLineCreatorPage : ContentPage
	{
		public MotivationLineCreatorPage ()
		{
			InitializeComponent ();
		}

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MotivationLine motivationLineItem = (MotivationLine)BindingContext;

            await App.Database.SaveMotivationLineAsync(motivationLineItem);
            await Navigation.PopAsync();
        }

        private async void Cancle_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}