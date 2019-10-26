using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace Passcore
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            StartActivity(new Intent(this, typeof(MainActivity)));
            Finish();
        }
    }
}