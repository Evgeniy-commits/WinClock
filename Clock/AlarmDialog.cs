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

//	using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Windows.Forms;

//public partial class MainForm : Form
//	{
//		// Храним исходные атрибуты .url-файлов для восстановления
//		private readonly Dictionary<string, FileAttributes> _urlAttributes = new();

//		// Папка, где ищем .url-файлы (можно задать извне)
//		private string _targetDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


//		private void buttonShowDialog_Click(object sender, EventArgs e)
//		{
//			// 1. Находим все .url-файлы в целевой папке и скрываем их
//			HideAllUrlFilesInDirectory(_targetDirectory);


//			// 2. Открываем диалог
//			using (var dialog = new OpenFileDialog())
//			{
//				dialog.InitialDirectory = _targetDirectory;
//				dialog.Filter = "Все файлы (*.*)|*.*";  // или свой фильтр
//				dialog.Title = "Выберите файлы (URL-ярлыки скрыты)";


//				DialogResult result = dialog.ShowDialog();

//				// Здесь можно обработать result (OK/Cancel), но это не влияет на восстановление
//			}

//			// 3. Восстанавливаем атрибуты всех .url-файлов
//			RestoreUrlFilesAttributes();
//		}

//		/// <summary>
//		/// Находит все .url-файлы в директории, сохраняет их атрибуты и делает скрытыми
//		/// </summary>
//		private void HideAllUrlFilesInDirectory(string directoryPath)
//		{
//			if (!Directory.Exists(directoryPath))
//				return;

//			string[] urlFiles = Directory.GetFiles(directoryPath, "*.url", SearchOption.TopDirectoryOnly);

//			foreach (string filePath in urlFiles)
//			{
//				try
//				{
//					// Сохраняем исходные атрибуты
//					FileAttributes original = File.GetAttributes(filePath);
//					_urlAttributes[filePath] = original;


//					// Устанавливаем атрибут Hidden
//					File.SetAttributes(filePath, original | FileAttributes.Hidden);
//				}
//				catch (Exception ex)
//				{
//					// Логируем ошибку (например, нет прав)
//					MessageBox.Show($"Не удалось скрыть {filePath}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//				}
//			}
//		}

//		/// <summary>
//		/// Восстанавливает атрибуты всех сохранённых .url-файлов
//		/// </summary>
//		private void RestoreUrlFilesAttributes()
//		{
//			foreach (var kvp in _urlAttributes)
//			{
//				string filePath = kvp.Key;
//				FileAttributes originalAttrs = kvp.Value;

//				if (File.Exists(filePath))
//				{
//					try
//					{
//						File.SetAttributes(filePath, originalAttrs);
//					}
//					catch (Exception ex)
//					{
//						MessageBox.Show($"Не удалось восстановить {filePath}:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
//					}
//				}
//			}

//			// Очищаем кэш после восстановления
//			_urlAttributes.Clear();
//		}
//	}
}
