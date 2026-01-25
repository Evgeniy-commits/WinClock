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
		public ListBox List { get => listBoxAlarms; }
		public AlarmsForm()
		{
			InitializeComponent();
		}
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			AlarmDialog alarmDialog = new AlarmDialog();
			if (alarmDialog.ShowDialog() == DialogResult.OK)
			{
				listBoxAlarms.Items.Add(new Alarm(alarmDialog.Alarm));
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
			Save_Load.SaveAlarm(listBoxAlarms);
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			listBoxAlarms.Items.RemoveAt(listBoxAlarms.SelectedIndex);
		}
	}
}
