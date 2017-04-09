using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ts3Tray
{
	internal class TreeUser
	{
		private string _Name = "";

		private ImageSource _Icon;

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

		public TreeUser(string name)
		{
			this._Name = name;
			this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_player.png"));
		}

		public TreeUser(string name, ImageSource icon)
		{
			this._Name = name;
			this._Icon = icon;
		}

		public TreeUser(TS3User user)
		{
			this._Name = user.Name;
			switch (user.ClientStatus)
			{
			case TS3User.CLIENTSTATUS.AWAY:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_away.png"));
				break;
			case TS3User.CLIENTSTATUS.ISTALKER:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_is_talker.png"));
				break;
			case TS3User.CLIENTSTATUS.TALKING:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_flag_talking.png"));
				break;
			case TS3User.CLIENTSTATUS.INPUTHARDWARE:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_input_hardware.png"));
				break;
			case TS3User.CLIENTSTATUS.OUTPUTHARDWARE:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_output_hardware.png"));
				break;
			case TS3User.CLIENTSTATUS.INPUTMUTED:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_input_muted.png"));
				break;
			case TS3User.CLIENTSTATUS.OUTPUTMUTED:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_output_muted.png"));
				break;
			default:
				this._Icon = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/client_player.png"));
				break;
			}
			if (user.IsChannelOperator)
			{
				this.AddXtras("pack://application:,,,/Ts3Tray;component/Resources/client_co.png");
			}
			if (user.IsChanneladmin)
			{
				this.AddXtras("pack://application:,,,/Ts3Tray;component/Resources/client_ca.png");
			}
			if (user.IsServeradmin)
			{
				this.AddXtras("pack://application:,,,/Ts3Tray;component/Resources/client_sa.png");
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
