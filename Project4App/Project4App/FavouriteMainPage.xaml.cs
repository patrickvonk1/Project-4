using DatabaseAssembly;
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

        public FavouriteMainPage()
        {
            InitializeComponent();
            AddFavouriteLinesToListview();
        }

        public async Task AddFavouriteLinesToListview()
        {
            //PickupLine newPickupLine = new PickupLine();
            //newPickupLine.Text = "Dit is helemaal kut";
            //newPickupLine.IsFavourited = true;ss
            //await App.Database.SavePickupLineAsync(newPickupLine);

            List<PickupLine> pickupLines =(await App.Database.GetPickupLinesAsync()).Where(p => p.IsFavourited == true).ToList();
            List<MotivationLine> motivationLines = (await App.Database.GetMotivationLinesAsync()).Where(m => m.IsFavourited == true).ToList();
            List<JokeLine> jokeLines = (await App.Database.GetJokeLinesAsync()).Where(j => j.IsFavourited == true).ToList();
            List<FavouriteLine> favouriteLines = new List<FavouriteLine>();

            foreach (var pickupLine in pickupLines)
            {
                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = pickupLine.Text;
                favouriteLine.FavouriteLineType = FavouriteLineType.PickUpLine;

                favouriteLines.Add(favouriteLine);
            }

            foreach (var motivationLine in motivationLines)
            {
                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = motivationLine.Text;
                favouriteLine.FavouriteLineType = FavouriteLineType.MotivationLine;

                favouriteLines.Add(favouriteLine);
            }

            foreach (var jokeLine in jokeLines)
            {
                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = jokeLine.Text;
                favouriteLine.FavouriteLineType = FavouriteLineType.JokeLine;

                favouriteLines.Add(favouriteLine);
            }

            FavouriteView.ItemsSource = favouriteLines;
        }

        public class FavouriteLine
        {
            public string Text { get; set; }
            public FavouriteLineType FavouriteLineType { get; set; }
        }

        public enum FavouriteLineType
        {
            PickUpLine,
            MotivationLine,
            JokeLine,
        }

    }
}
