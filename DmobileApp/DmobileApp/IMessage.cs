using System;
using System.Collections.Generic;
using System.Text;

namespace DmobileApp
{
    public interface IMessage
    {
        void longAlert(string message);
        void shortAlert(string message);
    }
}
