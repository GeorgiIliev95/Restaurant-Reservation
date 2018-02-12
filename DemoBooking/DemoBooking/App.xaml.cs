using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoBooking.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using DemoBooking.Data;

namespace DemoBooking
{
	public partial class App : Application
	{
		private static RestaurantReservations database;

		public static RestaurantReservations Database
		{
			get
			{
				if (database == null)
				{
					database = new RestaurantReservations();
				}

				return database;
			}
		}

		public App()
		{
			database = new RestaurantReservations();

			database = JsonConvert.DeserializeObject<RestaurantReservations>(JsonData.json);

			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart ()
		{
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
