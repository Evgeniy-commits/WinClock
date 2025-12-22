using ClockApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClockApplication
{
	public partial class DigitalClock : UserControl
	{
		private Timer timer;
		private Label labelTime;
		public DigitalClock()
		{
			InitializeComponent();
			//this.MaximizeBox = false;
			//this.MinimizeBox = false;

			//void SetVisibility(bool visible)
			//{
			//	cbShowDate.Visible = visible;
			//	cbShowWeekDay.Visible = visible;
			//	btnHideControls.Visible = visible;
			//	this.ShowInTaskbar = visible;
			//	this.FormBorderStyle = visible ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
			//	this.TransparencyKey = visible ? Color.Empty : this.BackColor;
			//}

			this.DoubleBuffered = true;
			labelTime = new Label
			{
				Font = new Font("Consolas", 24, FontStyle.Bold),
				ForeColor = Color.Navy,
				TextAlign = ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill
			};
			this.Controls.Add(labelTime);

			timer = new Timer { Interval = 1000 };
			timer.Tick += Timer_Tick;
		}
		private void Timer_Tick(object sender, EventArgs e)
		{
			labelTime.Text = DateTime.Now.ToString
				(
					"hh:mm:ss tt",
					System.Globalization.CultureInfo.InvariantCulture
				);
		}
		//if (cbShowDate.Checked)
		//	labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
		//if (cbShowWeekDay.Checked)
		//	labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
		//notifyIcon.Text = labelTime.Text;
		public void Start() => timer.Start();
		public void Stop() => timer.Stop();


		//private void btnHideControls_Click(object sender, EventArgs e)
		//{
		//	SetVisibility(false);
		//}

		//private void labelTime_MouseHover(object sender, EventArgs e)
		//{
		//	SetVisibility(true);
		//}

		//private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		//{
		//	this.TopMost = true;
		//	this.TopMost = false;
		//}
	}
}

