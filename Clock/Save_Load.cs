using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Clock
{
	public static class Save_Load
	{
		public static void SaveAlarm(ListBox listBoxAlarms)
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			try
			{
				StreamWriter writer = new StreamWriter("Alarm.ini");

				foreach (Alarm Alarm in listBoxAlarms.Items)
				{
					writer.WriteLine(Alarm.ToString());
				}

				writer.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении в файл: {ex.Message}");
			}

			//System.Diagnostics.Process.Start("notepad", "Alarm.ini");
		}
		public static List<Alarm> LoadAlarm(string filename)
		{
			List<Alarm> loadAlarm = new List<Alarm>();
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			if (!File.Exists(filename)) return null;
			try
			{
				StreamReader reader = new StreamReader(filename);
				List<string> lines = new List<string>();
				string line;
				while ((line = reader.ReadLine()) != null)
					lines.Add(line);
				foreach (string i in lines)
				{
					loadAlarm.Add(new Alarm(i));
				}
				reader.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при загрузке из файла: {ex.Message}");
			}
			return loadAlarm;
		}
	}
}
