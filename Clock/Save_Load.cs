using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.IO;
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

		public static void SaveAlarmToFile(Alarm alarm, string filename)
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..");
			try
			{
				if (alarm != null)
				{
					StreamWriter writer = new StreamWriter(filename);
					DateTime? nextAlarm = alarm.NextDate(DateTime.Now);
					writer.WriteLine(nextAlarm.Value.ToString("yyyy-MM-dd HH:mm:ss"));
					//System.Diagnostics.Process.Start("notepad", "alarmWakeUp.ini");
					writer.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении в файл: {ex.Message}");
			}
		}

		public static void SetTask(Alarm alarm)
		{
			if (alarm == null)
			{
				MessageBox.Show("Ошибка: объект Alarm не передан.");
				return;
			}

			// Получаем следующее время срабатывания будильника
			DateTime? nextAlarm = alarm.NextDate(DateTime.Now);

			// Проверяем, что время рассчитано
			if (!nextAlarm.HasValue)
			{
				MessageBox.Show("Ошибка: не удалось определить время срабатывания будильника.");
				return;
			}

			// Форматируем для отображения (опционально)
			string formattedTime = nextAlarm.Value.ToString("yyyy-MM-dd HH:mm:ss");
			//MessageBox.Show($"Задача запланирована на: {formattedTime}");

			TaskService ts = new TaskService();

			// 1. Создаём определение задачи
			TaskDefinition td = ts.NewTask();
			td.RegistrationInfo.Description = "WakeUp";
			td.RegistrationInfo.Author = "Evgen";

			// 2. Настраиваем триггеры (когда запускать)
			TimeTrigger trigger = new TimeTrigger(nextAlarm.Value);
			DateTime triggerTime = nextAlarm.Value.Subtract(TimeSpan.FromMinutes(2));
			TimeTrigger trig = new TimeTrigger(triggerTime);
			td.Triggers.Add(trig);


			// 3. Добавляем действие (что запускать)
			td.Actions.Add(new ExecAction
				(
				"C:\\Users\\Admin\\source\\repos\\WinClock\\Clock\\bin\\Debug\\Clock.exe",
				arguments: "",
				workingDirectory: ""
			));

			// 4. Дополнительные настройки (опционально)
			td.Settings.ExecutionTimeLimit = TimeSpan.FromMinutes(5); // Макс. время выполнения
			td.Settings.StopIfGoingOnBatteries = false; // Не останавливать при питании от батареи
			td.Settings.DisallowStartIfOnBatteries = false;
			td.Settings.WakeToRun = true;

			ts.RootFolder.DeleteTask("WakeUp");
			// 5. Регистрируем задачу
			ts.RootFolder.RegisterTaskDefinition(
				"WakeUp",
				td,
				TaskCreation.CreateOrUpdate,
				"Evgen",
				"123",
				TaskLogonType.InteractiveToken
			);

			Console.WriteLine("Задача создана!");
			ts.Dispose();



		}
	}
}
