using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPWebSocketClient
{
    public class ActionEvent
    {
        string action;
        public ActionEvent(string str)
        {
            action = str;
        }
    }
}
