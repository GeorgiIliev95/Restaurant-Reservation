using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoBooking.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoBooking
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public static ReservationRequest Reservation { get; private set; }

        private static readonly List<string> WorkingHours = new List<string>() { "11:00", "11:30", "12:00", "12:30", "13:00",
                                                                        "13:30", "14:00", "14:30", "15:00", "15:30",
                                                                        "16:00", "16:30", "17:00", "17:30", "18:00",
                                                                        "18:30", "19:00", "19:30", "20:00", "20:30",
                                                                        "21:00", "21:30", "22:00"};
        private static List<string> visibleHours;

        private List<string> GetNextDays()// Creates list for next 30 days and parses them to string
        {
            DateTime today = DateTime.Now;
            DateTime nextDay;
            string parsedDay;
            List<string> dates = new List<string>();
            for (int i = 0; i < 30; i++)
            {
                nextDay = today.AddDays(i);
                parsedDay = String.Format("{0:dddd, MMM, d}", nextDay);
                dates.Add(parsedDay);
            }

            if (today.Hour >= 22)
            {
                dates.RemoveAt(0);
            }

            return dates;
        }

        private void GetNextHours(DateTime selectedDay)//Creates list containing every working hour or the next hours of the day
        {
            visibleHours = WorkingHours;

            if (selectedDay == DateTime.Today)
            {
                List<string> items = new List<string>(WorkingHours);

                for (int i = 0; i < items.Count - 1; i++)
                {
                    if (items[i].StartsWith(DateTime.Now.Hour.ToString()))
                    {
                        items.RemoveRange(0, i + 2);
                        visibleHours = items;
                        return;
                    }
                }
            }
        }

        private int GetPickerSize(Picker picker)//Parses chosen party size from picker to integer value
        {
            string size = picker.SelectedItem.ToString().Substring(0, 1);
            return Convert.ToInt32(size);

        }

        private DateTime GetPickerDate(Picker datePicker)//Parses chosen date and chosen from pickers and adds them to DateTime class
        {
            DateTime chosenDate = DateTime.ParseExact(datePicker.SelectedItem.ToString(), "dddd, MMM, d", System.Globalization.CultureInfo.InvariantCulture);


            return chosenDate;
        }

        private DateTime GetChosenDateAndTime(Picker datePicker, Button button)//Parses chosen date and text from clicked button to DateTime class
        {
            DateTime chosenDate = DateTime.ParseExact(datePicker.SelectedItem.ToString(), "dddd, MMM, d", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan chosenTime = TimeSpan.Parse(button.Text);

            return chosenDate.Add(chosenTime);
        }

        private void PopulateScrollView()//Seeds ScrollView with buttons
        {
            stack.Children.Clear();
            List<Button> timeButtons = new List<Button>(visibleHours.Count);
            for (int i = 0; i < visibleHours.Count; i++)
            {
                timeButtons.Add(new Button() { Text = visibleHours[i], VerticalOptions = LayoutOptions.Center, BackgroundColor = Color.FromHex("#721c28"), TextColor = Color.White });
                timeButtons[i].Clicked += CreateReservation;
                stack.Children.Add(timeButtons[i]);

            }
        }

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            List<string> dates = GetNextDays();
            visibleHours = new List<string>(WorkingHours.Count);

            InitializeComponent();

            datePicker.ItemsSource = dates;
            datePicker.SelectedIndex = 0;

            GetNextHours(GetPickerDate(datePicker));

            if (Device.RuntimePlatform == Device.UWP)
            {
                headerLbl.FontSize = 24;
                partyPicker.TextColor = Color.Black;
                datePicker.TextColor = Color.Black;
            }

            datePicker.SelectedIndexChanged += (sender, args) =>
            {
                if (datePicker.SelectedIndex != -1)
                {
                    GetNextHours(GetPickerDate(datePicker));
                    PopulateScrollView();
                }
            };

            PopulateScrollView();

            datePicker.SelectedIndex = 0;
            partyPicker.SelectedIndex = 0;
        }

        async void CreateReservation(object s, EventArgs e)//On button click creates reservation and if there are free seats goes to next page, else pop up windows
        {
            Reservation = new ReservationRequest
            {
                PartySize = GetPickerSize(partyPicker),
                Date = GetChosenDateAndTime(datePicker, (s as Button))
            };

            if (App.Database.AreSeatsEnough(Reservation))
            {
                //If true go to next page
                await Navigation.PushAsync(new ClientData());

            }
            else
            {
                //Else tell the user the restraunt is busy
                await DisplayAlert("No free table", "All the tables for this time are taken!", "Ok");
            }
        }
    }
}