using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Clock
{
	public partial class FontDialog : Form
	{
		public FontDialog()
		{
			InitializeComponent();
		}

		private void FontDialog_Load(object sender, EventArgs e)
		{
			LoadFonts("*.ttf");
			LoadFonts("*.otf");
		}

		void LoadFonts(string extension)
		{
			string currentDir = Application.ExecutablePath;
			Directory.SetCurrentDirectory($"{currentDir}\\..\\..\\..\\Fonts");
			//MessageBox.Show
			//	(
			//		this,
			//		//currentDir,
			//		Directory.GetCurrentDirectory(),
			//		"CurrentDirectory",
			//		MessageBoxButtons.OK,
			//		MessageBoxIcon.Information
			//	);

			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), extension);
			//comboBoxFont.Items.AddRange(files); //Добавляем все содержимое массива "files" в выпадающий список
			for (int i = 0; i < files.Length; i++)
			{
				comboBoxFont.Items.Add( files[i].Split('\\').Last());
			}
		}
	}
}
