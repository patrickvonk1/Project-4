using DatabaseAssembly;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Project4App
{
	public partial class App : Application
	{
        public static List<string> pickupLines = new List<string>();

        public async void PickupLinesFromFile(List<string> pickupLines)
        {
            foreach (var line in pickupLines)
            {
                string[] splittedLine = line.Split('*');
                string text = splittedLine[0];
                string type = splittedLine[1];

                ////Converting string to int
                //string numberString = "1";
                //int numberInt = int.Parse(numberString);//Gebruik parse (type wat het moet worden) (variableName) = (type wat het moet worden).parse(value)

                PickupLine newPickupLine = new PickupLine();
                newPickupLine.Text = text;

                await Database.SavePickupLineAsync(newPickupLine);
            }
        }

        private static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("Project4DB.db3"));// Dont change the string!, this is the name of the Database
                }

                return database;
            }
        }

        public App ()
		{

            InitializeComponent();
            MainTabbedPage mainTabbedPage = new MainTabbedPage();

            //PickupLine page
            NavigationPage pickupLineNavPage = new NavigationPage(new PickupLineMainPage());
            pickupLineNavPage.Title = "PickupLines";

            //MotivationLine Page
            NavigationPage motivationLineNavPage = new NavigationPage(new MotivationLineMainPage());
            motivationLineNavPage.Title = "Motivation";

            // JokeLine Page
            NavigationPage jokeLineNavPage = new NavigationPage(new JokeLineMainPage());
            jokeLineNavPage.Title = "Jokes";

            //// Favourite Page
            //NavigationPage FavouriteNavPage = new NavigationPage(new FavouriteMainPage());
            //FavouriteNavPage.Title = "Favourites";

            mainTabbedPage.Children.Add(pickupLineNavPage);
            mainTabbedPage.Children.Add(motivationLineNavPage);
            mainTabbedPage.Children.Add(jokeLineNavPage);
            //mainTabbedPage.Children.Add(FavouriteNavPage);

            MainPage = mainTabbedPage;
		}

		protected override void OnStart ()
		{
            PickupLinesFromFile(pickupLines);
            // Handle when your app starts
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
