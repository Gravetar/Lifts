using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class Door
    {
        public StateDoor stateDoor = StateDoor.closed;
        public void Open()
        {
            stateDoor = StateDoor.opened;
        }

        public void Close()
        {
            stateDoor = StateDoor.closed;
        }

        public bool IsOpen()
        {
            if (stateDoor == StateDoor.opened) return true;
            else return false;
        }
    }

    enum StateDoor
    {
        opened, closed
    }
}
