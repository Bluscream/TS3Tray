using System;
using System.Collections.Generic;

namespace Ts3Tray
{
	public class TS3User
	{
		public enum CLIENTSTATUS
		{
			AWAY,
			ISTALKER,
			TALKING,
			INPUTHARDWARE,
			OUTPUTHARDWARE,
			INPUTMUTED,
			OUTPUTMUTED,
			NORMALPLAYER
		}

		private Dictionary<string, string> udata;

		private TS3GroupList sgroups = new TS3GroupList();

		private TS3GroupList cgroups = new TS3GroupList();

		public string this[string key]
		{
			get
			{
				if (this.udata.ContainsKey(key))
				{
					return this.udata[key];
				}
				return null;
			}
		}

		public int ID
		{
			get
			{
				return this.userint("clid");
			}
		}

		public string Name
		{
			get
			{
				return this.userstring("client_nickname");
			}
		}

		public TS3User.CLIENTSTATUS ClientStatus
		{
			get
			{
				string[] array = new string[]
				{
					"client_away",
					"client_is_talker",
					"client_flag_talking",
					"client_input_hardware",
					"client_output_hardware",
					"client_input_muted",
					"client_output_muted"
				};
				int[] array2 = new int[]
				{
					1,
					1,
					1,
					0,
					0,
					1,
					1
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(this[array[i]]) && this[array[i]] == array2[i].ToString())
					{
						return (TS3User.CLIENTSTATUS)i;
					}
				}
				return TS3User.CLIENTSTATUS.NORMALPLAYER;
			}
		}

		public TS3GroupList ServerGroups
		{
			get
			{
				return this.sgroups;
			}
		}

		public TS3GroupList ChannelGroups
		{
			get
			{
				return this.cgroups;
			}
		}

		public bool IsServeradmin
		{
			get
			{
				foreach (TS3Group current in this.sgroups)
				{
					if (current.Name.Equals("Server Admin", StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool IsChanneladmin
		{
			get
			{
				foreach (TS3Group current in this.cgroups)
				{
					if (current.Name.Equals("Channel Admin", StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool IsChannelOperator
		{
			get
			{
				foreach (TS3Group current in this.cgroups)
				{
					if (current.Name.Equals("Operator", StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
				}
				return false;
			}
		}

		internal TS3User(Dictionary<string, string> ulist, TS3GroupList servergroups, TS3GroupList channelgroups)
		{
			this.udata = ulist;
			string text = this["client_servergroups"];
			if (text != null)
			{
				string[] array;
				if (text.IndexOf(',') > 0)
				{
					array = text.Split(new char[]
					{
						','
					});
				}
				else
				{
					array = new string[]
					{
						text
					};
				}
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string b = array2[i];
					foreach (TS3Group current in servergroups)
					{
						if (current.ID.ToString() == b)
						{
							this.sgroups.Add(current);
						}
					}
				}
			}
			text = this["client_channel_group_id"];
			if (text != null)
			{
				string[] array3;
				if (text.IndexOf(',') > 0)
				{
					array3 = text.Split(new char[]
					{
						','
					});
				}
				else
				{
					array3 = new string[]
					{
						text
					};
				}
				string[] array4 = array3;
				for (int j = 0; j < array4.Length; j++)
				{
					string b2 = array4[j];
					foreach (TS3Group current2 in channelgroups)
					{
						if (current2.ID.ToString() == b2)
						{
							this.cgroups.Add(current2);
						}
					}
				}
			}
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

		private string userstring(string key)
		{
			if (!this.udata.ContainsKey(key))
			{
				return "";
			}
			return this.rep(this.udata[key]);
		}

		private int userint(string key)
		{
			if (!this.udata.ContainsKey(key))
			{
				return 0;
			}
			int result = 0;
			if (!int.TryParse(this.udata[key], out result))
			{
				return 0;
			}
			return result;
		}

		private bool userbool(string key)
		{
			return this.userint(key) == 1;
		}
	}
}
