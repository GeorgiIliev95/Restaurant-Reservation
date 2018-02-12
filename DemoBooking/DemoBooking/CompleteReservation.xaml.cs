using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoBooking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompleteReservation : ContentPage
    {
        public CompleteReservation()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            reservationDetails.Text = "You have reserved a table for " + MainPage.Reservation.PartySize + " people on " + MainPage.Reservation.Date.ToString("dddd, MMM d HH:mm");
        }
    }
}