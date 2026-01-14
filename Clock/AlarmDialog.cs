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
		private readonly Dictionary<string, FileAttributes> _urlAttributes = new Dictionary<string, FileAttributes>();
		private string _targetDirectory;
		OpenFileDialog dialog;
		public AlarmDialog()
		{
			InitializeComponent();
			dtpDate.Enabled = false;
			dialog = new OpenFileDialog();
			dialog.FileOk += new CancelEventHandler(IsFileOk);
			_targetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
				// 1. Устанавливаем начальную папку — Рабочий стол
				dialog.RootFolder = Environment.SpecialFolder.Desktop;
				dialog.Description = "Выберите папку:";


				// 2. Показываем диалог
				DialogResult result = dialog.ShowDialog();


				if (result == DialogResult.OK)
				{
					// 3. Сохраняем выбранный путь
					_targetDirectory = dialog.SelectedPath;

					// 4. Обновляем интерфейс
					labelFilename.Text = _targetDirectory;
				}
			}
			//1.Находим все.url - файлы в целевой папке и скрываем их
			HideAllUrlFilesInDirectory(_targetDirectory);

			// 2. Открываем диалог
			dialog = new OpenFileDialog();
			{
				dialog.InitialDirectory = _targetDirectory;
				dialog.Filter = "All_sound_files(*.mp3;*.flac;*.flacc)|*.mp3;*.flac;*.flacc|" +
								"mp3_files(*.mp3)|*.mp3|" +
								"Flac_files(*.flac;*flacc)|*.flac;*flacc";
				dialog.Title = "Выберите файлы (URL-ярлыки скрыты)";


				if (dialog.ShowDialog() == DialogResult.OK)
					labelFilename.Text = dialog.FileName;
			}

			// 3. Восстанавливаем атрибуты всех .url-файлов
			RestoreUrlFilesAttributes();		
		}

		private void HideAllUrlFilesInDirectory(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
				return;

			string[] urlFiles = Directory.GetFiles(directoryPath, "*.url", SearchOption.TopDirectoryOnly);

			foreach (string filePath in urlFiles)
			{
				try
				{
					// Сохраняем исходные атрибуты
					FileAttributes original = File.GetAttributes(filePath);
					_urlAttributes[filePath] = original;


					// Устанавливаем атрибут Hidden
					File.SetAttributes(filePath, original | FileAttributes.Hidden);
				}
				catch (Exception ex)
				{
					// Логируем ошибку (например, нет прав)
					MessageBox.Show($"Не удалось скрыть {filePath}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void RestoreUrlFilesAttributes()
		{
			foreach (var kvp in _urlAttributes)
			{
				string filePath = kvp.Key;
				FileAttributes originalAttrs = kvp.Value;

				if (File.Exists(filePath))
				{
					try
					{
						File.SetAttributes(filePath, originalAttrs);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Не удалось восстановить {filePath}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			// Очищаем кэш после восстановления
			_urlAttributes.Clear();
		}
	}
}
