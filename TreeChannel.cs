using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ts3Tray
{
	internal class TreeChannel
	{
		private string _Name = "";

		private string ParentChannelName = "";

		private bool _NeedPassword;

		private ImageSource _Icon;

		private IList<TreeChannel> _SubChannels = new List<TreeChannel>();

		private IList<TreeUser> _ChannelUser = new List<TreeUser>();

		private List<ImageSource> _Xtras = new List<ImageSource>();

		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		public ImageSource Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				this._Icon = value;
			}
		}

		public IList<TreeChannel> SubChannels
		{
			get
			{
				return this._SubChannels;
			}
			set
			{
				this._SubChannels = value;
			}
		}

		public IList<TreeUser> ChannelUser
		{
			get
			{
				return this._ChannelUser;
			}
			set
			{
				this._ChannelUser = value;
			}
		}

		public IList<object> Items
		{
			get
			{
				IList<object> list = new List<object>();
				foreach (TreeChannel current in this._SubChannels)
				{
					list.Add(current);
				}
				foreach (TreeUser current2 in this._ChannelUser)
				{
					list.Add(current2);
				}
				return list;
			}
		}

		public StackPanel Xtras
		{
			get
			{
				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;
				foreach (ImageSource current in this._Xtras)
				{
					Image image = new Image();
					image.Source = current;
					image.Height = 16.0;
					image.Width = 16.0;
					image.Margin = new Thickness(2.0);
					stackPanel.Children.Add(image);
				}
				return stackPanel;
			}
		}

		public string ClickName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.ParentChannelName))
				{
					return this.ParentChannelName + "/" + this._Name;
				}
				return this._Name;
			}
		}

		public bool NeedPassword
		{
			get
			{
				return this._NeedPassword;
			}
		}

		public TreeChannel(string name)
		{
			this._Name = name;
			this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/channel.png"));
		}

		public TreeChannel(string name, ImageSource icon)
		{
			this._Name = name;
			this._Icon = icon;
		}

		public TreeChannel(TS3Channel chan, string parentcn)
		{
			this._Name = chan.Title;
			this.ParentChannelName = parentcn;
			if (chan.HasPassword)
			{
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/channel_locked.png"));
			}
			else
			{
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/channel.png"));
			}
			if (chan.HasSubchannels)
			{
				parentcn = (string.IsNullOrEmpty(parentcn) ? this._Name : (parentcn + "/" + this._Name));
				foreach (TS3Channel current in chan.SubchannelList)
				{
					this._SubChannels.Add(new TreeChannel(current, parentcn));
				}
			}
			if (chan.HasUser)
			{
				foreach (TS3User current2 in chan.UserList)
				{
					this._ChannelUser.Add(new TreeUser(current2));
				}
			}
			if (chan.Codec == 3)
			{
				this.AddXtras("pack://application:,,,/Ts3Tray;component/Resources/channel_music.png");
			}
			if (chan.IsDefault)
			{
				this.AddXtras("pack://application:,,,/Ts3Tray;component/Resources/home.png");
			}
			if (chan.HasPassword)
			{
				this.AddXtras("pack://application:,,,/Ts3Tray;component/Resources/locked.png");
				this._NeedPassword = true;
			}
		}

		public void AddXtras(string uri)
		{
			this.AddXtras(new BitmapImage(new Uri(uri)));
		}

		public void AddXtras(ImageSource imgsrc)
		{
			this._Xtras.Add(imgsrc);
		}
	}
}
