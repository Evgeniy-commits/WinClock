using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
	public partial class AlarmsForm : Form
	{
		AlarmDialog alarm;
		private Alarm[] alarms;
		public AlarmsForm()
		{
			InitializeComponent();
			alarm = new AlarmDialog();
			//alarms = new Alarm[] { };
			//SetupListBox();

		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			if (alarm.ShowDialog() == DialogResult.OK)
			{
				listBoxAlarms.Items.Add(alarm.Alarm);
			}
		}

		//private void LoadAlarms()
		//{
		//	alarms = new Alarm[] { };
		//}

		private void SetupListBox()
		{
			listBoxAlarms.DataSource = alarms;
			listBoxAlarms.DisplayMember = "Filename";
			listBoxAlarms.MouseDoubleClick += listBoxAlarms_MouseDoubleClick;
		}
		private void listBoxAlarms_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int index = listBoxAlarms.SelectedIndex;
			if (index != -1)
			{
				Alarm selectedAlarm = alarms[index];
				OpenAlarmDialog(selectedAlarm, index);
			}
		}

		private void OpenAlarmDialog(Alarm alarm, int index)
		{
			AlarmDialog dialog = new AlarmDialog();
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				alarms[index] = dialog.EditAlarm;
				ListClear();
			}
			dialog.Dispose();
		}

		private void ListClear()
		{ 
			listBoxAlarms.DataSource = null;
			listBoxAlarms.DataSource = alarms;
		}
	}
}
