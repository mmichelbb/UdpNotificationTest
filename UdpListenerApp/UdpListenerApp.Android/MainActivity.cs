using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using UdpListenerApp.Droid.Services;
using System.Threading.Tasks;
using Android.Content;

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
			InitializeUdpService();
		}

		protected static UdpListenerServiceConnection udpServiceConnection;

		public void InitializeUdpService()
		{
			udpServiceConnection = new UdpListenerServiceConnection(null);

			// this event will fire when the Service connectin in the OnServiceConnected call 
			udpServiceConnection.ServiceConnected += (object sender, ServiceConnectedEventArgs e) => {
				UdpListenerService.OnMessageReceived += OnMessageReceived;
			};
			StartUdpService();
		}

		public void StartUdpService()
		{
			// Starting a service like this is blocking, so we want to do it on a background thread
			new Task(() => {

				// Start our main service
				Android.App.Application.Context.StartService(new Intent(Android.App.Application.Context, typeof(UdpListenerService)));

				// bind our service (Android goes and finds the running service by type, and puts a reference
				// on the binder to that service)
				// The Intent tells the OS where to find our Service (the Context) and the Type of Service
				// we're looking for (LocationService)
				Intent udpServiceIntent = new Intent(Android.App.Application.Context, typeof(UdpListenerService));

				// Finally, we can bind to the Service using our Intent and the ServiceConnection we
				// created in a previous step.
				this.ApplicationContext.BindService(udpServiceIntent, udpServiceConnection, Bind.AutoCreate);
			}).Start();
		}

		public UdpListenerService UdpListenerService
		{
			get
			{
				if (udpServiceConnection?.Binder != null)
					return udpServiceConnection.Binder.Service;
				return null;
				// note that we use the ServiceConnection to get the Binder, and the Binder to get the Service here
			}
		}

		public void ShowToast(string title, string message)
		{
			RunOnUiThread(() => { Toast.MakeText(this.ApplicationContext, string.Format("{0}\n{1}", title, message), ToastLength.Long).Show(); });
		}

		public void Notify(string title, string message)
		{
			if ((Xamarin.Forms.Application.Current as App).IsBackgrounded)
			{
				LocalNotificationService.ShowNotification(title, message);
			}
			else
			{
				MainActivity.Instance.ShowToast(title, message);
			}
		}

		private void OnMessageReceived(object sender, string[] e)
		{
			Notify(e[0], e[1]);
		}

		public static MainActivity Instance { get; set; }
    }
}

