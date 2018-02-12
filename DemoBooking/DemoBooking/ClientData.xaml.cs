using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoBooking
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientData : ContentPage
    {        
        private static bool ValidateForm()
        {
            return EmailValidatorBehavior.IsValid && PhoneValidatorBehavior.IsValid && NameValidatorBehavior.IsValid;
        }

        public ClientData()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private async void ValidateAndFinish(object s, EventArgs e)
        {
            if (ValidateForm())
            {
                await Navigation.PushAsync(new CompleteReservation());
            }
            else
            {
                await DisplayAlert("Invalid data", "The data you've input is incorrect!", "Ok");               
            }
        }
    }
}