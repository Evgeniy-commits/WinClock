using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
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
		AlarmsForm alarms;
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
			alarms = new AlarmsForm();
			LoadSettings();
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
		
		void SaveSettings()
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			StreamWriter writer = new StreamWriter("Settings.ini");

			writer.WriteLine(this.Location.X);
			writer.WriteLine(this.Location.Y);

			writer.WriteLine(tsmiTopmost.Checked);
			writer.WriteLine(tsmiShowControls.Checked);
			writer.WriteLine(tsmiShowConsole.Checked);

			writer.WriteLine(tsmiShowDate.Checked);
			writer.WriteLine(tsmiShowWeekDay.Checked);
			writer.WriteLine(tsmiAutoStart.Checked);

			writer.WriteLine(labelTime.BackColor.ToArgb());
			writer.WriteLine(labelTime.ForeColor.ToArgb());

			writer.WriteLine(fontDialog.Filename);
			writer.WriteLine(labelTime.Font.Size);

			writer.Close();

			System.Diagnostics.Process.Start("notepad", "Settings.ini");
		}

		void LoadSettings()
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			try
			{
				StreamReader reader = new StreamReader("Settings.ini");

				this.Location = new Point
					(
						Convert.ToInt32(reader.ReadLine()),
						Convert.ToInt32(reader.ReadLine())
					);

				this.TopMost = tsmiTopmost.Checked = bool.Parse(reader.ReadLine());
				tsmiShowControls.Checked = bool.Parse(reader.ReadLine());
				tsmiShowConsole.Checked = bool.Parse(reader.ReadLine());

				tsmiShowDate.Checked = bool.Parse(reader.ReadLine());
				tsmiShowWeekDay.Checked = bool.Parse(reader.ReadLine());
				tsmiAutoStart.Checked = bool.Parse(reader.ReadLine());

				labelTime.BackColor = backgroundColorDialog.Color = Color.FromArgb(Convert.ToInt32(reader.ReadLine()));
				labelTime.ForeColor = foregroundColorDialog.Color = Color.FromArgb(Convert.ToInt32(reader.ReadLine()));

				fontDialog = new FontDialog(reader.ReadLine(), reader.ReadLine());
				labelTime.Font = fontDialog.Font;



				reader.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Settings issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
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

		private void tsmiAutoStart_CheckedChanged(object sender, EventArgs e)
		{
			string key_name = "ClockPV_521";
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			if(tsmiAutoStart.Checked) rk.SetValue(key_name, Application.ExecutablePath);
			else rk.DeleteValue(key_name, false);
			rk.Dispose();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveSettings();
		}

		private void tsmiAlarms_Click(object sender, EventArgs e)
		{
			alarms.ShowDialog();
		}
	}
}



//private void buttonAddAlarm_Click(object sender, EventArgs e)
//{
//	if (string.IsNullOrWhiteSpace(textBoxAlarmsName.Text))
//	{
//		MessageBox.Show
//			(
//				"Введите название",
//				"Ошибка",
//				MessageBoxButtons.OK,
//				MessageBoxIcon.Information
//			);
//		return;
//	}

//	if (aCount >= alarms.Length)
//	{
//		MessageBox.Show
//			(
//				$"Лимит: {alarms.Length}",
//				"Ошибка",
//				MessageBoxButtons.OK,
//				MessageBoxIcon.Information
//			);
//		return;
//	}

//	Alarm newAlarm = new Alarm
//	{
//		Name = textBoxAlarmsName.Text,
//		Time = dateTimePicker.Value,
//		IsTrig = false
//	};

//	alarms[aCount] = newAlarm;
//	aCount++;

//	SaveAlarms();
//	UpdateListBox();
//	textBoxAlarmsName.Text = "";
//}

//private void buttonRemoveAlarm_Click(object sender, EventArgs e)
//{
//	if (listBoxAlarms.SelectedIndex == -1) return;

//	int selectedIndex = listBoxAlarms.SelectedIndex;

//	for (int i = selectedIndex; i < aCount - 1; i++)
//	{
//		alarms[i] = alarms[i + 1];
//	}

//	aCount--;
//	SaveAlarms();
//	UpdateListBox();
//}