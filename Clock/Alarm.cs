using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clock
{
	public class Alarm : IComparable<Alarm>
	{
		public DateTime Date { get; set; }
		public TimeSpan Time { get; set; }
		public Week Days { get; set; }
		public string Filename { get; set; }
		public Alarm() { }
		public Alarm(string data)
		{
			string[] value = data.Split('\t');
			int j = 0;
			if (DateTime.TryParse(value[j], out DateTime result))
				Date = DateTime.Parse(value[j++]);
			else
			{
				Date = DateTime.MaxValue;
				j++;
			}
			Time = TimeSpan.Parse(value[j++]);
			Days = new Week(Week.ParseDays(value[j++]));
			Filename = value[j++];
		}
		public Alarm(Alarm other)
		{
			this.Date = other.Date;
			this.Time = other.Time;
			this.Days = other.Days;
			this.Filename = other.Filename;
		}
		public int CompareTo(Alarm other)
		{
			return this.Time.CompareTo(other.Time);
		}
		public override string ToString()
		{
			string info = "";
			info += Date != DateTime.MaxValue ? Date.ToString("yyyy.MM.dd") : "Каждый день";
			info += $"\t{DateTime.Today.Add(Time).ToString("HH:mm:ss")}";
			info += $"\t{Days}";
			info += $"\t {Filename.Split('\\').Last()}";
			return info;
		}
		
	}
}
