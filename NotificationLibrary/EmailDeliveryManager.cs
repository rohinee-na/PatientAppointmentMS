using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationLibrary
{

    public delegate void EmailDelegate(string address,string message);
    public class EmailDeliveryManager:INotificationManager
    {
        public void SendNotification(string address, string message)
        {
            Console.WriteLine("Email sent to "+address+" : "+message);
        }
    }
}
