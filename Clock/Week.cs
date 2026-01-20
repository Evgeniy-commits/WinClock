using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;
namespace Clock
{
	public class Week
	{
		static readonly string[] NAMES = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
		byte days;
		public Week(byte days)
		{ 
			this.days = days; 
		}
		public void Extract(System.Windows.Forms.CheckedListBox clb)
		{
			if (clb.Items.Count != 7) return;
			for (byte i = 0; i < 7; i++)
			{
				clb.SetItemChecked(i, Convert.ToBoolean((1 << i) & days));
			}
		}
		public bool Contains(byte day)
		{
			if (day == 0) day = 7;
			day--;
			return (days & (1 << day)) != 0;
		}
		public override string ToString()
		{
			string days = "";
			for (byte i = 0; i < 7; i++)
			{
				byte day = (byte)(1 << i);
				if ((this.days & day) != 0) days += $"{NAMES[i]},";
			}
  			return days;
		}
		public static byte ParseDays(string Days)
		{
			string[] days = Days.Split(',');
			byte daymask = 0;
			foreach (string day in days)
			{
				string editstr = day.Trim();
				for (int i = 0; i < NAMES.Length; i++)
				{
					if (editstr == NAMES[i])
					{
						daymask |= (byte)(1 << i);
						break;
					}
				}
			}
			return daymask;
		}
	}
}
