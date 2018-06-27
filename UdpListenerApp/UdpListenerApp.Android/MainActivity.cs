using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using UdpListenerApp.Droid.Services;

namespace UdpListenerApp.Droid
{
    [Activity(Label = "UdpListenerApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
			MainActivity.Instance = this;
			UdpListenerService udpService = new UdpListenerService();
			udpService.StartListenForUDPBroadcast();
		}

		public void ShowToast(string title, string message)
		{
			RunOnUiThread(() => { Toast.MakeText(this.ApplicationContext, string.Format("{0}\n{1}", title, message), ToastLength.Long).Show(); });
		}

		public static MainActivity Instance { get; set; }
    }
}

