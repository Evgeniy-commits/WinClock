using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClock
{
	public partial class MainFormA : Form

	{
		private DateTime currentTime;
		public MainFormA()
		{
			InitializeComponent();
			timerClock.Tick += TimerClock_Tick;
			timerClock.Start();
			this.MaximizeBox = false;
			this.MinimizeBox = false;
		}

		private void TimerClock_Tick(object sender, EventArgs e)
		{
			currentTime = DateTime.Now;
			// Перерисовываем 20 раз в секунду
			this.Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// текущее время
			DateTime dateTimeNow = DateTime.Now;

			int width = this.ClientSize.Width;
			int height = this.ClientSize.Height;
			int centerX = width / 2;
			int centerY = height / 2;
			int radius = Math.Min(centerX, centerY) - 10;

			//Циферблат

			g.FillEllipse(Brushes.Silver, centerX - radius, centerY - radius, radius * 2, radius * 2);
			g.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);

			// Деления
			for (int i = 0; i < 12; i++)
			{ 
				double angle = i * Math.PI / 6;

				int x = (int)(centerX + radius * 0.85 * Math.Sin(angle));
				int y = (int)(centerY - radius * 0.85 * Math.Cos(angle));
				g.FillEllipse(Brushes.Black, x - 2, y - 2, 4, 4);
			}

			for (int i = 1; i <= 12; i++)
			{
				double angle = i * Math.PI / 6;
				int x = (int)(centerX + radius * 0.9 * Math.Sin(angle));
				int y = (int)(centerY - radius * 0.9 * Math.Cos(angle));
				TextRenderer.DrawText(g, i.ToString(), this.Font, new Point(x - 5, y - 10), Color.Black);
			}
			//Углы стрелок
			double secondAngle = (currentTime.Second + currentTime.Millisecond / 1000.0) * (2 * Math.PI / 60);
			double minuteAngle = (currentTime.Minute + currentTime.Second / 60.0) * (2 * Math.PI / 60);
			double hourAngle = (currentTime.Hour % 12 + currentTime.Minute / 60.0) * (2 * Math.PI / 12);
			
			//Рисуем стрелки
			DrawHand(g, centerX, centerY, hourAngle, radius * 0.5, 6, Pens.Black);
			DrawHand(g, centerX, centerY, minuteAngle, radius * 0.7, 4, Pens.DarkBlue);
			DrawHand(g, centerX, centerY, secondAngle, radius * 0.8, 2, Pens.DarkMagenta);
		}

		private void DrawHand
			(
			Graphics g,
			int centerX, int centerY,
			double angle, double length, int width,
			Pen pen 
			)
		{
			int x = (int)(centerX + length * Math.Sin(angle));
			int y = (int)(centerY - length * Math.Cos(angle));

			// Утолщаем стрелку
			Pen widePen = new Pen(pen.Color, 8);
			g.DrawLine(widePen, centerX, centerY, x, y);
		}
	}
}
