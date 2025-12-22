using System.Windows.Forms;

namespace ClockApplication
{
	partial class commonWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl));


			// 
			// panelDisplay
			//
			this.panelDisplay = new System.Windows.Forms.Panel();
			this.panelDisplay.BackColor = System.Drawing.Color.FromArgb(240, 240, 255);
			this.panelDisplay.Location = new System.Drawing.Point(12, 12);
			this.panelDisplay.Name = "panelDisplay";
			this.panelDisplay.Size = new System.Drawing.Size(460, 460);
			this.panelDisplay.TabIndex = 0;

			//
			// contextMenuClock
			//
			this.contextMenuClock = new System.Windows.Forms.ContextMenuStrip();
			this.toolStripAnalog = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripDigital = new System.Windows.Forms.ToolStripMenuItem();

			// contextMenuClock
			this.contextMenuClock.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
				this.toolStripAnalog,
				this.toolStripDigital});
			this.contextMenuClock.Name = "contextMenuClock";
			this.contextMenuClock.Size = new System.Drawing.Size(167, 70);

			// toolStripAnalog
			this.toolStripAnalog.Name = "toolStripAnalog";
			this.toolStripAnalog.Size = new System.Drawing.Size(166, 22);
			this.toolStripAnalog.Text = "Аналоговые часы";
			// toolStripDigital
			this.toolStripDigital.Name = "toolStripDigital";
			this.toolStripDigital.Size = new System.Drawing.Size(166, 22);
			this.toolStripDigital.Text = "Цифровые часы";

			// MainForm
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 484);
			this.Controls.Add(this.panelDisplay);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Часы";
			this.ContextMenuStrip = this.contextMenuClock;
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.Panel panelDisplay;
		private System.Windows.Forms.ContextMenuStrip contextMenuClock;
		private System.Windows.Forms.ToolStripMenuItem toolStripAnalog;
		private System.Windows.Forms.ToolStripMenuItem toolStripDigital;
	}
}

