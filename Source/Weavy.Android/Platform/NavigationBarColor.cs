using Android.App;
using Weavy.Droid.Platform;
using Weavy.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationBarColor))]
namespace Weavy.Droid.Platform {

    /// <summary>
    /// Android implementation of setting the status bar color
    /// </summary>
    public class NavigationBarColor : INavigationBarColor {

        /// <summary>
        /// Set the color for the status bar according to the Weavy theming color
        /// </summary>
        /// <param name="hexColor">The hex color to use</param>
        public void SetStatusBarColor(string hexColor) {

            // parse to droid color
            var color = Android.Graphics.Color.ParseColor(hexColor);

            // get color with 95% alpha channel
            var alphaColor = GetColorWithAlpha(color, 0.95);

            // set status bar color
            ((Activity)Forms.Context).Window.SetStatusBarColor(alphaColor);
        }

        /// <summary>
        /// Get color with alpha
        /// </summary>
        /// <param name="color">the color</param>
        /// <param name="alpha">the alpha channel i %</param>
        /// <returns></returns>
        private Android.Graphics.Color GetColorWithAlpha(Android.Graphics.Color color, double alpha) {
            int red = color.R;
            int blue = color.B;
            int green = color.G;
            var a = (int)(255 * alpha);
            return Android.Graphics.Color.Argb(a, red, green, blue);
        }
    }

    
}