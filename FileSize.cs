using System;

namespace Ts3Tray
{
	public class FileSize
	{
		public const decimal OneByte = 1m;

		public const decimal OneKiloByte = 1024m;

		public const decimal OneMegaByte = 1048576m;

		public const decimal OneGigaByte = 1073741824m;

		public const decimal OneTerraByte = new decimal(1099511627776L);

		private uint fileSize;

		internal uint Integer
		{
			get
			{
				return this.fileSize;
			}
		}

		internal FileSize(uint fs)
		{
			this.fileSize = fs;
		}

		public override string ToString()
		{
			decimal num = 0m;
			try
			{
				num = Convert.ToDecimal(this.fileSize);
			}
			catch (InvalidCastException)
			{
				return "0 Bytes";
			}
			string arg;
			if (num >= new decimal(1099511627776L))
			{
				num /= new decimal(1099511627776L);
				arg = " TB";
			}
			else if (num >= 1073741824m)
			{
				num /= 1073741824m;
				arg = " GB";
			}
			else if (num >= 1048576m)
			{
				num /= 1048576m;
				arg = " MB";
			}
			else if (num >= 1024m)
			{
				num /= 1024m;
				arg = " kB";
			}
			else
			{
				arg = " Bytes";
			}
			return string.Format("{0:N2}{1}", num, arg);
		}

		public static implicit operator string(FileSize fs)
		{
			return fs.ToString();
		}

		public static implicit operator uint(FileSize fs)
		{
			return fs.Integer;
		}
	}
}
