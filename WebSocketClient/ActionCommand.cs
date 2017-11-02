using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPWebSocketClient
{
    interface ActionCommand
    {
        void Action(ActionEvent e);
    }
}
