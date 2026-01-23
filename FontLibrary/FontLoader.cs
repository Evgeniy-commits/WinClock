using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FontLibrary
{
	public class FontLoader
	{
		private static PrivateFontCollection fColl = new PrivateFontCollection();
		//private static Dictionary<string, string> _fontResourceMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
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
					//foreach (FontFamily family in fColl.Families)
					//{
					//	_fontResourceMap[family.Name] = resourceName;
					//}
					//Console.WriteLine($"Font loaded: {resourceName}");
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error loading font {resourceName}: {ex.Message}");
				}
			}
			fLoad = true;
		}
		public static FontFamily GetFontFamily(string fontName)
		{
			return fColl.Families
				.FirstOrDefault(f => string.Equals(f.Name, fontName, StringComparison.OrdinalIgnoreCase));
		}

		//public static string[] GetLoadedFontNames()
		//{
		//	return fColl.Families.Select(f => f.Name).ToArray();
		//}
		public static string[] GetLoadedFontNames()
		{
			return fColl.Families
				.Select(f => f.Name)  // берём только свойство Name у каждого FontFamily
				.ToArray();           // преобразуем в массив string[]
		}
	}
}
