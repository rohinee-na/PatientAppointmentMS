using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary
{
    public delegate void SMSDelegate(string address, string message);
    public class SMSDeliveryManager : INotificationManager
    {
        
        public void SendNotification(string address,string message)
        {
            long phoneNumber = long.Parse(address);
            Console.WriteLine("SMS sent to " + phoneNumber+ ": "+message);
        }
    }
}
