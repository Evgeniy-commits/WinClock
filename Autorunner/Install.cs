using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Claims;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;
using System.Diagnostics;

namespace Autorunner
{
	public partial class Install : Form
	{
		public Install()
		{
			InitializeComponent();
		}

		private void btnComplite_Click(object sender, EventArgs e)
		{
			FileSecurity originalSecurity = null;
			string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			string settingsPath = Path.Combine(
				programFiles,
				"BV_521",
				"Clock",
				"bin",
				"Debug",
				"Settings.ini"
			);
			try
			{
				if (File.Exists(settingsPath))
					originalSecurity = File.GetAccessControl(settingsPath);
				else
					originalSecurity = new FileSecurity();

				SecurityIdentifier everySid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
				FileSystemAccessRule rule = new FileSystemAccessRule
				(
					everySid,
					FileSystemRights.Write,
					InheritanceFlags.None,
					PropagationFlags.NoPropagateInherit,
					AccessControlType.Allow
				);

				FileSecurity fileSecurity = File.Exists(settingsPath)
					? File.GetAccessControl(settingsPath)
					: new FileSecurity();

				fileSecurity.AddAccessRule(rule);
				File.SetAccessControl(settingsPath, fileSecurity);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Нет прав", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			string program = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
			string exePath = Path.Combine(
				program,
				"BV_521",
				"Clock",
				"bin",
				"Debug",
				"Clock.exe"
			);
			Process.Start(new ProcessStartInfo
			{
				FileName = "Clock.exe",
				WorkingDirectory = exePath, // Рабочая директория для процесса
				UseShellExecute = true,        // Важно для корректного запуска .exe
				Verb = "open"                   // Явно указываем действие (опционально)
			});
			this.Close();
		}
	}
}


