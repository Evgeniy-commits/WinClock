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
using System.Drawing.Text;

namespace Clock
{
	public partial class FontDialog : Form
	{
		public Font Font {  get; set; }
		int lastChosenIndex;
		public FontDialog()
		{
			InitializeComponent();
			lastChosenIndex = 0;
			LoadFonts("*.ttf");
			LoadFonts("*.otf");
		}

		private void FontDialog_Load(object sender, EventArgs e)
		{
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

		private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			string info = $"Selected:\nIndex:\t{comboBoxFont.SelectedIndex.ToString()}";
			info += $"\nItem:\t{comboBoxFont.SelectedItem}";
			info += $"\nText:\t{comboBoxFont.SelectedText}";
			info += $"\nValue:\t{comboBoxFont.SelectedValue}";
			//MessageBox.Show
			//	(
			//		this,
			//		info,
			//		"SelectedIndexChanged",
			//		MessageBoxButtons.OK,
			//		MessageBoxIcon.Information
			//	);
			SetFont(comboBoxFont.SelectedItem.ToString());
		}

		void SetFont(string filename)
		{
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile(filename);
			labelExample.Font = new Font(pfc.Families[0], 32);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Font = labelExample.Font;
			this.lastChosenIndex = comboBoxFont.SelectedIndex;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			labelExample.Font = this.Font;
			comboBoxFont.SelectedIndex = lastChosenIndex;
		}
	}
}
