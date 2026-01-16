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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Clock
{
	public partial class AlarmDialog : Form
	{
		OpenFileDialog fileDialog;

		public Alarm EditAlarm { get; set; }
		public Alarm alarms { get; set; }
		public AlarmDialog()
		{
			InitializeComponent();
			dtpDate.Enabled = false;
			fileDialog = new OpenFileDialog();
			fileDialog.Filter =
				"All_sound_files(*.mp3;*.flac;*.flacc)|*.mp3;*.flac;*.flacc|" +
				"mp3_files(*.mp3)|*.mp3|" +
				"Flac_files(*.flac;*flacc)|*.flac;*flacc";
			fileDialog.FileOk += new CancelEventHandler(IsFileOk);
			//alarm = new Alarm();
		}

		public AlarmDialog(Alarm alarms)
		{
			InitializeComponent ();
			if(alarms != null)  EditAlarm = new Alarm(alarms);
			else EditAlarm = new Alarm();
			LoadData();
		}

		private void LoadData()
		{
			dtpDate.Value = EditAlarm.Date;
			dtpTime.Value = EditAlarm.Time;
			labelFilename.Text = EditAlarm.Filename;

			clbWeekDays.Items.Clear();

			bool[] checkedStates = EditAlarm.Days.GetStates();

			for (int i = 0; i < EditAlarm.Days.Count; i++)
			{
				string dayName = EditAlarm.Days.GetDayName(i);
				clbWeekDays.Items.Add(dayName, checkedStates[i]);
			}
			
		}
		private void IsFileOk(object sender, CancelEventArgs e)
		{
			string ext = Path.GetExtension(fileDialog.FileName).ToLower();
			if (ext == ".lnk" || ext == ".url")
			{
				e.Cancel = true;
				return;
			}
		}

		private void checkBoxUseDate_CheckedChanged(object sender, EventArgs e)
		{
			dtpDate.Enabled = (sender as CheckBox).Checked;
			clbWeekDays.Enabled = !dtpDate.Enabled;
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			if (fileDialog.ShowDialog() == DialogResult.OK)	
				labelFilename.Text = fileDialog.FileName;			
		}

		private void clbWeekDays_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			
		}

		private void clbWeekDays_SelectedIndexChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < clbWeekDays.CheckedItems.Count; i++)
				Console.Write($"{clbWeekDays.CheckedItems[i]}\t");
			Console.WriteLine();

			byte days = 0;
			for (int i = 0; i < clbWeekDays.CheckedIndices.Count; i++)
			{
				days |= (byte)(1 << clbWeekDays.CheckedIndices[i]);
				Console.Write($"{clbWeekDays.CheckedIndices[i]}\t"); 
			}
			Console.WriteLine($"Days mask: {days}");
			Console.WriteLine("\n-------------------------------------\n");
		}

		byte GetDaysMask()
		{
			byte days = 0;
			for (int i = 0; i < clbWeekDays.CheckedIndices.Count; i++)
				days |= (byte)(1 << clbWeekDays.CheckedIndices[i]);
			return days;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (EditAlarm == null) throw new InvalidOperationException("Alarm is null");
			if (clbWeekDays == null) throw new InvalidOperationException("clbWeekDays is null");

			EditAlarm.Date = checkBoxUseDate.Checked ? dtpDate.Value : DateTime.MaxValue;
			EditAlarm.Time = dtpTime.Value;
			EditAlarm.Days = new Week(GetDaysMask());
			EditAlarm.Filename = labelFilename.Text;
			EditAlarm.CheckedStates = new bool[clbWeekDays.Items.Count];
			for (int i = 0; i < clbWeekDays.Items.Count; i++)
			{
				EditAlarm.CheckedStates[i] = clbWeekDays.GetItemChecked(i);
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCansel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
