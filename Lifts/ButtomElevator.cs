using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class ButtomElevator
    {
        public ButtomElevator(int floor)
        {
            _floor = floor;
        }

        private int _floor;
        public int Floor
        {
            get
            {
                return _floor;
            }
        }
        private bool _state = false;
        /// <summary>
        /// считывает значение датчика кнопки лифта
        /// </summary>
        public bool State
        {
            get
            {
                return _state;
            }
        }

        public void ChangeStateToOff()
        {
            _state = false;
        }

        public void ChangeStateToOn()
        {
            _state = true;
        }
    }
}
