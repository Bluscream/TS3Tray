using System;
using System.Collections.Generic;

namespace Ts3Tray
{
	public class TS3Group
	{
		private Dictionary<string, string> gdata;

		public int ID
		{
			get
			{
				if (!this.gdata.ContainsKey("sgid"))
				{
					return this.groupint("cgid");
				}
				return this.groupint("sgid");
			}
		}

		public string Name
		{
			get
			{
				return this.groupstring("name");
			}
		}

		public int Type
		{
			get
			{
				return this.groupint("iconid");
			}
		}

		public int SaveDB
		{
			get
			{
				return this.groupint("savedb");
			}
		}

		public int SortID
		{
			get
			{
				return this.groupint("sortid");
			}
		}

		public int Namemode
		{
			get
			{
				return this.groupint("namemode");
			}
		}

		public string this[string key]
		{
			get
			{
				if (this.gdata.ContainsKey(key))
				{
					return this.gdata[key];
				}
				return null;
			}
		}

		internal TS3Group(Dictionary<string, string> dat)
		{
			this.gdata = dat;
		}

		private string rep(string var)
		{
			var = var.Replace("\\/", "/");
			var = var.Replace("\\s", " ");
			var = var.Replace("\\p", "|");
			var = var.Replace("[URL]", "");
			var = var.Replace("[/URL]", "");
			var = var.Replace("[b]", "");
			var = var.Replace("[/b]", "");
			return var;
		}

		private string groupstring(string key)
		{
			if (!this.gdata.ContainsKey(key))
			{
				return "";
			}
			return this.rep(this.gdata[key]);
		}

		private int groupint(string key)
		{
			if (!this.gdata.ContainsKey(key))
			{
				return 0;
			}
			int result = 0;
			if (!int.TryParse(this.gdata[key], out result))
			{
				return 0;
			}
			return result;
		}

		private bool groupbool(string key)
		{
			return this.groupint(key) == 1;
		}
	}
}
