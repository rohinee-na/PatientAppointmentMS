using System;

namespace NotificationLibrary
{
    public interface INotificationManager
    {
        void SendNotification(string address,string message);
    }
}