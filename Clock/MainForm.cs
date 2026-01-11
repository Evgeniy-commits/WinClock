using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Clock
{
	public partial class MainForm : Form
	{
		FontDialog fontDialog;
		ColorDialog foregroundColorDialog;
		ColorDialog backgroundColorDialog;
		public MainForm()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point
				(
					Screen.PrimaryScreen.Bounds.Width - this.Width - 25,
					50
				);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			SetVisibility(false);
			fontDialog = new FontDialog();
			foregroundColorDialog = new ColorDialog();
			backgroundColorDialog = new ColorDialog();
			this.TopMost = tsmiTopmost.Checked = true;
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

		private void tsmiForegroundColor_Click(object sender, EventArgs e)
		{
			DialogResult result = foregroundColorDialog.ShowDialog();
			if (result == DialogResult.OK)
				labelTime.ForeColor = foregroundColorDialog.Color;
		}

		private void tsmiBackgroundColor_Click(object sender, EventArgs e)
		{
			DialogResult result = backgroundColorDialog.ShowDialog();
			if (result == DialogResult.OK)
				labelTime.BackColor = backgroundColorDialog.Color;
		}

		private void tsmiFont_Click(object sender, EventArgs e)
		{
			fontDialog.Location = new Point
				(
					this.Location.X - fontDialog.Width - 10,
					this.Location.Y
				);
			fontDialog.Font = labelTime.Font;
			DialogResult result = fontDialog.ShowDialog();
			if(result == DialogResult.OK)
				labelTime.Font = fontDialog.Font;
		}
	}
}
