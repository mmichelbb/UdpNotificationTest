﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Net;

namespace UdpListenerApp.Droid.Services
{
	public class UdpListenerService : Service
	{
		DatagramSocket socket;

		static string UDP_BROADCAST = "UDPBroadcast";
		static int UDP_LISTEN_PORT = 6666;

		public override IBinder OnBind(Intent intent)
		{
			return null;
		}

		public override void OnDestroy()
		{
			StopListen();
		}

		private bool shouldRestartSocketListen = true;

		public void StopListen()
		{
			shouldRestartSocketListen = false;
			socket.Close();
		}

		Java.Lang.Thread UDPBroadcastThread;

		public void StartListenForUDPBroadcast()
		{
			UDPBroadcastThread = new Java.Lang.Thread(new Runnable(Run));

			UDPBroadcastThread.Start();

		}

		public void Run()
		{
			try
			{
				int port = 6666;
				while (shouldRestartSocketListen)
				{
					ListenAndWaitAndThrowNotification();
				}
			}
			catch (System.Exception e)
			{
				int i = 0;
			}
		}

		private void ListenAndWaitAndThrowNotification()
		{
			byte[] recvBuf = new byte[15000];
			if (socket == null || socket.IsClosed)
			{
				socket = new DatagramSocket(UDP_LISTEN_PORT);
				socket.Broadcast = true;
			}
			DatagramPacket packet = new DatagramPacket(recvBuf, recvBuf.Length);
			socket.Receive(packet);

			string senderIP = packet.Address.HostAddress;
			string message = Encoding.UTF8.GetString(packet.GetData());
			string[] msg = message.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

			if ((Xamarin.Forms.Application.Current as App).IsBackgrounded)
			{
				LocalNotificationService.ShowNotification(msg[0], "From " + senderIP + ": " + msg[1]);
			}
			else
			{
				MainActivity.Instance.ShowToast(msg[0], "From " + senderIP + ": " + msg[1]);
			}
			socket.Close();
		}
	}
}