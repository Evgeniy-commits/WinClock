using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock
{
	internal class Alarm
	{
		public DateTime Date { get; set; }
		public DateTime Time { get; set; }
		public byte Weekdays { get; set; }
		public string Filename { get; set; }

	}
}
