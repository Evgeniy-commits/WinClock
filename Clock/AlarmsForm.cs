using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
	public partial class AlarmsForm : Form
	{
		AlarmDialog alarm;
		Alarm alarmLoad;
		public ListBox List { get => listBoxAlarms; }
		public AlarmsForm()
		{
			InitializeComponent();
			alarm = new AlarmDialog();
			LoadAlarm();
		}
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			if (alarm.ShowDialog() == DialogResult.OK)
			{
				listBoxAlarms.Items.Add(new Alarm(alarm.Alarm));
			}
		}

		private void listBoxAlarms_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (listBoxAlarms.Items.Count > 0 && listBoxAlarms.SelectedItems != null)
			{
				AlarmDialog alarm = new AlarmDialog(listBoxAlarms.SelectedItem as  Alarm);
				alarm.ShowDialog();
				listBoxAlarms.Items[listBoxAlarms.SelectedIndex] = new Alarm(alarm.Alarm);
			}
			else 
			{
				buttonAdd_Click(sender, e);
			}
		}

		private void AlarmsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveAlarm();
		}
		void SaveAlarm()
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			StreamWriter writer = new StreamWriter("Alarm.ini");

			foreach (Alarm Alarm in listBoxAlarms.Items)
			{
				writer.WriteLine(Alarm.ToString());
			}

			writer.Close();

			System.Diagnostics.Process.Start("notepad", "Alarm.ini");
		}

		void LoadAlarm()
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			try
			{
				StreamReader reader = new StreamReader("Alarm.ini");
				List<string> lines = new List<string>();
				string line;
				
				while ((line = reader.ReadLine()) != null)
					lines.Add(line);
				foreach (string i in lines)
				{
					Alarm alarmLoad = new Alarm();
					string[] value = i.Split('\t');
					int j = 0;
					if (DateTime.TryParse(value[j], out DateTime result))
						alarmLoad.Date = DateTime.Parse(value[j++]);
					else 
					{ 
						alarmLoad.Date = DateTime.MaxValue; 
						j++;
					}
					alarmLoad.Time = TimeSpan.Parse(value[j++]);
					alarmLoad.Days = new Week(ParseDays(value[j++]));
					alarmLoad.Filename = value[j++];
					listBoxAlarms.Items.Add(alarmLoad);
				}

				reader.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Settings issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		byte ParseDays(string Days)
		{
			string[] Names = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
			string[] days = Days.Split(',');
			byte daymask = 0;
			foreach (string day in days)
			{
				string editstr = day.Trim();
				for (int i = 0; i < Names.Length; i++)
				{
					if (editstr == Names[i])
					{
						daymask |= (byte)(1 << i);
						break;
					}
				}
			}
			return daymask;
		}

	}
}
