using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifts
{
    class Motor
    {
        /// <summary>
        /// Статус действий на этаже
        /// 0 - Действия на этаже не выполняются
        /// 1 - Открываются двери
        /// 2 - Закрываются двери
        /// </summary>
        private int StopStatus = 0;

        /// <summary>
        /// Направить лифт вверх на один этаж
        /// </summary>
        /// <param name="floor">Изменяемый этаж(к нему будет +1)</param>
        public void Up(ref int floor)
        {
            if (floor + 1 != Settings.FLOORCOUNT && StopStatus == 0) floor += 1;
        }

        /// <summary>
        /// Направить лифт вниз на один этаж
        /// </summary>
        /// <param name="floor">Изменяемый этаж(к нему будет -1)</param>
        public void Down(ref int floor)
        {
            if (floor - 1 != -1 && StopStatus == 0) floor -= 1;
        }

        /// <summary>
        /// Ожидание
        /// </summary>
        public void Stop()
        {
        }

        /// <summary>
        /// Действия лифта на этаже
        /// </summary>
        /// <param name="Finished">Завершены ли все действия</param>
        public void StopOnFloor(ref bool Finished)
        {
            Finished = false;
            if (StopStatus == 0) { StopStatus = 1; Console.WriteLine("Лифт остановился"); } //Остановка
            else if (StopStatus == 1) {StopStatus = 2; Console.WriteLine("Лифт открыл двери"); } //Открытие двери
            else if (StopStatus == 2) { StopStatus = 3; Console.WriteLine("Лифт закрыл двери"); } //Открытие двери
            else if (StopStatus == 3) {StopStatus = 0; Console.WriteLine("Лифт продолжил выполнять задачи", Finished = true); } // Закрытие двери и ожидание
        }
    }
}
