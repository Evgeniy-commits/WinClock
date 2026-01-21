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
		public Alarm Alarm { get; private set; }
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
			Alarm = new Alarm();
			for (int i = 0; i < 7; i++)	clbWeekDays.SetItemChecked(i, true);
		}

		public AlarmDialog(Alarm alarm) : this()
		{
			Alarm = alarm;
			Extract();
		}
		void Extract()
		{
			if(Alarm.Date != DateTime.MaxValue) 
			{
				dtpDate.Value = Alarm.Date;
				checkBoxUseDate.Checked = true;
			}
			dtpTime.Value = DateTime.Now.Date + Alarm.Time;
			Alarm.Days.Extract(clbWeekDays); 
			labelFilename.Text = Alarm.Filename;
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
			if(clbWeekDays.CheckedIndices.Count == 0)
			{
				MessageBox.Show
					(
						this,
						"Выберите хотя бы один день недели",
						"",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
					);
			}
			Alarm.Date = checkBoxUseDate.Checked ? dtpDate.Value : DateTime.MaxValue;
			Alarm.Time = dtpTime.Value.TimeOfDay;
			Alarm.Days = new Week(GetDaysMask());
			if (Alarm.Days.GetMask() == 0) Alarm.Days = new Week(127);
			Alarm.Filename = labelFilename.Text;
		}
	}
}
