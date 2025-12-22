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
	public partial class commonWindow : Form
	{
		private AnalogClock analogClock;
		private DigitalClock digitalClock;
		private Panel panelDisplay;
		private ContextMenuStrip contextMenuClock;
		private ToolStripMenuItem toolStripAnalog;
		private ToolStripMenuItem toolStripDigital;
		public commonWindow()
		{
			InitializeComponent();

			// Инициализация контролов
			analogClock = new AnalogClock();
			digitalClock = new DigitalClock();

			// Настройка контекстного меню
			ContextMenuStrip contextMenuClock = new ContextMenuStrip();
			ToolStripMenuItem toolStripAnalog = new ToolStripMenuItem("Аналоговые часы");
			ToolStripMenuItem toolStripDigital = new ToolStripMenuItem("Цифровые часы");

			contextMenuClock.Items.Add(toolStripAnalog);
			contextMenuClock.Items.Add(toolStripDigital);

			toolStripAnalog.Click += ToolStripAnalog_Click;
			toolStripDigital.Click += ToolStripDigital_Click;

			this.ContextMenuStrip = contextMenuClock;

			// Обработчик ПКМ
			this.MouseClick += MainForm_MouseClick;

			// Стартовый режим
			ShowDigitalClock();
		}
		// Обработка ПКМ
		private void MainForm_MouseClick(object sender, MouseEventArgs e, ContextMenuStrip contextMenuClock)
		{
			if (e.Button == MouseButtons.Right)
				contextMenuClock.Show(this, e.Location);
		}

		// Показать аналоговые часы
		private void ShowAnalogClock()
		{
			panelDisplay.Controls.Clear();
			panelDisplay.Controls.Add(analogClock);
			analogClock.Dock = DockStyle.Fill;
			analogClock.Start();
		}

		// Показать цифровые часы
		private void ShowDigitalClock()
		{
			panelDisplay.Controls.Clear();
			panelDisplay.Controls.Add(digitalClock);
			digitalClock.Dock = DockStyle.Fill;
			digitalClock.Start();
		}

		// Обработчики меню
		private void ToolStripAnalog_Click(object sender, EventArgs e) => ShowAnalogClock();
		private void ToolStripDigital_Click(object sender, EventArgs e) => ShowDigitalClock();
	}
}
