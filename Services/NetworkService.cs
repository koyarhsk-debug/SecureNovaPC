using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SecureNovaPC.Services {
    public class NetworkService {
        private List<string> blockedApplications;
        private List<string> allowedApplications;

        public NetworkService() {
            blockedApplications = new List<string>();
            allowedApplications = new List<string>();
        }

        public bool IsNetworkConnected() {
            try {
                using (var client = new WebClient()) {
                    using (client.OpenRead("http://google.com")) {
                        return true;
                    }
                }
            }
            catch {
                return false;
            }
        }

        public string GetPublicIPAddress() {
            try {
                using (var client = new WebClient()) {
                    string publicIP = client.DownloadString("https://api.ipify.org");
                    return publicIP;
                }
            }
            catch {
                return "Unknown";
            }
        }

        public string GetLocalIPAddress() {
            try {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0)) {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    return endPoint.Address.ToString();
                }
            }
            catch {
                return "127.0.0.1";
            }
        }

        public List<string> GetActiveConnections() {
            try {
                List<string> connections = new List<string>();
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnections = ipGlobalProperties.GetActiveTcpConnections();

                foreach (var connection in tcpConnections) {
                    connections.Add($"{connection.LocalEndPoint} -> {connection.RemoteEndPoint}");
                }
                return connections;
            }
            catch {
                return new List<string>();
            }
        }

        public double CheckNetworkLatency(string hostName = "8.8.8.8") {
            try {
                Ping ping = new Ping();
                PingReply reply = ping.Send(hostName, 1000);
                if (reply.Status == IPStatus.Success) {
                    return reply.RoundtripTime;
                }
                return -1;
            }
            catch {
                return -1;
            }
        }

        public bool IsFirewallEnabled() {
            try {
                var firewallPolicy = new NetFw.INetFwPolicy2();
                return firewallPolicy.FirewallEnabled;
            }
            catch {
                return false;
            }
        }

        public void BlockApplication(string applicationPath) {
            if (!blockedApplications.Contains(applicationPath)) {
                blockedApplications.Add(applicationPath);
            }
        }

        public void AllowApplication(string applicationPath) {
            if (blockedApplications.Contains(applicationPath)) {
                blockedApplications.Remove(applicationPath);
            }
            if (!allowedApplications.Contains(applicationPath)) {
                allowedApplications.Add(applicationPath);
            }
        }

        public List<string> GetBlockedApplications() {
            return blockedApplications;
        }

        public List<string> GetAllowedApplications() {
            return allowedApplications;
        }

        public void ScanForSuspiciousConnections() {
            try {
                var connections = GetActiveConnections();
                foreach (var connection in connections) {
                    // Add logic to identify suspicious connections
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error scanning connections: {ex.Message}");
            }
        }
    }
}
