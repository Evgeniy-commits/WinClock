using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Threading.Tasks;

namespace CustomFonts
{
    public class Fonts
	{
		private static PrivateFontCollection fColl = new PrivateFontCollection();
		private static bool fLoad = false;
		public static void LoadFonts(string extension)
		{
			if (string.IsNullOrEmpty(extension))
				throw new ArgumentNullException(nameof(extension), "Extension cannot be null or empty.");

			if (!extension.StartsWith("."))
				throw new ArgumentException("Extension must start with a dot (e.g., \".ttf\").", nameof(extension));

			if (fLoad) return;

			Assembly assembly = Assembly.GetExecutingAssembly();
			string[] resourceNames = assembly.GetManifestResourceNames();

			List<string> matchingResources = resourceNames
				.Where(name => name.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
				.ToList();

			if (!matchingResources.Any())
			{
				Console.WriteLine($"No fonts with extension '{extension}' found in resources.");
				return;
			}

			foreach (string resourceName in matchingResources)
			{
				try
				{
					Stream stream = assembly.GetManifestResourceStream(resourceName);
					if (stream == null)
					{
						Console.WriteLine($"Failed to open resource: {resourceName}");
						continue;
					}

					byte[] fontBytes = new byte[stream.Length];
					stream.Read(fontBytes, 0, fontBytes.Length);
					stream.Close();

					IntPtr ptr = Marshal.AllocCoTaskMem(fontBytes.Length);
					Marshal.Copy(fontBytes, 0, ptr, fontBytes.Length);

					fColl.AddMemoryFont(ptr, fontBytes.Length);
					Marshal.FreeCoTaskMem(ptr);

					Console.WriteLine($"Font loaded: {resourceName}");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error loading font {resourceName}: {ex.Message}");
				}
			}
			fLoad = true;
		}
	}
}
