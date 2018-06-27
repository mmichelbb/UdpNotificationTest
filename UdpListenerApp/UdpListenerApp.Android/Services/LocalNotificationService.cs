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
	public static class LocalNotificationService
	{
		public static void ShowNotification(string title, string message)
		{
			Context act = ((Activity)MainActivity.Instance);
			Intent intent = new Intent(act, typeof(MainActivity));

			// Create a PendingIntent; we're only using one PendingIntent (ID = 0):
			const int pendingIntentId = 0;
			PendingIntent pendingIntent =
				PendingIntent.GetActivity(act, pendingIntentId, intent, PendingIntentFlags.OneShot);

			Notification.Builder builder = new Notification.Builder(act)
				.SetContentIntent(pendingIntent)
			.SetContentTitle(title)
			.SetContentText(message)
			.SetSmallIcon(Resource.Drawable.Icon);

			Notification notification = builder.Build();
			NotificationManager notificationManager =
				act.GetSystemService(Context.NotificationService) as NotificationManager;
			const int notificationId = 0;
			notificationManager.Notify(notificationId, notification);
		}
	}
}