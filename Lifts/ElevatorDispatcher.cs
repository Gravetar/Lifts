using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class ElevatorDispatcher
    {
        /// <summary>
        /// Очередь лифта
        /// </summary>
        private List<int> queue = new List<int>();
        /// <summary>
        /// Очередь лифта геттер в представлении string
        /// Строка в виде "Этаж1 Этаж2 Этаж3 ..." цифрой
        /// </summary>
        public List<int> Queue
        {
            get
            {
                return queue;
            }
        }
        /// <summary>
        /// Очередь лифта геттер в представлении string
        /// Строка в виде "Этаж1 Этаж2 Этаж3 ..." цифрой
        /// </summary>
        public string StringQueue
        {
            get
            {
                string res = "";
                for (int i = 0; i<queue.Count; i++)
                {
                    res += queue[i] + " ";
                }
                return res;
            }
        }
        /// <summary>
        /// Количество этажей в очереди лифта
        /// </summary>
        public int QueueCount
        {
            get
            {
                return queue.Count();
            }
        }

        /// <summary>
        /// Контроллер лифта
        /// - Посылает сигналы мотору и устанавливает статус лифта
        /// </summary>
        public Controller controller = new Controller();

        /// <summary>
        /// Текущий этаж
        /// </summary>
        private int currentFloor = 0;

        /// <summary>
        /// Этаж к которому нужно двигаться лифту
        /// </summary>
        public int MainFloor = -1;

        /// <summary>
        /// Добавить этаж в очередь
        /// </summary>
        /// <param name="floor">Добовляемый этаж</param>
        public void AddFloor(int floor)
        {
            if (floor != currentFloor) queue.Add(floor);
            if (MainFloor == -1) MainFloor = floor;
        }

        /// <summary>
        /// Определить куда нужно двигаться лифту (Работает каждый тик программы)
        /// </summary>
        public void DeterminingDirection()
        {
            currentFloor = controller.CurrentFloor; // Установить текущий этаж
            if (queue.Count == 0) // Если очередь пуста
            {
                controller.stateElevator = StateElevator.wait;
                MainFloor = -1; // "Обнулить" требуемый этаж
            }
            else if (queue.Contains(currentFloor)) // Иначе если текущий этаж есть в очередь
            {
                controller.Direction = 2; // Установка лифту сделать нужные действия на этаже
                queue.RemoveAll(x => x == currentFloor); // Удалить этаж из списка очереди
                if (queue.Count != 0 && MainFloor == currentFloor) MainFloor = queue[0]; // Если очередь не пуста и достигли требуемого этажа, то установить требуемый этаж на первый из очереди
            }
            else if (controller.Direction != 2) // Инчае если лифт не выполняет действий на этаже
            {
                if (currentFloor > MainFloor) controller.Direction = -1; // Если текущий этаж выше нужного, то отправить лифт вниз
                if (currentFloor < MainFloor) controller.Direction = 1; // Если текущий этаж ниже нужного, то отправить лифт вверх
            }
            else if (controller.Direction == 2 || controller.stateElevator == StateElevator.wait) // Инчае если лифт выполняет действий на этаже
            {
                if (queue.Count != 0 && MainFloor == currentFloor) MainFloor = queue[0]; // Если очередь не пуста и достигли требуемого этажа, то установить требуемый этаж на первый из очереди
            }
        }
    }
}
