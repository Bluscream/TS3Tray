using System;
using System.Collections;
using System.Collections.Generic;

namespace Ts3Tray
{
	public class TS3Channel : IEnumerable<TS3Channel>, IEnumerable
	{
		private Dictionary<string, string> cdata;

		private TS3ChannelList subchannels = new TS3ChannelList();

		private TS3UserList user = new TS3UserList();

		public string this[string key]
		{
			get
			{
				if (this.cdata.ContainsKey(key))
				{
					return this.cdata[key];
				}
				return null;
			}
		}

		public int ParentID
		{
			get
			{
				return this.chanint("pid");
			}
		}

		public int ID
		{
			get
			{
				return this.chanint("cid");
			}
		}

		public string Title
		{
			get
			{
				return this.chanstring("channel_name");
			}
		}

		public string Topic
		{
			get
			{
				return this.chanstring("channel_topic");
			}
		}

		public bool IsDefault
		{
			get
			{
				return this.chanbool("channel_flag_default");
			}
		}

		public bool HasPassword
		{
			get
			{
				return this.chanbool("channel_flag_password");
			}
		}

		public int Codec
		{
			get
			{
				return this.chanint("channel_codec");
			}
		}

		public bool HasSubchannels
		{
			get
			{
				return this.subchannels.Count > 0;
			}
		}

		public bool HasUser
		{
			get
			{
				return this.user.Count > 0;
			}
		}

		public TS3ChannelList SubchannelList
		{
			get
			{
				return this.subchannels;
			}
		}

		public TS3UserList UserList
		{
			get
			{
				return this.user;
			}
		}

		internal TS3Channel(Dictionary<string, string> clist, List<Dictionary<string, string>> allchan, List<Dictionary<string, string>> alluser, TS3GroupList servergroups, TS3GroupList channelgroups)
		{
			this.cdata = clist;
			foreach (Dictionary<string, string> current in allchan)
			{
				int num = -1;
				if (current.ContainsKey("pid") && !int.TryParse(current["pid"], out num))
				{
					num = -1;
				}
				if (num == this.ID)
				{
					this.subchannels.Add(new TS3Channel(current, allchan, alluser, servergroups, channelgroups));
				}
			}
			foreach (Dictionary<string, string> current2 in alluser)
			{
				int num2 = -1;
				if (current2.ContainsKey("cid") && !int.TryParse(current2["cid"], out num2))
				{
					num2 = -1;
				}
				if (num2 == this.ID)
				{
					int num3 = -1;
					if (current2.ContainsKey("client_type") && !int.TryParse(current2["client_type"], out num3))
					{
						num3 = -1;
					}
					if (num3 != 1)
					{
						this.user.Add(new TS3User(current2, servergroups, channelgroups));
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

		private string chanstring(string key)
		{
			if (!this.cdata.ContainsKey(key))
			{
				return "";
			}
			return this.rep(this.cdata[key]);
		}

		private int chanint(string key)
		{
			if (!this.cdata.ContainsKey(key))
			{
				return 0;
			}
			int result = 0;
			if (!int.TryParse(this.cdata[key], out result))
			{
				return 0;
			}
			return result;
		}

		private bool chanbool(string key)
		{
			return this.chanint(key) == 1;
		}

		public IEnumerator<TS3Channel> GetEnumerator()
		{
			return this.subchannels.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.subchannels.GetEnumerator();
		}
	}
}
