using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class Controller
    {
        /// <summary>
        /// Мотор лифта отвечает за все движения лифта
        /// </summary>
        public Motor motor = new Motor();

        /// <summary>
        /// Состояния лифта
        /// </summary>
        public StateElevator stateElevator = StateElevator.wait;

        /// <summary>
        /// Закончил ли лифт выполнять действия на этаже
        /// </summary>
        private bool FinishOnFloor = true;

        /// <summary>
        /// Направление движения лифта
        /// </summary>
        private int direction;
        /// <summary>
        /// Направление движения лифта
        /// 1 = Вверх
        /// -1 = Вниз
        /// 0 = На месте
        /// 2 = Действия на этаже
        /// </summary>
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// Текущий этаж
        /// </summary>
        private int currentFloor;
        /// <summary>
        /// Текущий этаж
        /// </summary>
        public int CurrentFloor
        {
            get { return currentFloor; }
            set { currentFloor = value; }
        }

        public void Move()
        {
            if (direction == 1) // Если лифту надо двигаться вверх
            {
                motor.Up(ref currentFloor); // Послать сигнал движения вверх мотору
                stateElevator = StateElevator.up; // Установить статус лифта в "Движется вверх"
            }
            else if (direction == -1) // Если лифту надо двигаться вниз
            {
                motor.Down(ref currentFloor); // Послать сигнал движения вниз мотору
                stateElevator = StateElevator.down; // Установить статус лифта в "Движется вниз"
            }
            else if (direction == 2) // Если лифту надо выполнять действия на этаже
            {
                motor.StopOnFloor(ref FinishOnFloor); // Послать сигнал выполнения действий на этаже мотору
                stateElevator = StateElevator.waitonfloor; // Установить статус лифта в "Лифт выполняет действия на этаже"
                if (FinishOnFloor) // Если лифт закончил выполнять действия на этаже
                {
                    direction = 0; // Обнулить направление лифта(стоять на месте)
                }
            }
            else // Иначе действий никаких не трубется
            {
                motor.Stop(); // "Выключить" мотор
            }
        }
    }

    /// <summary>
    /// Состояния лифта
    /// up - Движется вверх
    /// down - Движется вниз
    /// wait - Лифт ожидает
    /// waitonfloor - Лифт выполняет действия на этаже
    /// </summary>
    enum StateElevator
    {
        up, down, wait, waitonfloor
    }
}
