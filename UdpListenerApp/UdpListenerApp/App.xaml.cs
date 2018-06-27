using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace UdpListenerApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new MainPage();
		}

		public bool IsBackgrounded { get; set; }

		protected override void OnStart ()
		{
			IsBackgrounded = false;
		}

		protected override void OnSleep ()
		{
			IsBackgrounded = true;
		}

		protected override void OnResume ()
		{
			IsBackgrounded = false;
		}
	}
}
