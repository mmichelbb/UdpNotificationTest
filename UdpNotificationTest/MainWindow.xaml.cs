using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UdpNotificationTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void SendUDPPacketClick(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(TitleTextBox.Text) || string.IsNullOrEmpty(MessageTextBox.Text))
				return;
			using (UdpClient client = new UdpClient())
			{
				IPEndPoint ip;
				if (string.IsNullOrEmpty(TargetTextBox.Text) || TargetTextBox.Text == "0.0.0.0")
				{
					ip = new IPEndPoint(IPAddress.Broadcast, 6666);
				}
				else
				{
					ip = new IPEndPoint(IPAddress.Parse(TargetTextBox.Text), 6666);
				}
				byte[] msgBytes = Encoding.UTF8.GetBytes(string.Format("{0}::{1}", TitleTextBox.Text, MessageTextBox.Text));
				client.Send(msgBytes, msgBytes.Length, ip);
			}
		}
	}
}
