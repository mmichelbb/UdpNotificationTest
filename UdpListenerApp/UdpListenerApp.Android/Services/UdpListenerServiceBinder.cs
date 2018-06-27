using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UdpListenerApp.Droid.Services
{
	public class UdpListenerServiceBinder : Binder
	{
		public UdpListenerService Service
		{
			get { return this.service; }
		}
		protected UdpListenerService service;

		public bool IsBound { get; set; }

		// constructor
		public UdpListenerServiceBinder(UdpListenerService service)
		{
			this.service = service;
		}
	}
}