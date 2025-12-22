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
	public partial class AnalogClock : UserControl

	{
		private DateTime currentTime;
		private Timer timer;
		public AnalogClock()
		{
			InitializeComponent();
			timer.Tick += Timer_Tick;
		}

		private void Timer_Tick(object sender, EventArgs e)
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
			int radius = Math.Min(centerX, centerY) - 20;

			//Циферблат

			g.FillEllipse(Brushes.AliceBlue, centerX - radius, centerY - radius, radius * 2, radius * 2);
			g.DrawEllipse(new Pen(Color.DarkBlue, 3), centerX - radius, centerY - radius, radius * 2, radius * 2);

			// Деления
			for (int i = 0; i < 60; i++)
			{
				double angle = i * 6 * Math.PI / 180;

				int x1 = (int)(centerX + (radius - 10) * Math.Sin(angle));
				int y1 = (int)(centerY - (radius - 10) * Math.Cos(angle));
				int x2 = (int)(centerX + radius * Math.Sin(angle));
				int y2 = (int)(centerY - radius * Math.Cos(angle));
				if (i % 5 == 0) // Часовые метки (жирные)
					g.DrawLine(new Pen(Color.Blue, 3), x1, y1, x2, y2);
				else // Минутные деления (тонкие)
					g.DrawLine(new Pen(Color.DarkBlue, 1), x1, y1, x2, y2);
			}

			//Углы стрелок
			double secondAngle = (currentTime.Second + currentTime.Millisecond / 1000.0) * (2 * Math.PI / 60);
			double minuteAngle = (currentTime.Minute + currentTime.Second / 60.0) * (2 * Math.PI / 60);
			double hourAngle = (currentTime.Hour % 12 + currentTime.Minute / 60.0) * (2 * Math.PI / 12);

			//Рисуем стрелки
			DrawHand(g, centerX, centerY, hourAngle, radius * 0.5, 6, Pens.Black);
			DrawHand(g, centerX, centerY, minuteAngle, radius * 0.7, 4, Pens.DarkBlue);
			DrawHand(g, centerX, centerY, secondAngle, radius * 0.8, 2, Pens.DarkMagenta);

			DrawNumber(g, centerX, centerY, radius, "12", 0);
			DrawNumber(g, centerX, centerY, radius, "3", 90);
			DrawNumber(g, centerX, centerY, radius, "6", 180);
			DrawNumber(g, centerX, centerY, radius, "9", 270);
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


		private void DrawNumber(Graphics g, int centerX, int centerY, int radius, string text, double angleDeg)
		{
			angleDeg -= 90; // Корректировка для вертикального положения
			double angleRad = angleDeg * Math.PI / 180;

			// Позиция цифры
			int x = (int)(centerX + (radius * 0.9) * Math.Cos(angleRad));
			int y = (int)(centerY + (radius * 0.9) * Math.Sin(angleRad));

			// Тень
			SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
			g.DrawString(text, new Font("Arial", 14, FontStyle.Bold), shadowBrush, x - 2, y - 2);

			// Основная цифра
			g.DrawString(text, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x - 10, y - 10);
		}
		public void Start() => timer.Start();
		public void Stop() => timer.Stop();
	}
}
