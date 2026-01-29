namespace Autorunner
{
	partial class Install
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
			this.labelText = new System.Windows.Forms.Label();
			this.btnComplite = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelText
			// 
			this.labelText.AutoSize = true;
			this.labelText.Location = new System.Drawing.Point(126, 60);
			this.labelText.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.labelText.Name = "labelText";
			this.labelText.Size = new System.Drawing.Size(243, 31);
			this.labelText.TabIndex = 0;
			this.labelText.Text = "Установка завершена!";
			this.labelText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnComplite
			// 
			this.btnComplite.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnComplite.Location = new System.Drawing.Point(393, 194);
			this.btnComplite.Name = "btnComplite";
			this.btnComplite.Size = new System.Drawing.Size(116, 34);
			this.btnComplite.TabIndex = 1;
			this.btnComplite.Text = "Завершить";
			this.btnComplite.UseVisualStyleBackColor = true;
			this.btnComplite.Click += new System.EventHandler(this.btnComplite_Click);
			// 
			// Install
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 31F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(521, 240);
			this.Controls.Add(this.btnComplite);
			this.Controls.Add(this.labelText);
			this.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.Name = "Install";
			this.Text = "Install Complete";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelText;
		private System.Windows.Forms.Button btnComplite;
	}
}

