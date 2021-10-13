using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class DEBUGGER
    {
        private Scheduler _scheduler;
        public int _ticks = 0;
        public DEBUGGER(Scheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public string print(bool lifts, bool Ticks)
        {
            string result = "";
            if (lifts)
            {
                int lift = 1;
                foreach (Elevator item in _scheduler.elevators)
                {
                    string str0 = lift.ToString();
                    string str1 = item.elevatorDispatcher.controller.CurrentFloor.ToString();
                    string str2 = item.elevatorDispatcher.MainFloor.ToString();
                    string str3 = item.elevatorDispatcher.StringQueue;
                    string str4 = item.elevatorDispatcher.controller.stateElevator.ToString();

                    result += string.Format("Лифт: {0}\nЭтаж: {1}\nТребуемый этаж: {2}\nОчередь: {3}\nСостояние: {4}\n", str0, str1, str2, str3, str4);
                    result += "--------------------\n";
                    lift += 1;
                }
            }
            if (Ticks)
            {
                result += "Тики:" + _ticks.ToString() + "\n";
            }
            return result;
        }
    }
}
