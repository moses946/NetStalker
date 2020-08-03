<p align="center">
  <img align="center" src="https://i.imgur.com/4NdcRHF.png" alt="NetStalker">
</p>


# NetStalker
A network tool to control the bandwidth over your local network, it can block internet access form any selected device, or limit its speed using packet redirection, in addition, it can log web activity for the targeted device using a built in packet sniffer.

# Features:
- Background Scanning for newly connected devices.
- Bandwidth limitation for better distribution of internet speed across devices, both upload and download speeds can be controlled for each device separately.
- A Packet Sniffer that is intended to log addresses that each device on the network visits with the ability to decode Http headers for HTTP packets and resolve domains for HTTPS packets, also the packet direction can be chosen in order to capture requests only or requests and responses.
- A packet viewer to view the properties of a selected packet in the Packet Sniffer with the ability to expand the viewer for better visibility.
- Export the captured packets as a log file with the resolved domains included along with the timestamp for each packet.
- Spoof protection in order not to get spoofed by the same tool or any other spoofing software.
- Dark and light modes.
- Get network card vendor for every device using MacVendors API for better device identification.
- Can be locked with a password.
- Can be minimized to tray if the option is chosen.
- Integration with windows 10 notification system (works from build 17763).
- When minimized it notifies the user of newly discovered devices using Windows 10 notification system with the ability to choose from multiple options on what actions to take.
- Track disconnected devices with a timeout for each device. 

# Binaries
The latest stable version:
- NetStalker [Setup package](https://github.com/hmz777/NetStalker/releases/download/v2.0/NetStalker.exe)

# Notes
- The app uses the [Mac Vendors API](https://macvendors.com/) to retrieve the device's manufacturer, but it only uses the OUI (Organizational Unique Identifier) aka, the first 6 digits of the MAC address.
- The app is tested only on a small amount of network cards, so I can't guarantee it will work on yours.
- The source code may contain experimental features, if you're looking for a stable version, refer to the binaries or the releases section. 

# Pictures:

### Dark Mode:
![MainDark](https://i.imgur.com/CpnUqdC.jpg)

### Light Mode:
![MainLight](https://i.imgur.com/HOQl1kI.jpg)

### Packet Sniffer Dark:
![SnifferDark](https://i.imgur.com/6C5qkRu.jpg)

### Packet Sniffer Light:
![SnifferLight](https://i.imgur.com/RtwLAst.jpg)

### Speed Limiter:
![SpeedLimiter](https://i.imgur.com/bJdjiMX.jpg)

### Expanded Packet Viewer:
![ExpandedPacketViewer](https://i.imgur.com/dzFAQjV.jpg)

# Todo
Currently, I am doing a complete rewrite of this project with a cleaner codebase and updated dependencies.

# Additional Information

If you notice any errors or have a suggestion, you're free to email me or submit a pull request.

# Author
### Hamzi Alsheikh
### Website: [HMZ-Software](https://www.hmz-software.tk)
