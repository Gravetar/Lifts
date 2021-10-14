using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lifts
{
    class Visualizer
    {
        public List<PictureBox> ElevatorsVis = new List<PictureBox>();
        public List<GroupBox> FloorVis = new List<GroupBox>();
        public GroupBox ElevatorView;

        private TextBox tbFloor;
        private Label lbDirection;

        public Visualizer()
        {
            Point position = new Point(Settings.ELEVATORSIZE.Width, Settings.ELEVATORSIZE.Height * Settings.FLOORCOUNT-100);

            for (int i = 0; i < Settings.ELEVATORS; i++)
            { 
                PictureBox newPictureBox = new PictureBox();
                newPictureBox.Name = "ElevatorPic_" + i.ToString();
                newPictureBox.Location = position;
                newPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                position.X += Settings.ELEVATORSIZE.Width;
                newPictureBox.Size = Settings.ELEVATORSIZE;
                ElevatorsVis.Add(newPictureBox);
            }

            position = new Point(0, Settings.ELEVATORSIZE.Height * Settings.FLOORCOUNT-100);
            for (int i = 0; i < Settings.FLOORCOUNT; i++)
            {
                GroupBox newgroupbox = new GroupBox();
                newgroupbox.Size = Settings.ELEVATORSIZE;
                newgroupbox.Location = position;
                position.Y -= Settings.ELEVATORSIZE.Height;
                newgroupbox.Name = "Floor_" + i.ToString();
                newgroupbox.Text = "Этаж: " + i.ToString();

                Button buttonup = new Button();
                buttonup.Name = "ButU_" + i.ToString();
                buttonup.Text = "U";
                buttonup.Tag = i.ToString() + "|1";
                buttonup.Size = new Size(30, 30);
                buttonup.Location = new Point(newgroupbox.Size.Width / 3 - 30, newgroupbox.Size.Width / 3);

                Button buttondown = new Button();
                buttondown.Name = "ButD_" + i.ToString();
                buttondown.Text = "D";
                buttondown.Tag = i.ToString() + "|-1";
                buttondown.Size = new Size(30, 30);
                buttondown.Location = new Point(newgroupbox.Size.Width / 3 + 30, newgroupbox.Size.Width / 3);

                if (buttonup.Tag.ToString().Split('|')[0] != (Settings.FLOORCOUNT-1).ToString())
                    newgroupbox.Controls.Add(buttonup);
                if (buttonup.Tag.ToString().Split('|')[0] != "0")
                    newgroupbox.Controls.Add(buttondown);

                FloorVis.Add(newgroupbox);
            }

            #region Внутренность лифта
            //================== КАБИНКА ЛИФТА ==================
            ElevatorView = new GroupBox();
            ElevatorView.AutoSize = true;
            ElevatorView.Size = new Size(0, 0);
            ElevatorView.Location = Settings.ELEVATORINSIDELOCATION;
            ElevatorView.Name = "ElevatorView";
            ElevatorView.Text = "Лифт 0";

            //================== ПАНЕЛЬ КНОПОК ==================
            //Вычисления размера панели кнопок
            int RectSizeX = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Settings.FLOORCOUNT)));
            int RectSizeY = Convert.ToInt32(Math.Ceiling(Math.Sqrt(Convert.ToDouble(Settings.FLOORCOUNT))));

            //Панель кнопок
            TableLayoutPanel ElevatorButtons = new TableLayoutPanel();
            ElevatorButtons.ColumnCount = RectSizeX;
            ElevatorButtons.RowCount = RectSizeY;
            ElevatorButtons.Location = new Point(0, 0);
            ElevatorButtons.Name = "ElevatorButs";
            ElevatorButtons.Size = new Size(0, 0);
            ElevatorButtons.AutoSize = true;
            ElevatorButtons.Left = 10;
            ElevatorButtons.Top = 50;

            //Сколько кнопок уже добавлено:
            int FloorOnBut = 0;
            //Заполнение "матрицы" кнопок лифта
            for (int i = 0; i < ElevatorButtons.ColumnCount; i++)
            {
                for (int j = 0; j < ElevatorButtons.RowCount; j++)
                {
                    if (FloorOnBut < Settings.FLOORCOUNT)
                    {
                        Button buttononfloor = new Button();
                        buttononfloor.Size = new Size(30, 30);
                        buttononfloor.Name = "ButOnFloor_" + FloorOnBut.ToString();
                        buttononfloor.Text = FloorOnBut.ToString();
                        ElevatorButtons.Controls.Add(buttononfloor, j, i);
                        FloorOnBut += 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            //Добавить панель кнопок в кабинку лифта
            ElevatorView.Controls.Add(ElevatorButtons);

            //================== ИНФОРМАЦИОННЫЕ СТЕНДЫ ==================
            // Информация о том, на каком этаже сейчас лифт
            tbFloor = new TextBox();
            tbFloor.Size = new Size(40, 20);
            tbFloor.Left = 10;
            tbFloor.Top = 20;
            tbFloor.ReadOnly = true;
            tbFloor.Name = "tbFloor";
            tbFloor.Text = "0";
            // Информация о том, куда едет сейчас лифт
            lbDirection = new Label();
            lbDirection.Size = new Size(40, 20);
            lbDirection.Left = 60;
            lbDirection.Top = 15;
            lbDirection.AutoSize = true;
            lbDirection.Font = new Font(lbDirection.Font.FontFamily, 12);
            lbDirection.Name = "lbDirection";
            lbDirection.Text = "•";

            ElevatorView.Controls.Add(tbFloor);
            ElevatorView.Controls.Add(lbDirection);
            #endregion
        }

        public void DisplayElevator(List<Elevator> elevators, List<PictureBox> elevatorspic)
        {
            // Положение лифтов
            for (int i = 0; i < elevatorspic.Count(); i++)
            {
                elevatorspic[i].Location = new Point(elevatorspic[i].Location.X, (Settings.ELEVATORSIZE.Height * Settings.FLOORCOUNT)-elevators[i].elevatorDispatcher.controller.CurrentFloor * Settings.ELEVATORSIZE.Height-100);
            }
            // Изменение состояний кнопок UP_DOWN
            foreach (GroupBox floor in FloorVis)
            {
                foreach (Button button in floor.Controls.OfType<Button>().ToList())
                {
                    foreach (Elevator elev in elevators)
                    {
                        if (elev.elevatorDispatcher.controller.CurrentFloor == int.Parse(button.Tag.ToString().Split('|')[0]))
                        {
                            if (elev.elevatorDispatcher.controller.stateElevator == StateElevator.wait)
                            button.Enabled = true;
                        }
                    }
                }
            }

            // Двери лифта
            for (int i = 0; i < elevators.Count(); i++)
            {
                if (elevators[i].elevatorDispatcher.controller.door.IsOpen()) elevatorspic[i].BackgroundImage = new Bitmap(Properties.Resources.ElevatorOpen);
                else elevatorspic[i].BackgroundImage = new Bitmap(Properties.Resources.ElevatorClose);
            }
        }

        public void PriorityDisplay(List<Elevator> elevators, int CurrentElevator)
        {
            // Показ текущего лифта
            ElevatorView.Text = "Лифт № " + CurrentElevator.ToString();
            // Панель текущего этажа
            tbFloor.Text = elevators[CurrentElevator].elevatorDispatcher.controller.CurrentFloor.ToString();
            // Панель текущего направления
            if (elevators[CurrentElevator].elevatorDispatcher.controller.Direction == 1) lbDirection.Text = "↑";
            if (elevators[CurrentElevator].elevatorDispatcher.controller.Direction == -1) lbDirection.Text = "↓";
            if (elevators[CurrentElevator].elevatorDispatcher.controller.Direction == 0) lbDirection.Text = "•";
            if (elevators[CurrentElevator].elevatorDispatcher.controller.Direction == 2) lbDirection.Text = "█";
            // Кнопки
            foreach (Control cnt in ElevatorView.Controls.OfType<TableLayoutPanel>().FirstOrDefault().Controls.OfType<Button>().ToList())
            {
                Button b = cnt as Button;
                if (b != null)
                {
                    b.Enabled = true;
                    foreach (int item in elevators[CurrentElevator].elevatorDispatcher.Queue)
                    {
                        if (b.Text == item.ToString()) b.Enabled = false;
                    }
                }
            }
        }
    }
}
