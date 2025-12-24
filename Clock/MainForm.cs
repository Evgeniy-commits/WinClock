using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Clock
{
	public partial class MainForm : Form
	{
		private PrivateFontCollection _privateFonts = new PrivateFontCollection();
		public MainForm()
		{
			InitializeComponent();
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			SetVisibility(false);
			this.StartPosition = FormStartPosition.Manual;

			Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
			this.Location = new Point(
				workingArea.Right - this.Width - 50,
				workingArea.Top + 50
			);
		}
		void SetVisibility(bool visible)
		{
			cbShowDate.Visible = visible;
			cbShowWeekDay.Visible = visible;
			btnHideControls.Visible = visible;
			this.ShowInTaskbar = visible;
			this.FormBorderStyle = visible ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
			this.TransparencyKey = visible ? Color.Empty : this.BackColor;
		}
		System.Drawing.Color SetColor()
		{
			Color selectCol = default;
			ColorDialog dialog = new ColorDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				selectCol = Color.FromArgb
					(
						dialog.Color.A,
						dialog.Color.R,
						dialog.Color.G,
						dialog.Color.B
					);
			}
			return selectCol;
		}
		private void timer_Tick(object sender, EventArgs e)
		{
			labelTime.Text = DateTime.Now.ToString
				(
					"hh:mm:ss tt",
					System.Globalization.CultureInfo.InvariantCulture
				);
			if (cbShowDate.Checked)
				labelTime.Text += $"\n{DateTime.Now.ToString("yyyy.MM.dd")}";
			if (cbShowWeekDay.Checked)
				labelTime.Text += $"\n{DateTime.Now.DayOfWeek}";
			notifyIcon.Text = labelTime.Text ;
		}

		private void btnHideControls_Click(object sender, EventArgs e)
		{
			SetVisibility(tsmiShowControls.Checked = false);
			
		}

		//private void labelTime_MouseHover(object sender, EventArgs e)
		//{
		//	SetVisibility(true);
		//}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!TopMost)
			{
				this.TopMost = true;
				this.TopMost = false; 
			}
		}

		private void tsmiTopmost_Click(object sender, EventArgs e) =>
			this.TopMost = tsmiTopmost.Checked;

		private void tsmiShowControls_CheckedChanged(object sender, EventArgs e)
		{
			SetVisibility((sender as ToolStripMenuItem).Checked);
			//Sender - это отправитель события(control, который прислал событие).
			//Если на элемент окна воздействует пользователь при помощи клавиатуры и мыши,
			//этот control отправляет событие своему родителю, 
			//а родитель может обрабатывать, или не обрабатывать это событие.
		}

		private void tsmiShowDate_CheckedChanged(object sender, EventArgs e) =>		
			cbShowDate.Checked = tsmiShowDate.Checked;

		private void cbShowDate_CheckedChanged(object sender, EventArgs e) =>
			tsmiShowDate.Checked = cbShowDate.Checked;

		private void tsmiShowWeekDay_CheckedChanged(object sender, EventArgs e) =>
			cbShowWeekDay.Checked = tsmiShowWeekDay.Checked;

		private void cbShowWeekDay_CheckedChanged(object sender, EventArgs e) =>
			tsmiShowWeekDay.Checked = cbShowWeekDay.Checked;

		private void tsmiQuit_Click(object sender, EventArgs e) => this.Close();

		private void tsmiForegroundColor_Click(object sender, EventArgs e) =>
			labelTime.BackColor = SetColor();

		private void tsmiBackgroundColor_Click(object sender, EventArgs e)
		{
			this.BackColor = SetColor();
		}

		private void tsmiFont_Click(object sender, EventArgs e)
		{
			FontDialog fontDialog = new FontDialog();
			
			if (fontDialog.ShowDialog() == DialogResult.OK)
			{

				labelTime.Font = fontDialog.Font;
				labelTime.ForeColor = fontDialog.Color;
			}
		}
	}
}

















//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace AnalogClock
//{
//	public partial class MainFormA : Form

//	{
//		private DateTime currentTime;
//		public MainFormA()
//		{
//			InitializeComponent();
//			timerClock.Tick += TimerClock_Tick;
//			timerClock.Start();
//			//this.MaximizeBox = false;
//			//this.MinimizeBox = false;
//		}

//		private void TimerClock_Tick(object sender, EventArgs e)
//		{
//			currentTime = DateTime.Now;
//			this.Invalidate();
//		}

//		protected override void OnPaint(PaintEventArgs e)
//		{
//			base.OnPaint(e);
//			Graphics g = e.Graphics;
//			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

//			// текущее время
//			DateTime dateTimeNow = DateTime.Now;

//			int width = this.ClientSize.Width;
//			int height = this.ClientSize.Height;
//			int centerX = width / 2;
//			int centerY = height / 2;
//			int radius = Math.Min(centerX, centerY) - 20;

//			//Циферблат

//			g.FillEllipse(Brushes.AliceBlue, centerX - radius, centerY - radius, radius * 2, radius * 2);
//			g.DrawEllipse(new Pen(Color.DarkBlue, 3), centerX - radius, centerY - radius, radius * 2, radius * 2);

//			// Деления
//			for (int i = 0; i < 60; i++)
//			{
//				double angle = i * 6 * Math.PI / 180;

//				int x1 = (int)(centerX + (radius - 10) * Math.Sin(angle));
//				int y1 = (int)(centerY - (radius - 10) * Math.Cos(angle));
//				int x2 = (int)(centerX + radius * Math.Sin(angle));
//				int y2 = (int)(centerY - radius * Math.Cos(angle));
//				if (i % 5 == 0)
//					g.DrawLine(new Pen(Color.Blue, 3), x1, y1, x2, y2);
//				else
//					g.DrawLine(new Pen(Color.DarkBlue, 1), x1, y1, x2, y2);
//			}

//			//Углы стрелок
//			double secondAngle = (currentTime.Second + currentTime.Millisecond / 1000.0) * (2 * Math.PI / 60);
//			double minuteAngle = (currentTime.Minute + currentTime.Second / 60.0) * (2 * Math.PI / 60);
//			double hourAngle = (currentTime.Hour % 12 + currentTime.Minute / 60.0) * (2 * Math.PI / 12);

//			//Рисуем стрелки
//			DrawHand(g, centerX, centerY, hourAngle, radius * 0.5, 6, Pens.Black);
//			DrawHand(g, centerX, centerY, minuteAngle, radius * 0.7, 4, Pens.DarkBlue);
//			DrawHand(g, centerX, centerY, secondAngle, radius * 0.8, 2, Pens.Coral);

//			DrawNumber(g, centerX, centerY, radius, "12", 0);
//			DrawNumber(g, centerX, centerY, radius, "3", 90);
//			DrawNumber(g, centerX, centerY, radius, "6", 180);
//			DrawNumber(g, centerX, centerY, radius, "9", 270);
//		}

//		private void DrawHand
//			(
//			Graphics g,
//			int centerX, int centerY,
//			double angle, double length, int width,
//			Pen pen
//			)
//		{
//			int x = (int)(centerX + length * Math.Sin(angle));
//			int y = (int)(centerY - length * Math.Cos(angle));

//			//стрелки толще 
//			Pen widePen = new Pen(pen.Color, 8);
//			g.DrawLine(widePen, centerX, centerY, x, y);
//		}


//		private void DrawNumber(Graphics g, int centerX, int centerY, int radius, string text, double angleDeg)
//		{
//			angleDeg -= 90;
//			double angleRad = angleDeg * Math.PI / 180;

//			// Позиция цифры
//			int x = (int)(centerX + (radius * 0.9) * Math.Cos(angleRad));
//			int y = (int)(centerY + (radius * 0.9) * Math.Sin(angleRad));

//			// Тень
//			SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
//			g.DrawString(text, new Font("Arial", 14, FontStyle.Bold), shadowBrush, x - 2, y - 2);

//			// Основная цифра
//			g.DrawString(text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x - 10, y - 10);
//		}
//	}
//}
