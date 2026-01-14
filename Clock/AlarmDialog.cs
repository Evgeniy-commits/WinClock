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
	}

}
