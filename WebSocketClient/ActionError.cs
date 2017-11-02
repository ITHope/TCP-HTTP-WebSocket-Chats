using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPWebSocketClient
{
    class ActionError : ActionCommand
    {
        public string action;
        public ActionError(string str)
        {
            action = str;
        }
        public void Action(ActionCommand e)
        {
            
        }
    }
}
