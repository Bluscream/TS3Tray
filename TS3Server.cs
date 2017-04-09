using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Ts3Tray.Properties;

namespace Ts3Tray
{
	public class TS3Server
	{
		internal class Ts3Socket
		{
			private TcpClient tcp;

			public Ts3Socket(string host, int port)
			{
				this.tcp = new TcpClient(host, port);
				byte[] array = new byte[8096];
				this.tcp.GetStream().Read(array, 0, array.Length);
			}

			~Ts3Socket()
			{
				if (this.tcp != null)
				{
					if (this.tcp.Connected)
					{
						this.sendCmd("quit\n");
					}
				}
			}

			public string sendCmd()
			{
				return this.sendCmd(null);
			}

			public string sendCmd(string cmd)
			{
				string text = "";
				try
				{
					if (this.tcp == null)
					{
						string result = null;
						return result;
					}
					if (!this.tcp.Connected)
					{
						string result = null;
						return result;
					}
					NetworkStream stream = this.tcp.GetStream();
					byte[] array;
					if (!string.IsNullOrEmpty(cmd))
					{
						array = Encoding.UTF8.GetBytes(cmd);
						stream.Write(array, 0, array.Length);
					}
					array = new byte[8096];
					while (text.IndexOf("msg=") < 1)
					{
						int num = stream.Read(array, 0, array.Length);
						if (num > 0)
						{
							text += Encoding.UTF8.GetString(array, 0, num);
						}
					}
					if (text.IndexOf("msg=ok") > 0)
					{
						string result = text;
						return result;
					}
				}
				catch
				{
				}
				return null;
			}

			public Dictionary<string, string> splitInfo(string info)
			{
				string text = info.Replace("error id=0 msg=ok", "").Trim();
				string[] array = text.Split(new char[]
				{
					' '
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					if (text2.IndexOf('=') < 0)
					{
						dictionary.Add(text2, "");
					}
					else
					{
						dictionary.Add(text2.Substring(0, text2.IndexOf('=')).Trim(), text2.Substring(text2.IndexOf('=') + 1).Trim());
					}
				}
				return dictionary;
			}

			public string[] splitInfo2(string info)
			{
				string text = info.Replace("error id=0 msg=ok", "").Trim();
				return text.Split(new char[]
				{
					'|'
				});
			}
		}

		public string _TS3Address = "localhost";

		public int _TS3QueryPort;

		public int _TS3ServerNr;

		public string _TS3QueryUser = "";

		public string _TS3QueryPasswort = "";

		private Dictionary<string, string> serverinfo = new Dictionary<string, string>();

		private List<Dictionary<string, string>> channels = new List<Dictionary<string, string>>();

		private List<Dictionary<string, string>> clients = new List<Dictionary<string, string>>();

		private TS3GroupList sgroups = new TS3GroupList();

		private TS3GroupList cgroups = new TS3GroupList();

		public string ServerAddress
		{
			get
			{
				return this._TS3Address;
			}
			set
			{
				this._TS3Address = value;
			}
		}

		public int ConnectionPort
		{
			get
			{
				return this.vservint("virtualserver_port");
			}
		}

		public int QueryPort
		{
			get
			{
				return this._TS3QueryPort;
			}
			set
			{
				this._TS3QueryPort = value;
			}
		}

		public string QueryUser
		{
			get
			{
				return this._TS3QueryUser;
			}
			set
			{
				this._TS3QueryUser = value;
			}
		}

		public string QueryPasswort
		{
			get
			{
				return this._TS3QueryPasswort;
			}
			set
			{
				this._TS3QueryPasswort = value;
			}
		}

		public int ServerNr
		{
			get
			{
				return this._TS3ServerNr;
			}
			set
			{
				this._TS3ServerNr = value;
			}
		}

		public string Title
		{
			get
			{
				return this.vservstring("virtualserver_name");
			}
		}

		public int UserOnline
		{
			get
			{
				if (!this.serverinfo.ContainsKey("virtualserver_clientsonline"))
				{
					return 0;
				}
				int num = int.Parse(this.serverinfo["virtualserver_clientsonline"]);
				int num2 = this.serverinfo.ContainsKey("virtualserver_queryclientsonline") ? int.Parse(this.serverinfo["virtualserver_queryclientsonline"]) : 0;
				return num - num2;
			}
		}

		public int MaximalClients
		{
			get
			{
				return this.vservint("virtualserver_maxclients");
			}
		}

		public string Website
		{
			get
			{
				return this.vservstring("virtualserver_hostbutton_tooltip");
			}
		}

		public string WebsiteUrl
		{
			get
			{
				return this.vservstring("virtualserver_hostbutton_url");
			}
		}

		public string BannerUrl
		{
			get
			{
				return this.vservstring("virtualserver_hostbanner_gfx_url");
			}
		}

		public string WelcomeMessage
		{
			get
			{
				return this.vservstring("virtualserver_welcomemessage");
			}
		}

		public TS3ChannelList ChannelList
		{
			get
			{
				TS3ChannelList tS3ChannelList = new TS3ChannelList();
				foreach (Dictionary<string, string> current in this.channels)
				{
					int num = -1;
					if (current.ContainsKey("pid") && !int.TryParse(current["pid"], out num))
					{
						num = -1;
					}
					if (num == 0)
					{
						tS3ChannelList.Add(new TS3Channel(current, this.channels, this.clients, this.sgroups, this.cgroups));
					}
				}
				return tS3ChannelList;
			}
		}

		public DateTime ServerCreated
		{
			get
			{
				DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
				return dateTime.AddSeconds(this.vservdouble("virtualserver_created")).ToLocalTime();
			}
		}

		public string OS
		{
			get
			{
				return this.vservstring("virtualserver_platform");
			}
		}

		public string Version
		{
			get
			{
				return this.vservstring("virtualserver_version");
			}
		}

		public int ChannelsOnline
		{
			get
			{
				return this.vservint("virtualserver_channelsonline");
			}
		}

		public TimeSpan Uptime
		{
			get
			{
				return new TimeSpan(0, 0, this.vservint("virtualserver_uptime"));
			}
		}

		public FileSize BytesSentTotal
		{
			get
			{
				return new FileSize(this.vservuint("connection_bytes_sent_total"));
			}
		}

		public FileSize BytesReceivedTotal
		{
			get
			{
				return new FileSize(this.vservuint("connection_bytes_received_total"));
			}
		}

		public bool ServerpwRequired
		{
			get
			{
				return this.vservint("virtualserver_flag_password") == 1;
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

		public TS3Server()
		{
		}

		public TS3Server(string host, int query, int srvnr)
		{
			this._TS3Address = host;
			this._TS3QueryPort = query;
			this._TS3ServerNr = srvnr;
			this._TS3QueryUser = "";
			this._TS3QueryPasswort = "";
			this.Refreh();
		}

		public bool Refreh()
		{
			if (string.IsNullOrEmpty(this._TS3Address))
			{
				return false;
			}
			if (this._TS3QueryPort < 100 || this._TS3QueryPort > 65000)
			{
				return false;
			}
			if (this._TS3ServerNr < 1 || this._TS3QueryPort > 65000)
			{
				return false;
			}
			try
			{
				TS3Server.Ts3Socket ts3Socket = new TS3Server.Ts3Socket(this._TS3Address, this._TS3QueryPort);
				ts3Socket.sendCmd("hello\n");
				if (!string.IsNullOrEmpty(this._TS3QueryUser) && !string.IsNullOrEmpty(this._TS3QueryPasswort) && ts3Socket.sendCmd(string.Concat(new string[]
				{
					"login ",
					this._TS3QueryUser,
					" ",
					this._TS3QueryPasswort,
					"\n"
				})) == null)
				{
					System.Windows.Forms.MessageBox.Show(Resources.MSG_QUERYLOGINFAILED, Resources.MSG_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					bool result = false;
					return result;
				}
				if (string.IsNullOrEmpty(ts3Socket.sendCmd("use sid=" + this._TS3ServerNr.ToString() + "\n")))
				{
					bool result = false;
					return result;
				}
				string text = ts3Socket.sendCmd("serverinfo\n");
				if (string.IsNullOrEmpty(text))
				{
					bool result = false;
					return result;
				}
				this.serverinfo = ts3Socket.splitInfo(text);
				text = ts3Socket.sendCmd("servergrouplist\n");
				if (string.IsNullOrEmpty(text))
				{
					bool result = false;
					return result;
				}
				string[] array = ts3Socket.splitInfo2(text);
				this.sgroups.Clear();
				if (array != null)
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string info = array2[i];
						Dictionary<string, string> dat = ts3Socket.splitInfo(info);
						TS3Group tS3Group = new TS3Group(dat);
						if (tS3Group.ID > 0)
						{
							this.sgroups.Add(tS3Group);
						}
					}
				}
				text = ts3Socket.sendCmd("channelgrouplist\n");
				if (string.IsNullOrEmpty(text))
				{
					bool result = false;
					return result;
				}
				string[] array3 = ts3Socket.splitInfo2(text);
				this.cgroups.Clear();
				if (array3 != null)
				{
					string[] array4 = array3;
					for (int j = 0; j < array4.Length; j++)
					{
						string info2 = array4[j];
						Dictionary<string, string> dat2 = ts3Socket.splitInfo(info2);
						TS3Group tS3Group2 = new TS3Group(dat2);
						if (tS3Group2.ID > 0)
						{
							this.cgroups.Add(tS3Group2);
						}
					}
				}
				text = ts3Socket.sendCmd("channellist -topic -flags -voice -limits\n");
				if (string.IsNullOrEmpty(text))
				{
					bool result = false;
					return result;
				}
				string[] array5 = ts3Socket.splitInfo2(text);
				this.channels.Clear();
				if (array5 != null)
				{
					string[] array6 = array5;
					for (int k = 0; k < array6.Length; k++)
					{
						string info3 = array6[k];
						this.channels.Add(ts3Socket.splitInfo(info3));
					}
				}
				text = ts3Socket.sendCmd("clientlist -uid -away -voice -groups\n");
				if (string.IsNullOrEmpty(text))
				{
					bool result = false;
					return result;
				}
				string[] array7 = ts3Socket.splitInfo2(text);
				this.clients.Clear();
				if (array7 != null)
				{
					string[] array8 = array7;
					for (int l = 0; l < array8.Length; l++)
					{
						string info4 = array8[l];
						this.clients.Add(ts3Socket.splitInfo(info4));
					}
				}
			}
			catch
			{
				bool result = false;
				return result;
			}
			return true;
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

		private string vservstring(string key)
		{
			if (!this.serverinfo.ContainsKey(key))
			{
				return "";
			}
			return this.rep(this.serverinfo[key]);
		}

		private int vservint(string key)
		{
			if (!this.serverinfo.ContainsKey(key))
			{
				return 0;
			}
			int result = 0;
			if (!int.TryParse(this.serverinfo[key], out result))
			{
				return 0;
			}
			return result;
		}

		private uint vservuint(string key)
		{
			if (!this.serverinfo.ContainsKey(key))
			{
				return 0u;
			}
			uint result = 0u;
			if (!uint.TryParse(this.serverinfo[key], out result))
			{
				return 0u;
			}
			return result;
		}

		private double vservdouble(string key)
		{
			if (!this.serverinfo.ContainsKey(key))
			{
				return 0.0;
			}
			double result = 0.0;
			if (!double.TryParse(this.serverinfo[key], out result))
			{
				return 0.0;
			}
			return result;
		}
	}
}
