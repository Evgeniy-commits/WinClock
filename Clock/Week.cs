using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public bool[] GetStates()
		{
			bool[] states = new bool[Count];
			for (int i = 0; i < Count; i++)
			{
				states[i] = (days & (1 << i)) != 0;
			}
			return states;
		}

		public string GetDayName(int index)
		{
			return NAMES[index];
		}
		public int Count => NAMES.Length;
	}
}
