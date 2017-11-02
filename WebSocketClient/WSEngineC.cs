using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPWebSocketClient
{
    class WSEngineC
    {
        List<ActionCommand> msgL = new List<ActionCommand>();
        List<ActionCommand> errL = new List<ActionCommand>();

        public void AddMessageListener(ActionCommand cmd)
        {
            msgL.Add(cmd);
        }

        public void Message(String str)
        {
            ActionEvent e = new ActionEvent(str);
            foreach (ActionCommand c in msgL)
            {
                c.Action(e);
            }
        }

        public void AddErrorListener(ActionCommand cmd)
        {
            errL.Add(cmd);
        }

        public void Error(String str)
        {
            ActionError e = new ActionError(str);
            foreach (ActionCommand c in errL)
            {
                c.Action(e);
            }
        }
    }
}
