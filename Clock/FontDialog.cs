using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontLibrary;

namespace Clock
{
	public partial class FontDialog : Form
	{
		public Font Font {  get; set; }
		public string Filename { get; set; }
		int lastChosenIndex;
		public FontDialog()
		{
			InitializeComponent();
			lastChosenIndex = 0;
			LoadFonts(".ttf");
			//LoadFonts(".otf");
			comboBoxFont.SelectedIndex = 1;
		}

		public FontDialog(string font_name, string font_size) : this() 
		{
			Filename = font_name;
			if (font_size != null) numericUpDownFontSize.Value = Convert.ToDecimal(font_size);
			lastChosenIndex = comboBoxFont.FindString(font_name);
			if (lastChosenIndex == -1) lastChosenIndex = 2; 
			comboBoxFont.SelectedIndex = lastChosenIndex;
			SetFont();
			Font = labelExample.Font;
		}

		private void FontDialog_Load(object sender, EventArgs e)
		{
			numericUpDownFontSize.Value = (decimal)Font.Size;
		}

		//void LoadFonts(string extension)
		//{
		//	string currentDir = Application.ExecutablePath;
		//	Directory.SetCurrentDirectory($"{currentDir}\\..\\..\\..\\Fonts");
		//	string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), extension);
		//	//comboBoxFont.Items.AddRange(files); //Добавляем все содержимое массива "files" в выпадающий список
		//	for (int i = 0; i < files.Length; i++)
		//	{
		//		comboBoxFont.Items.Add(files[i].Split('\\').Last());
		//	}
		//}
		void LoadFonts(string extension)
		{
			FontLoader.LoadFonts(extension);
			//MessageBox.Show($"Загружено шрифтов: {FontLoader.GetLoadedFontNames().Length}");
			//MessageBox.Show("Доступные шрифты:");
			//foreach (var name in FontLoader.GetLoadedFontNames())
			//{
			//	MessageBox.Show(name);
			//}
			string[] files = FontLoader.GetLoadedFontNames();
			comboBoxFont.Items.Clear();
			foreach (string fontName in files)
			{
				comboBoxFont.Items.Add(fontName);
			}
		}

		private void comboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			string info = $"Selected:\nIndex:\t{comboBoxFont.SelectedIndex.ToString()}";
			info += $"\nItem:\t{comboBoxFont.SelectedItem}";
			info += $"\nText:\t{comboBoxFont.SelectedText}";
			info += $"\nValue:\t{comboBoxFont.SelectedValue}";
			SetFont();
		}

		//void SetFont()
		//{
		//	Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..\\Fonts");
		//	PrivateFontCollection pfc = new PrivateFontCollection();
		//	pfc.AddFontFile(comboBoxFont.SelectedItem.ToString());
		//	labelExample.Font = new Font(pfc.Families[0], (float)numericUpDownFontSize.Value);
		//}
		void SetFont()
		{
			//Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..\\Fonts");
			//PrivateFontCollection pfc = FontLoader.fColl;
			//pfc.AddFontFile(comboBoxFont.SelectedItem.ToString());
			if (comboBoxFont.SelectedItem == null)
			{
				MessageBox.Show("Выберите шрифт из списка!");
				return;
			}

			string selName = comboBoxFont.SelectedItem.ToString();
			//MessageBox.Show($"Имя шрифта в Box: {selName}");
			FontFamily family = FontLoader.GetFontFamily(selName);
			//MessageBox.Show($"Имя шрифта: {family.Name}");
			//if (family == null)
			//{
			//	MessageBox.Show($"Шрифт '{selName}' не найден!");
			//	return;
			//}



			try
			{
				var testFont = new Font(family, (float)numericUpDownFontSize.Value);
				labelExample.Font = testFont;
				MessageBox.Show($"Шрифт применён: {testFont.Name}, {testFont.Size}pt");
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка создания шрифта: {ex.Message}");
			}
			if (!family.IsStyleAvailable(FontStyle.Regular))
			{
				MessageBox.Show("Шрифт не поддерживает начертание Regular!");
				return;
			}
			labelExample.Font = new Font(family, (float)numericUpDownFontSize.Value);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this.Font = labelExample.Font;
			this.Filename = comboBoxFont.SelectedItem.ToString();
			this.lastChosenIndex = comboBoxFont.SelectedIndex;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			labelExample.Font = this.Font;
			comboBoxFont.SelectedIndex = lastChosenIndex;
		}

		private void numericUpDownFontSize_ValueChanged(object sender, EventArgs e)
		{
			SetFont();
		}
	}
}
