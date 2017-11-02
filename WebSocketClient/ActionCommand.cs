using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPWebSocketClient
{
    public interface ActionCommand
    {
        void Action(ActionCommand e);
    }
}
