﻿namespace komal.puremvc
{
    public interface INotifier
    {
        void SendNotification(string notificationName, object body = null, string type = null);
        void SendNotification(string notificationName, params object[] param);
    }
}
