using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project4App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouriteMainPage : ContentPage
	{
        public ObservableCollection<Grouping<string, FavouriteLine>> Items = new ObservableCollection<Grouping<string, FavouriteLine>>();

		public FavouriteMainPage ()
		{
			InitializeComponent ();
		}

        public async Task GetFavourites()
        {
            //Items.Clear();

            //List<FavouriteLine> favouriteLines = await App.Database.GetAllFavourites();

            //Items.Add(new Grouping<string, FavouriteLine>("Pickup", favouriteLines.Where(f => f.FavouriteLineType == FavouriteLineType.PickupLine)));
            //Items.Add(new Grouping<string, FavouriteLine>("Motivation", favouriteLines.Where(f => f.FavouriteLineType == FavouriteLineType.MotivationLine)));
            //Items.Add(new Grouping<string, FavouriteLine>("Jokes", favouriteLines.Where(f => f.FavouriteLineType == FavouriteLineType.JokeLine)));

            //this.listviewFavourites.ItemsSource = Items;
        }
    }

    public class FavouriteLine
    {
        public string Text { get; set; }
        public FavouriteLineType FavouriteLineType { get; set; } 
    }
    
    public enum FavouriteLineType
    {
        PickupLine,
        MotivationLine,
        JokeLine
    }
}