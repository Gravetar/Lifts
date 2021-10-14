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

        public Visualizer()
        {
            Point position = new Point(Settings.ELEVATORSIZE.Width, Settings.ELEVATORSIZE.Height * Settings.FLOORCOUNT);

            Random rnd = new Random();
            for (int i = 0; i < Settings.ELEVATORS; i++)
            { 
                PictureBox newPictureBox = new PictureBox();
                newPictureBox.Name = "ElevatorPic_" + i.ToString();
                newPictureBox.BackColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                newPictureBox.Location = position;
                position.X += Settings.ELEVATORSIZE.Width;
                newPictureBox.Size = Settings.ELEVATORSIZE;
                ElevatorsVis.Add(newPictureBox);
            }

            position = new Point(0, Settings.ELEVATORSIZE.Height * Settings.FLOORCOUNT);
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

                newgroupbox.Controls.Add(buttonup);
                newgroupbox.Controls.Add(buttondown);

                FloorVis.Add(newgroupbox);
            }

            int RectSizeX = Convert.ToInt32(Math.Sqrt(Convert.ToDouble(Settings.FLOORCOUNT)));
            int RectSizeY = Convert.ToInt32(Math.Ceiling(Math.Sqrt(Convert.ToDouble(Settings.FLOORCOUNT))));


            ElevatorView = new GroupBox();
            ElevatorView.AutoSize = true;
            ElevatorView.Location = new Point(300, 300);
            ElevatorView.Name = "ElevatorView";
            ElevatorView.Text = "Лифт 0";

            TableLayoutPanel ElevatorButtons = new TableLayoutPanel();
            ElevatorButtons.ColumnCount = RectSizeX;
            ElevatorButtons.RowCount = RectSizeY;
            ElevatorButtons.Location = new Point(0, 0);
            ElevatorButtons.Name = "ElevatorButs";

            int FloorOnBut = 0;
            for (int i = 0; i<ElevatorButtons.ColumnCount; i++)
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

            ElevatorView.Controls.Add(ElevatorButtons);
        }

        public void Display(List<Elevator> elevators, List<PictureBox> elevatorspic)
        {
            for (int i = 0; i < elevatorspic.Count(); i++)
            {
                elevatorspic[i].Location = new Point(elevatorspic[i].Location.X, (Settings.ELEVATORSIZE.Height * Settings.FLOORCOUNT)-elevators[i].elevatorDispatcher.controller.CurrentFloor * Settings.ELEVATORSIZE.Height);
            }
        }
    }
}
