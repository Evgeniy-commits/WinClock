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
		public ListBox List { get => listBoxAlarms; }
		
		public AlarmsForm()
		{
			InitializeComponent();
			alarm = new AlarmDialog();
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			//AlarmDialog alarm = new AlarmDialog();
			//alarm.LoadAlarm();
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
			AlarmDialog dialog = new AlarmDialog(this.listBoxAlarms);
			alarm.SaveAlarm(listBoxAlarms);
		}
	}
}
