# UdpNotificationTest
Quick proof of concept for invoking local app notifications with udp messages

Usage: Compile and run UdpNotificationTest WPF Application (pseudo-server)
- enter title and message text
- enter ip address of the target device (the phone running the app)
- Click "Send UDP Broadcast" to send message (actually only  broadcasts if ip is 0.0.0.0 or empty)

Compile and run UdpListenerApp.Android
- Send UDP message from WPF app with app either open or in background
