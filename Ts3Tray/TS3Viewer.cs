using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class TS3Viewer : UserControl, IComponentConnector
	{
		private IList<TreeChannel> treeChannelList = new List<TreeChannel>();

		private string _TS3Address = "localhost";

		private int _TS3QueryPort;

		private string _TS3QueryUser = "";

		private string _TS3QueryPasswort = "";

		private int _TS3ServerNr;

		private byte _TS3UpdateIntervall = 10;

		private TS3Server serv;

		private DispatcherTimer dispatcherTimer = new DispatcherTimer();

		private List<string> user = new List<string>();

		private bool _DisplayServerInfo = true;

		internal RowDefinition sinfoRow;

		internal Label serverLabel;

		internal TreeView treeView1;

		internal Expander expander1;

		internal Label svrOs;

		internal Label svrVersion;

		internal Label svrUptime;

		internal Label svrChanCnt;

		internal Label svrUsrCnt;

		internal Label svrCltCnt;

		private bool _contentLoaded;

		public event NicknameRequiredHandler NicknameRequired;

		public event ServerpasswordRequiredHandler ServerpasswordRequired;

		public event ChannelpasswordRequiredHandler ChannelpasswordRequired;

		public event UserJoinedHandler UserJoined;

		public event UserLeaveHandler UserLeave;

		public event ErrorHandler Error;

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
				if (this.serv != null)
				{
					return this.serv.ConnectionPort;
				}
				return 9987;
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

		public byte UpdateIntervall
		{
			get
			{
				return this._TS3UpdateIntervall;
			}
			set
			{
				if (value == 0)
				{
					return;
				}
				this._TS3UpdateIntervall = value;
				this.dispatcherTimer.Stop();
				this.dispatcherTimer.Interval = new TimeSpan(0, (int)this._TS3UpdateIntervall, 0);
				this.dispatcherTimer.Start();
			}
		}

		public bool DisplayServerInfo
		{
			get
			{
				return this._DisplayServerInfo;
			}
			set
			{
				this._DisplayServerInfo = value;
				this.OnExpandChanged(this.expander1, null);
			}
		}

		public bool IsExpanded
		{
			get
			{
				return this.expander1.IsExpanded;
			}
			set
			{
				this.expander1.IsExpanded = value;
			}
		}

		public TS3Viewer()
		{
			this.InitializeComponent();
			this.dispatcherTimer.Tick += new EventHandler(this.dispatcherTimer_Tick);
			this.dispatcherTimer.Interval = new TimeSpan(0, (int)this._TS3UpdateIntervall, 0);
			this.dispatcherTimer.Start();
			this.OnExpandChanged(this.expander1, null);
			this.Refresh();
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e)
		{
			this.Refresh();
		}

		private List<string> parseuser(List<string> map, TS3Channel chan)
		{
			if (chan.HasUser)
			{
				foreach (TS3User current in chan.UserList)
				{
					if (!map.Contains(current.Name))
					{
						map.Add(current.Name);
					}
				}
			}
			if (chan.HasSubchannels)
			{
				foreach (TS3Channel current2 in chan.SubchannelList)
				{
					map = this.parseuser(map, current2);
				}
			}
			return map;
		}

		public bool Refresh()
		{
			this.serv = new TS3Server();
			this.serv.ServerAddress = this._TS3Address;
			this.serv.QueryPort = this._TS3QueryPort;
			this.serv.QueryUser = this._TS3QueryUser;
			this.serv.QueryPasswort = this._TS3QueryPasswort;
			this.serv.ServerNr = this._TS3ServerNr;
			this.treeView1.ItemsSource = null;
			this.treeChannelList.Clear();
			if (!this.serv.Refreh())
			{
				if (this.Error != null && !string.IsNullOrEmpty(this._TS3Address) && !string.IsNullOrEmpty(this._TS3QueryUser) && !string.IsNullOrEmpty(this._TS3QueryPasswort))
				{
					this.Error(new Exception("Keine Verbindung!"));
				}
				this.expander1.IsExpanded = false;
				this.expander1.IsEnabled = false;
				return false;
			}
			this.expander1.IsEnabled = true;
			List<string> list = new List<string>();
			this.serverLabel.Content = string.Concat(new string[]
			{
				this.serv.Title,
				" (",
				this.serv.UserOnline.ToString(),
				"/",
				this.serv.MaximalClients.ToString(),
				")"
			});
			foreach (TS3Channel current in this.serv.ChannelList)
			{
				list = this.parseuser(list, current);
				this.treeChannelList.Add(new TreeChannel(current, null));
			}
			if (this.UserJoined != null)
			{
				List<string> list2 = new List<string>();
				foreach (string current2 in list)
				{
					if (!this.user.Contains(current2))
					{
						list2.Add(current2);
					}
				}
				if (list2.Count > 0)
				{
					this.UserJoined(list2.ToArray());
				}
			}
			if (this.UserLeave != null)
			{
				List<string> list3 = new List<string>();
				foreach (string current3 in this.user)
				{
					if (!list.Contains(current3))
					{
						list3.Add(current3);
					}
				}
				if (list3.Count > 0)
				{
					this.UserLeave(list3.ToArray());
				}
			}
			this.user = list;
			this.treeView1.ItemsSource = this.treeChannelList;
			this.svrOs.Content = this.serv.OS;
			this.svrVersion.Content = this.serv.Version;
			this.svrUptime.Content = this.serv.Uptime.ToString();
			this.svrChanCnt.Content = this.serv.ChannelsOnline.ToString();
			this.svrUsrCnt.Content = this.serv.UserOnline.ToString();
			this.svrCltCnt.Content = this.serv.MaximalClients.ToString();
			this.serv = null;
			base.InvalidateVisual();
			return true;
		}

		private void OnExpandChanged(object sender, RoutedEventArgs e)
		{
			if (!this._DisplayServerInfo)
			{
				this.sinfoRow.Height = new GridLength(0.0);
				return;
			}
			this.sinfoRow.Height = (this.expander1.IsExpanded ? new GridLength(160.0) : new GridLength(26.0));
		}

		private void treeView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			object selectedItem = this.treeView1.SelectedItem;
			if (selectedItem == null)
			{
				return;
			}
			if (selectedItem.GetType() == typeof(TreeChannel))
			{
				TreeChannel treeChannel = selectedItem as TreeChannel;
				string text = "ts3server://" + this._TS3Address + "?port=" + this.serv.ConnectionPort.ToString();
				if (this.NicknameRequired != null)
				{
					string text2 = this.NicknameRequired();
					if (!string.IsNullOrEmpty(text2))
					{
						text = text + "&nickname=" + text2;
					}
				}
				if (this.serv.ServerpwRequired && this.ServerpasswordRequired != null)
				{
					string text2 = this.ServerpasswordRequired();
					if (!string.IsNullOrEmpty(text2))
					{
						text = text + "&password=" + text2;
					}
				}
				if (treeChannel.NeedPassword && this.ChannelpasswordRequired != null)
				{
					string text2 = this.ChannelpasswordRequired();
					if (!string.IsNullOrEmpty(text2))
					{
						text = text + "&channelpassword=" + text2;
					}
				}
				text = text + "&channel=" + treeChannel.ClickName;
				Process.Start(text);
			}
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/Ts3Tray;component/ts3viewer.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.sinfoRow = (RowDefinition)target;
				return;
			case 2:
				this.serverLabel = (Label)target;
				return;
			case 3:
				this.treeView1 = (TreeView)target;
				this.treeView1.MouseDoubleClick += new MouseButtonEventHandler(this.treeView1_MouseDoubleClick);
				return;
			case 4:
				this.expander1 = (Expander)target;
				this.expander1.Collapsed += new RoutedEventHandler(this.OnExpandChanged);
				this.expander1.Expanded += new RoutedEventHandler(this.OnExpandChanged);
				return;
			case 5:
				this.svrOs = (Label)target;
				return;
			case 6:
				this.svrVersion = (Label)target;
				return;
			case 7:
				this.svrUptime = (Label)target;
				return;
			case 8:
				this.svrChanCnt = (Label)target;
				return;
			case 9:
				this.svrUsrCnt = (Label)target;
				return;
			case 10:
				this.svrCltCnt = (Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
