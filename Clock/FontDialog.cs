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
using CustomFonts;

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
			LoadFonts("*.ttf");
			LoadFonts("*.otf");
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

		void LoadFonts(string extension)
		{
			//string currentDir = Application.ExecutablePath;
			//Directory.SetCurrentDirectory($"{currentDir}\\..\\..\\..\\Fonts");
			Fonts.LoadFonts(extension);

			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), extension);
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
			SetFont();
		}

		void SetFont()
		{
			Directory.SetCurrentDirectory($"{Application.ExecutablePath}\\..\\..\\..\\Fonts");
			PrivateFontCollection pfc = new PrivateFontCollection();
			pfc.AddFontFile(comboBoxFont.SelectedItem.ToString());
			labelExample.Font = new Font(pfc.Families[0], (float)numericUpDownFontSize.Value);
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
