using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class Elevator
    {
        private Door door = new Door();
        private List<ButtomElevator> buttomElevators = new List<ButtomElevator>(Settings.FLOORCOUNT);

        public ElevatorDispatcher elevatorDispatcher = new ElevatorDispatcher();

        public Elevator()
        {
            for (int i = 1; i <= Settings.FLOORCOUNT; i++)
            {
                buttomElevators.Add(new ButtomElevator(i));
            }
        }
    }
}
