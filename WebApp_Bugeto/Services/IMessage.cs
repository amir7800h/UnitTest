using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_Bugeto.Services
{
    public interface IMessage
    {
        void Sms(string message, int userId);
        void Email(string message, int userId);
        void Notif(string message, int userId);
    }

    public enum Messagetype
    {
        Sms,
        Email,
        Notif
    }
}
