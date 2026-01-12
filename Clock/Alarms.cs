using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Drawing.Text;

namespace Clock
{
	public partial class AlarmsDialog : Form
	{
		private Alarm[] alarms = new Alarm[256];
		private int aCount = 0;
		private SoundPlayer player;

		public AlarmsDialog()
		{
			InitializeComponent();
			timerClock.Start();
			timerCheck.Start();
		}

		private void timerClock_Tick(object sender, EventArgs e)
		{
			labelCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
		}

		private void timerCheck_Tick(object sender, EventArgs e)
		{
			DateTime now = DateTime.Now;
			for (int i = 0; i < aCount; i++)
			{
				Alarm alarm = alarms[i];
				if (!alarm.IsTrig &&
					now.Hour == alarm.Time.Hour &&
					now.Minute == alarm.Time.Minute &&
					now.Second == alarm.Time.Second) StartAlarm(alarm, i);
			}
		}

		private void StartAlarm(Alarm alarm, int i)
		{
			alarm.IsTrig = true;
			alarms[i] = alarm;
			

			MessageBox.Show
				(
					($"Будильник: {alarm.Name}\n" + $"Время: {alarm.Time:HH:mm}"),
					"Подъем, нас ждут великие дела",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
				);
			UpdateListBox();
		}

		private void UpdateListBox()
		{
			listBoxAlarms.Items.Clear();

			for (int i = 0; i < aCount; i++)
			{
				listBoxAlarms.Items.Add(alarms[i].Name);
			}
		}

		private void buttonAddAlarm_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(textBoxAlarmsName.Text))
			{
				MessageBox.Show
					(
						"Введите название",
						"Ошибка",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
					);
				return;
			}

			if (aCount >= alarms.Length)
			{
				MessageBox.Show
					(
						$"Лимит: {alarms.Length}",
						"Ошибка",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
					);
				return;
			}

			Alarm newAlarm = new Alarm
			{
				Name = textBoxAlarmsName.Text,
				Time = dateTimePicker.Value,
				IsTrig = false
			};

			alarms[aCount] = newAlarm;
			aCount++;

			SaveAlarms();
			UpdateListBox();
			textBoxAlarmsName.Text = "";
		}

		private void buttonRemoveAlarm_Click(object sender, EventArgs e)
		{
			if (listBoxAlarms.SelectedIndex == -1) return;

			int selectedIndex = listBoxAlarms.SelectedIndex;

			for (int i = selectedIndex; i < aCount - 1; i++)
			{
				alarms[i] = alarms[i + 1];
			}

			aCount--;
			SaveAlarms();
			UpdateListBox();
		}

		private void SaveAlarms()
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			StreamWriter writer = new StreamWriter("Alarms.ini");

			for (int i = 0; i < aCount; i++)
			{
				Alarm alarm = alarms[i];
				writer.WriteLine
					(
						$"{alarm.Name}\n" +
						$"{alarm.Time:yyyy-MM-dd hh:mm:ss}\n" +
						$"{alarm.IsTrig}"
					);
			}

			System.Diagnostics.Process.Start("notepad", "Alarms.ini");

			writer.Close();
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			SaveAlarms();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
	public class Alarm
	{
		public string Name { get; set; }
		public DateTime Time { get; set; }
		public bool IsTrig { get; set; }
	}
}
