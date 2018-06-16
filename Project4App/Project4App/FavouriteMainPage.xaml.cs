﻿using DatabaseAssembly;
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

        public ObservableCollection<Grouping<string, FavouriteLine>> Items { get; set; } = new ObservableCollection<Grouping<string, FavouriteLine>>();

        public FavouriteMainPage()
        {
            InitializeComponent();
            FavouriteView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                if (e.Item == null)
                {
                    return;
                }

                ((ListView)sender).SelectedItem = null;
            };
        }

        public async Task AddFavouriteLinesToListview()
        {
            Items.Clear();

            List<PickupLine> pickupLines = (await App.Database.GetPickupLinesAsync()).Where(p => p.IsFavourited == true).ToList();
            List<MotivationLine> motivationLines = (await App.Database.GetMotivationLinesAsync()).Where(m => m.IsFavourited == true).ToList();
            List<JokeLine> jokeLines = (await App.Database.GetJokeLinesAsync()).Where(j => j.IsFavourited == true).ToList();
            List<FavouriteLine> favouriteLines = new List<FavouriteLine>();

            foreach (PickupLine pickupLine in pickupLines)
            {
                int totalSameLines = favouriteLines.Count(f => f.Text.ToLower() == pickupLine.Text.ToLower());

                if (totalSameLines > 0)
                {
                    continue;
                }

                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = pickupLine.Text;
                favouriteLine.Heading = "OPENINGSZINNEN";
                favouriteLine.FavouriteLineType = FavouriteLineType.PickUpLine;

                favouriteLines.Add(favouriteLine);
            }

            foreach (MotivationLine motivationLine in motivationLines)
            {
                int totalSameLines = favouriteLines.Count(f => f.Text.ToLower() == motivationLine.Text.ToLower());

                if (totalSameLines > 0)
                {
                    continue;
                }

                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = motivationLine.Text;
                favouriteLine.Heading = "MOTIVATIE";
                favouriteLine.FavouriteLineType = FavouriteLineType.MotivationLine;

                favouriteLines.Add(favouriteLine);
            }

            foreach (JokeLine jokeLine in jokeLines)
            {

                int totalSameLines = favouriteLines.Count(f => f.Text.ToLower() == jokeLine.Text.ToLower());

                if (totalSameLines > 0)
                {
                    continue;
                }

                FavouriteLine favouriteLine = new FavouriteLine();
                favouriteLine.Text = jokeLine.Text;
                favouriteLine.Heading = "GRAPPEN";
                favouriteLine.FavouriteLineType = FavouriteLineType.JokeLine;

                favouriteLines.Add(favouriteLine);
            }

            var sorted = from favouriteLine in favouriteLines
                         orderby favouriteLine.Heading
                         group favouriteLine by favouriteLine.Heading into favouriteLineGroup
                         select new Grouping<string, FavouriteLine>(favouriteLineGroup.Key, favouriteLineGroup);

            

            foreach (var g in sorted)
                Items.Add(g);

            FavouriteView.HasUnevenRows = true;
            BindingContext = this;
        }

        public class FavouriteLine
        {
            public string Text { get; set; }
            public string Heading { get; set; }
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
