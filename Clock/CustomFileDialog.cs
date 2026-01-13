using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Clock
{
	public class CustomFileDialog
	{
		private readonly Dictionary<string, FileAttributes> _originalAttributes = new Dictionary<string, FileAttributes>();

		private bool _filesWereHidden = false;
		private OpenFileDialog fileDialog = new OpenFileDialog();

		public string Filter
		{
			get => fileDialog.Filter;
			set => fileDialog.Filter = value;
		}

		public string Title
		{
			get => fileDialog.Title;
			set => fileDialog.Title = value;
		}

		protected bool ValidateNames()
		{
			try
			{
				_filesWereHidden = false;
				_originalAttributes.Clear();

				foreach (string fileName in FileNames)
				{
					if (!File.Exists(fileName))
					{
						MessageBox.Show($"Файл не найден:\n{fileName}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}

					string ext = Path.GetExtension(fileName).ToLower();

					// Если это ярлык — скрываем
					if (ext == ".lnk" || ext == ".url")
					{
						try
						{
							// Сохраняем исходные атрибуты
							FileAttributes original = File.GetAttributes(fileName);
							_originalAttributes[fileName] = original;


							// Устанавливаем атрибут "скрытый"
							File.SetAttributes(fileName, original | FileAttributes.Hidden);
							_filesWereHidden = true;

							//MessageBox.Show(
							//	($"Файл \"{fileName}\" скрыт (атрибут \"Скрытый\" установлен).",
							//	"Информация",
							//	MessageBoxButtons.OK,
							//	MessageBoxIcon.Information);
						}
						catch (Exception ex)
						{
							MessageBox.Show
								($"Не удалось скрыть файл:\n{ex.Message}",
								"Ошибка",
								MessageBoxButtons.OK,
								MessageBoxIcon.Error);
							return false; // Отменяем выбор при ошибке скрытия
						}
					}
				}

				return true; // Продолжаем — файлы выбраны и (возможно) скрыты
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка проверки файлов:\n{ex.Message}", "Системная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		protected void Reset()
		{
			base.Reset();
			RestoreFiles(); // Восстанавливаем при сбросе диалога
		}

		// Метод для восстановления атрибутов
		private void RestoreFiles()
		{
			if (!_filesWereHidden) return;


			foreach (var kvp in _originalAttributes)
			{
				try
				{
					File.SetAttributes(kvp.Key, kvp.Value);
				}
				catch (Exception ex)
				{
					// Логируем, но не прерываем — дальше диалог уже закрыт
					Console.WriteLine($"Не удалось восстановить атрибуты для {kvp.Key}: {ex.Message}");
				}
			}

			_originalAttributes.Clear();
			_filesWereHidden = false;
		}

		// Переопределяем ShowDialog, чтобы гарантировать восстановление при закрытии
		public new DialogResult ShowDialog()
		{
			DialogResult result = base.ShowDialog();


			// Если диалог закрыт (OK/Cancel), восстанавливаем файлы
			RestoreFiles();
			return result;
		}
	}

			return fileDialog.ShowDialog();
		}

		public string FileName => fileDialog.FileName;
	}
}
