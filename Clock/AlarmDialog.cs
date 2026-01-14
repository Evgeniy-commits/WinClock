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
		private readonly Dictionary<string, FileAttributes> Attributes = new Dictionary<string, FileAttributes>();
		private string directory;
		OpenFileDialog dialog;
		public AlarmDialog()
		{
			InitializeComponent();
			dtpDate.Enabled = false;
			dialog = new OpenFileDialog();

			dialog.FileOk += new CancelEventHandler(IsFileOk);
			directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		private void IsFileOk(object sender, CancelEventArgs e)
		{
			string ext = Path.GetExtension(dialog.FileName).ToLower();
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
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.RootFolder = Environment.SpecialFolder.Desktop;
				dialog.Description = "Выберите папку:";
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					directory = dialog.SelectedPath;
					labelFilename.Text = directory;
				}
			}

			FindUrl(directory);

			dialog = new OpenFileDialog();
			dialog.InitialDirectory = directory;
			dialog.Filter = "All_sound_files(*.mp3;*.flac;*.flacc)|*.mp3;*.flac;*.flacc|" +
							"mp3_files(*.mp3)|*.mp3|" +
							"Flac_files(*.flac;*flacc)|*.flac;*flacc";
			dialog.Title = "Выберите файлы (URL-ярлыки скрыты)";
			if (dialog.ShowDialog() == DialogResult.OK)
				labelFilename.Text = dialog.FileName;

			RestoreUrl();
		}

		private void FindUrl(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
				return;

			string[] urlFiles = Directory.GetFiles(directoryPath, "*.url", SearchOption.TopDirectoryOnly);

			foreach (string filePath in urlFiles)
			{
				try
				{
					FileAttributes original = File.GetAttributes(filePath);
					Attributes[filePath] = original;
										
					File.SetAttributes(filePath, original | FileAttributes.Hidden);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Не удалось скрыть {filePath}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void RestoreUrl()
		{
			foreach (KeyValuePair<string, FileAttributes> pair in Attributes)
			{
				string filePath = pair.Key;
				FileAttributes original = pair.Value;

				if (File.Exists(filePath))
				{
					try
					{
						File.SetAttributes(filePath, original);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Не удалось восстановить {filePath}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			Attributes.Clear();
		}
	}
}
