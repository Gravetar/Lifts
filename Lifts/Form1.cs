using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lifts
{
    public partial class Form1 : Form
    {
        int CurrentElevator = 0;
        Scheduler scheduler = new Scheduler();
        Visualizer visualizer = new Visualizer();

        DEBUGGER _DEBUGGER;

        private void StartBuilding()
        {
            Controls.AddRange(visualizer.ElevatorsVis.ToArray());
            Controls.AddRange(visualizer.FloorPic.ToArray());
            Controls.AddRange(visualizer.FloorVis.ToArray());
            Controls.Add(visualizer.ElevatorView);

            foreach (PictureBox item in visualizer.ElevatorsVis)
            {
                item.Click += OnElevatorClick;
            }

            TableLayoutPanel ElView = visualizer.ElevatorView.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
            List<Button> ElButs = ElView.Controls.OfType<Button>().ToList();

            foreach (Button item in ElButs)
            {
                item.Click += ButtonsPanelClick;
            }
            foreach (GroupBox itemgb in Controls.OfType<GroupBox>().Where(x => x.Name.Contains("Floor_")).ToList())
            {
                foreach (Button itemb in itemgb.Controls.OfType<Button>().Where(x => x.Name.Contains("ButU_") || x.Name.Contains("ButD_")).ToList())
                {
                    itemb.Click += UpDownButtonsClick;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            TimerPriority.Start();
            _DEBUGGER = new DEBUGGER(scheduler);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartBuilding();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scheduler.Working();

            _DEBUGGER._ticks += 1;
            richTextBox1.Text = _DEBUGGER.print(true, true, true, CurrentElevator);

            visualizer.DisplayElevator(scheduler.elevators, Controls.OfType<PictureBox>().Where(x => x.Name.Contains("ElevatorPic_")).ToList());
        }

        private void ButtonsPanelClick(object sender, EventArgs e)
        {
            OutFocus.Focus();
            if ((sender as Button).Tag.ToString() == "Off")
            {
                scheduler.elevators[CurrentElevator].elevatorDispatcher.AddFloor(int.Parse((sender as Button).Text));
            }
        }

        private void UpDownButtonsClick(object sender, EventArgs e)
        {
            Button button = (sender as Button);
            if (button.Tag.ToString()[button.Tag.ToString().Length-1] == '-')
            {
                int floor = int.Parse((sender as Button).Tag.ToString().Split('|')[0]);
                int direction = int.Parse((sender as Button).Tag.ToString().Split('|')[1]);
                scheduler.AddRequest(direction, floor);
                button.FlatAppearance.BorderSize = 2;
                button.FlatAppearance.BorderColor = Color.Red;

                button.Tag = button.Tag.ToString().Substring(0, button.Tag.ToString().Length - 1) + "+";
            }
        }

        private void OnElevatorClick(object sender, EventArgs e)
        {
            visualizer.ChangedElevator(scheduler.elevators, CurrentElevator);
            CurrentElevator = int.Parse((sender as PictureBox).Name.Split('_')[1]);
        }

        private void TimerPriority_Tick(object sender, EventArgs e)
        {
            visualizer.PriorityDisplay(scheduler.elevators, CurrentElevator);
        }

        private void RepairClick(object sender, EventArgs e)
        {
            scheduler.elevators[CurrentElevator].elevatorDispatcher.AddFloor(0);
        }
    }
}
