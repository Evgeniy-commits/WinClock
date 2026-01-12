namespace Clock
{
	partial class AlarmsDialog
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
			this.listBoxAlarms = new System.Windows.Forms.ListBox();
			this.textBoxAlarmsName = new System.Windows.Forms.TextBox();
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.buttonAddAlarm = new System.Windows.Forms.Button();
			this.buttonRemoveAlarm = new System.Windows.Forms.Button();
			this.timerClock = new System.Windows.Forms.Timer(this.components);
			this.timerCheck = new System.Windows.Forms.Timer(this.components);
			this.buttonOk = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelCurrentTime = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listBoxAlarms
			// 
			this.listBoxAlarms.FormattingEnabled = true;
			this.listBoxAlarms.Location = new System.Drawing.Point(12, 12);
			this.listBoxAlarms.Name = "listBoxAlarms";
			this.listBoxAlarms.Size = new System.Drawing.Size(459, 95);
			this.listBoxAlarms.TabIndex = 0;
			// 
			// textBoxAlarmsName
			// 
			this.textBoxAlarmsName.Location = new System.Drawing.Point(12, 113);
			this.textBoxAlarmsName.Name = "textBoxAlarmsName";
			this.textBoxAlarmsName.Size = new System.Drawing.Size(100, 20);
			this.textBoxAlarmsName.TabIndex = 1;
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker.Location = new System.Drawing.Point(12, 139);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.ShowUpDown = true;
			this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.dateTimePicker.TabIndex = 2;
			// 
			// buttonAddAlarm
			// 
			this.buttonAddAlarm.Location = new System.Drawing.Point(12, 199);
			this.buttonAddAlarm.Name = "buttonAddAlarm";
			this.buttonAddAlarm.Size = new System.Drawing.Size(75, 23);
			this.buttonAddAlarm.TabIndex = 3;
			this.buttonAddAlarm.Text = "Add";
			this.buttonAddAlarm.UseVisualStyleBackColor = true;
			this.buttonAddAlarm.Click += new System.EventHandler(this.buttonAddAlarm_Click);
			// 
			// buttonRemoveAlarm
			// 
			this.buttonRemoveAlarm.Location = new System.Drawing.Point(93, 199);
			this.buttonRemoveAlarm.Name = "buttonRemoveAlarm";
			this.buttonRemoveAlarm.Size = new System.Drawing.Size(75, 23);
			this.buttonRemoveAlarm.TabIndex = 4;
			this.buttonRemoveAlarm.Text = "Remove";
			this.buttonRemoveAlarm.UseVisualStyleBackColor = true;
			this.buttonRemoveAlarm.Click += new System.EventHandler(this.buttonRemoveAlarm_Click);
			// 
			// timerClock
			// 
			this.timerClock.Interval = 1000;
			this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
			// 
			// timerCheck
			// 
			this.timerCheck.Interval = 1000;
			this.timerCheck.Tick += new System.EventHandler(this.timerCheck_Tick);
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(317, 270);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 5;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(398, 270);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// labelCurrentTime
			// 
			this.labelCurrentTime.AutoSize = true;
			this.labelCurrentTime.Location = new System.Drawing.Point(12, 166);
			this.labelCurrentTime.Name = "labelCurrentTime";
			this.labelCurrentTime.Size = new System.Drawing.Size(86, 13);
			this.labelCurrentTime.TabIndex = 7;
			this.labelCurrentTime.Text = "labelCurrentTime";
			// 
			// AlarmsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(485, 305);
			this.Controls.Add(this.labelCurrentTime);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.buttonRemoveAlarm);
			this.Controls.Add(this.buttonAddAlarm);
			this.Controls.Add(this.dateTimePicker);
			this.Controls.Add(this.textBoxAlarmsName);
			this.Controls.Add(this.listBoxAlarms);
			this.Name = "AlarmsDialog";
			this.Text = "Alarms Dialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBoxAlarms;
		private System.Windows.Forms.TextBox textBoxAlarmsName;
		private System.Windows.Forms.DateTimePicker dateTimePicker;
		private System.Windows.Forms.Button buttonAddAlarm;
		private System.Windows.Forms.Button buttonRemoveAlarm;
		private System.Windows.Forms.Timer timerClock;
		private System.Windows.Forms.Timer timerCheck;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelCurrentTime;
	}
}