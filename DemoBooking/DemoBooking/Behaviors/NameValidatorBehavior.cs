using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace DemoBooking
{
    public class NameValidatorBehavior : Behavior<Entry>
    {
        const string nameRegex = @"^([^0-9]*)$";

        public static bool IsValid { get; private set; }

        public const string Message = "• Name cannot be empty";


        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += HandleTextChanged;
        }


        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = (!String.IsNullOrWhiteSpace(e.OldTextValue)) && (Regex.IsMatch(e.NewTextValue, nameRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= HandleTextChanged;

        }
    }
}
