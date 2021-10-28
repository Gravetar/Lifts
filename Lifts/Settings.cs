using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lifts
{
    class Settings
    {
        /// <summary>
        /// Количество этажей
        /// </summary>
        public static int FLOORCOUNT = 7;
        /// <summary>
        /// Количество лифтов
        /// </summary>
        public static int ELEVATORS = 7;

        /// <summary>
        /// Размер лифта(изображение)
        /// </summary>
        public static Size ELEVATORSIZE = new Size(100, 130);
        /// <summary>
        /// Размер внутренностей лифта(Минимальный)
        /// </summary>
        public static Size ELEVATORINSIDESIZE = new Size(200, 220);

        /// <summary>
        /// Положение внутренности лифта
        /// </summary>
        public static Point ELEVATORINSIDELOCATION = new Point(790, 0);
    }
}
