using Acr.Settings;
using System;
using Weavy.Helpers;
using Weavy.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Weavy.Controls {
    /// <summary>
    /// Custom Weavy content page
    /// </summary>
    public class WeavyPage : ContentPage {
        public EventHandler NotificationReceived;

        /// <summary>
        /// When view appears
        /// </summary>
        protected override void OnAppearing() {
            base.OnAppearing();

            // set iOS padding     
            if (Device.RuntimePlatform == Device.iOS) {
                var safeArea = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();
                safeArea.Bottom = 0;
                Padding = safeArea;
            }

            // set theme color     
            var color = CrossSettings.Current.Get<string>("themecolor") ?? Constants.DefaultColor;
            SetThemeColor(color);
        }

        /// <summary>
        /// Handle notification (iOS)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnNotificationReceived(object sender, NotificationEventArgs e) {
            var handler = this.NotificationReceived;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Set theme color
        /// </summary>
        /// <param name="color">The hex color to use</param>
        protected void SetThemeColor(string color) {
            // update theme color 
            CrossSettings.Current.Set<string>("themecolor", color);

            if (Device.RuntimePlatform == Device.iOS) {
                BackgroundColor = Color.FromHex(color).MultiplyAlpha(0.95);
            }

            INavigationBarColor nav = DependencyService.Get<INavigationBarColor>();

            Device.BeginInvokeOnMainThread(() => {
                nav.SetStatusBarColor(color);
            });
        }
    }

    /// <summary>
    /// Notification event arguments
    /// </summary>
    public class NotificationEventArgs : EventArgs {
        public string NotificationUrl { get; set; }
    }
}
