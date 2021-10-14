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
        Elevator elev = new Elevator();
        Scheduler scheduler = new Scheduler();
        Visualizer visualizer = new Visualizer();

        DEBUGGER _DEBUGGER;

        private void StartBuilding()
        {
            Controls.AddRange(visualizer.ElevatorsVis.ToArray());
            Controls.AddRange(visualizer.FloorVis.ToArray());
            Controls.Add(visualizer.ElevatorView);

            TableLayoutPanel ElView = visualizer.ElevatorView.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
            List<Button> ElButs = ElView.Controls.OfType<Button>().ToList();

            foreach (Button item in ElButs)
            {
                item.Click += button1_Click;
            }
            foreach (GroupBox itemgb in Controls.OfType<GroupBox>().Where(x => x.Name.Contains("Floor_")).ToList())
            {
                foreach (Button itemb in itemgb.Controls.OfType<Button>().Where(x => x.Name.Contains("ButU_") || x.Name.Contains("ButD_")).ToList())
                {
                    itemb.Click += button4_Click;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
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
            richTextBox1.Text = _DEBUGGER.print(true, true);

            visualizer.Display(scheduler.elevators, Controls.OfType<PictureBox>().Where(x => x.Name.Contains("ElevatorPic_")).ToList());

            foreach (Control cnt in groupBox1.Controls)
            {
                Button tb = cnt as Button;
                if (tb != null)
                {
                    tb.Enabled = true;
                    foreach (int item in scheduler.elevators[0].elevatorDispatcher.Queue)
                    {
                        if (tb.Text == item.ToString()) tb.Enabled = false;
                    }
                }
            }
            textBox1.Text = scheduler.elevators[0].elevatorDispatcher.controller.CurrentFloor.ToString();
            string dir = "";

            if (scheduler.elevators[0].elevatorDispatcher.controller.Direction == 1) dir = "Вверх";
            if (scheduler.elevators[0].elevatorDispatcher.controller.Direction == -1) dir = "Вниз";
            if (scheduler.elevators[0].elevatorDispatcher.controller.Direction == 0) dir = "Стоит";
            if (scheduler.elevators[0].elevatorDispatcher.controller.Direction == 2) dir = "Двери";
            label1.Text = dir;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scheduler.elevators[0].elevatorDispatcher.AddFloor(int.Parse((sender as Button).Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int floor = int.Parse((sender as Button).Tag.ToString().Split('|')[0]);
            int direction = int.Parse((sender as Button).Tag.ToString().Split('|')[1]);
            scheduler.AddRequest(direction, floor);
        }
    }
}
