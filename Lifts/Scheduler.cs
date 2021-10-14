using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class Scheduler
    {
        public List<Elevator> elevators = new List<Elevator>();

        public Scheduler()
        {
            for (int i = 0; i < Settings.ELEVATORS; i++)
            {
                elevators.Add(new Elevator());
            }
        }

        public void AddRequest(int direction, int floor)
        {
            //Проверка лифта на то, что он стоит на данном этаже
            if (CheckWaiting(floor)) return;
            //Проверка лифта на совпадения направления
            else if (CheckByDirection(floor, direction)) return;
            //Проверка, есть ли свободные лифты и какой из них ближайший
            else if (CheckNear(floor)) return;
            //Установить лифт, который освободится в ближайшее время
            else if (CheckNearFree(floor)) return;
        }

        bool CheckNear(int floor)
        {
            Elevator elevmin = null;
            int min = 100;
            foreach (Elevator item in elevators)
            {
                if (item.elevatorDispatcher.controller.stateElevator == StateElevator.wait)
                {
                    if (Math.Abs(floor - item.elevatorDispatcher.controller.CurrentFloor) < min)
                    {
                        elevmin = item;
                        min = Math.Abs(floor - item.elevatorDispatcher.controller.CurrentFloor);
                    }
                }
            }
            if (elevmin == null) return false;
            elevmin.elevatorDispatcher.AddFloor(floor);
            return true; // Запрос обработан, этаж добавлен в очередь
        }

        bool CheckWaiting(int floor)
        {
            foreach (Elevator item in elevators)
            {
                if (item.elevatorDispatcher.controller.stateElevator == StateElevator.wait && item.elevatorDispatcher.controller.CurrentFloor == floor)
                {
                    item.elevatorDispatcher.AddFloor(floor);
                    return true; // Запрос обработан, этаж добавлен в очередь
                }
            }
            return false; //Свободных лифтов нет
        }
        bool CheckByDirection(int floor, int direction)
        {
            //Проверка, есть ли свободные лифты
            foreach (Elevator item in elevators)
            {
                if (item.elevatorDispatcher.controller.Direction == direction)// Определение направления
                {
                    // Если текущий этаж лифта ниже этажа запроса и направление запроса=вверх, то добавить нужный этаж в очередь лифта
                    if (item.elevatorDispatcher.controller.CurrentFloor < floor && direction == 1)
                    {
                        item.elevatorDispatcher.AddFloor(floor);
                        return true; // Запрос обработан, этаж добавлен в очередь
                    }
                    // Если текущий этаж лифта выше этажа запроса и направление запроса=вниз, то добавить нужный этаж в очередь лифта
                    else if (item.elevatorDispatcher.controller.CurrentFloor > floor && direction == -1)
                    {
                        item.elevatorDispatcher.AddFloor(floor);
                        return true; // Запрос обработан, этаж добавлен в очередь
                    }
                }
            }
            return false; //Свободных лифтов нет
        }

        bool CheckNearFree(int floor)
        {
            Elevator NearElevator = elevators[0];
            foreach (Elevator item in elevators)
            {
                if (item.elevatorDispatcher.QueueCount < NearElevator.elevatorDispatcher.QueueCount)
                {
                    NearElevator = item;
                }
            }
            NearElevator.elevatorDispatcher.AddFloor(floor);
            return true; // Запрос обработан, этаж добавлен в очередь
        }

        public void Working()
        {
            foreach (Elevator item in elevators)
            {
                item.elevatorDispatcher.DeterminingDirection();
                item.elevatorDispatcher.controller.Move();
            }
        }
    }
}
