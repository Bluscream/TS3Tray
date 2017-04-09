using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Ts3Tray.Properties;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class MainWindow : Window, IComponentConnector
	{
		private ExtendedNotifyIcon extendedNotifyIcon;

		private Storyboard gridFadeInStoryBoard;

		private Storyboard gridFadeOutStoryBoard;

		internal MainWindow TS3Tray;

		internal Grid uiGridMain;

		internal SlickToggleButton PinButton;

		internal Image PinImage;

		internal SlickToggleButton CloseButton;

		internal TS3Viewer tS3Viewer1;

		internal System.Windows.Controls.Label TitleLabel;

		private bool _contentLoaded;

		public MainWindow()
		{
			this.extendedNotifyIcon = new ExtendedNotifyIcon();
			this.extendedNotifyIcon.MouseLeave += new ExtendedNotifyIcon.MouseLeaveHandler(this.extendedNotifyIcon_OnHideWindow);
			this.extendedNotifyIcon.MouseMove += new ExtendedNotifyIcon.MouseMoveHandler(this.extendedNotifyIcon_OnShowWindow);
			this.extendedNotifyIcon.targetNotifyIcon.Text = "TS3Tray";
			this.extendedNotifyIcon.targetNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.targetNotifyIcon_MouseDoubleClick);
			this.InitializeComponent();
			base.Left = SystemParameters.WorkArea.Width - base.Width - 10.0;
			base.Top = SystemParameters.WorkArea.Height - base.Height;
			base.Opacity = 0.0;
			this.uiGridMain.Opacity = 0.0;
			this.gridFadeOutStoryBoard = (Storyboard)base.TryFindResource("gridFadeOutStoryBoard");
			this.gridFadeOutStoryBoard.Completed += new EventHandler(this.gridFadeOutStoryBoard_Completed);
			this.gridFadeInStoryBoard = (Storyboard)base.TryFindResource("gridFadeInStoryBoard");
			this.gridFadeInStoryBoard.Completed += new EventHandler(this.gridFadeInStoryBoard_Completed);
			this.LoadSettings();
			Settings settings = new Settings();
			if (string.IsNullOrEmpty(settings.TS3Address) && string.IsNullOrEmpty(settings.Nickname))
			{
				settings.Nickname = Environment.UserName;
				settings.Save();
			}
			this.PinButton.IsChecked = new bool?(settings.Pined);
			this.PinButton_Click(null, null);
			if (settings.Pined)
			{
				this.extendedNotifyIcon_OnShowWindow();
			}
		}

		private void targetNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.PinButton.IsChecked = !this.PinButton.IsChecked;
			this.PinButton_Click(null, null);
		}

		private void extendedNotifyIcon_OnShowWindow()
		{
			this.gridFadeOutStoryBoard.Stop();
			base.Opacity = 1.0;
			base.Topmost = true;
			if (this.uiGridMain.Opacity > 0.0 && this.uiGridMain.Opacity < 1.0)
			{
				this.uiGridMain.Opacity = 1.0;
				return;
			}
			if (this.uiGridMain.Opacity == 0.0)
			{
				this.gridFadeInStoryBoard.Begin();
			}
		}

		private void extendedNotifyIcon_OnHideWindow()
		{
			if (this.PinButton.IsChecked == true)
			{
				return;
			}
			this.gridFadeInStoryBoard.Stop();
			if (this.uiGridMain.Opacity == 1.0 && base.Opacity == 1.0)
			{
				this.gridFadeOutStoryBoard.Begin();
				return;
			}
			this.uiGridMain.Opacity = 0.0;
			base.Opacity = 0.0;
		}

		private void uiWindowMainNotification_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.extendedNotifyIcon.StopMouseLeaveEventFromFiring();
			this.gridFadeOutStoryBoard.Stop();
			this.uiGridMain.Opacity = 1.0;
		}

		private void uiWindowMainNotification_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			this.extendedNotifyIcon_OnHideWindow();
		}

		private void gridFadeOutStoryBoard_Completed(object sender, EventArgs e)
		{
			base.Opacity = 0.0;
		}

		private void gridFadeInStoryBoard_Completed(object sender, EventArgs e)
		{
			base.Opacity = 1.0;
		}

		private void PinButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.PinButton.IsChecked == true)
			{
				this.PinImage.Source = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/pinned.png"));
			}
			else
			{
				this.PinImage.Source = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/unpinned.png"));
			}
			new Settings
			{
				Pined = this.PinButton.IsChecked.Value
			}.Save();
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.extendedNotifyIcon.Dispose();
			base.Close();
		}

		private void SetupButton_Click(object sender, RoutedEventArgs e)
		{
			SetupWindow setupWindow = new SetupWindow();
			setupWindow.ShowDialog();
			this.LoadSettings();
		}

		private void AboutButton_Click(object sender, RoutedEventArgs e)
		{
			AboutBox aboutBox = new AboutBox();
			aboutBox.ShowDialog();
		}

		private string tS3Viewer1_ChannelpasswordRequired()
		{
			Password password = new Password();
			password.Text = "Channelpassword required";
			password.ShowDialog();
			if (!string.IsNullOrEmpty(password.PasswordText))
			{
				return password.PasswordText;
			}
			return null;
		}

		private string tS3Viewer1_NicknameRequired()
		{
			Settings settings = new Settings();
			return settings.Nickname;
		}

		private string tS3Viewer1_ServerpasswordRequired()
		{
			Settings settings = new Settings();
			if (!string.IsNullOrEmpty(settings.ServerPw))
			{
				return settings.ServerPw;
			}
			Password password = new Password();
			password.Text = "Serverpassword required";
			password.ShowDialog();
			if (!string.IsNullOrEmpty(password.PasswordText))
			{
				return password.PasswordText;
			}
			return null;
		}

		private void LoadSettings()
		{
			Settings settings = new Settings();
			this.tS3Viewer1.ServerAddress = settings.TS3Address;
			this.tS3Viewer1.QueryPort = settings.TS3QueryPort;
			this.tS3Viewer1.QueryUser = settings.TS3QueryUser;
			this.tS3Viewer1.QueryPasswort = settings.TS3QueryPw;
			this.tS3Viewer1.ServerNr = settings.TS3ServerNr;
			this.tS3Viewer1.DisplayServerInfo = settings.DisplayServerInfo;
			this.tS3Viewer1.UpdateIntervall = settings.Update;
			this.tS3Viewer1.Refresh();
		}

		private void tS3Viewer1_Error(Exception ex)
		{
			if (ex.Message == "Keine Verbindung!")
			{
				MessageBox.ShowDialog(MessageBox.MESSAGEBOX_ICON.ICON_ERROR, Ts3Tray.Properties.Resources.MESSAGEBOX_ERROR, Ts3Tray.Properties.Resources.MESSAGEBOX_TEXT1);
			}
		}

		private void tS3Viewer1_UserJoined(string[] names)
		{
			Settings settings = new Settings();
			if (!settings.NotificationJoin)
			{
				return;
			}
			if (settings.NotificationStyle == 0)
			{
				MessageBox.ShowDialog(MessageBox.MESSAGEBOX_ICON.ICON_INFO, Ts3Tray.Properties.Resources.MESSAGEBOX_INFO, Ts3Tray.Properties.Resources.MESSAGEBOX_TEXT2 + "\n" + string.Join(", ", names));
				return;
			}
			this.extendedNotifyIcon.targetNotifyIcon.ShowBalloonTip(5000, Ts3Tray.Properties.Resources.MESSAGEBOX_INFO, Ts3Tray.Properties.Resources.MESSAGEBOX_TEXT2 + "\n" + string.Join(", ", names), ToolTipIcon.Info);
		}

		private void tS3Viewer1_UserLeave(string[] names)
		{
			Settings settings = new Settings();
			if (!settings.NotificationLeave)
			{
				return;
			}
			if (settings.NotificationStyle == 0)
			{
				MessageBox.ShowDialog(MessageBox.MESSAGEBOX_ICON.ICON_INFO, Ts3Tray.Properties.Resources.MESSAGEBOX_INFO, Ts3Tray.Properties.Resources.MESSAGEBOX_TEXT3 + "\n" + string.Join(", ", names));
				return;
			}
			this.extendedNotifyIcon.targetNotifyIcon.ShowBalloonTip(5000, Ts3Tray.Properties.Resources.MESSAGEBOX_INFO, Ts3Tray.Properties.Resources.MESSAGEBOX_TEXT3 + "\n" + string.Join(", ", names), ToolTipIcon.Info);
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/Ts3Tray;component/mainwindow.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		[DebuggerNonUserCode]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.TS3Tray = (MainWindow)target;
				this.TS3Tray.MouseEnter += new System.Windows.Input.MouseEventHandler(this.uiWindowMainNotification_MouseEnter);
				this.TS3Tray.MouseLeave += new System.Windows.Input.MouseEventHandler(this.uiWindowMainNotification_MouseLeave);
				return;
			case 2:
				this.uiGridMain = (Grid)target;
				return;
			case 3:
				this.PinButton = (SlickToggleButton)target;
				return;
			case 4:
				this.PinImage = (Image)target;
				return;
			case 5:
				this.CloseButton = (SlickToggleButton)target;
				return;
			case 6:
				this.tS3Viewer1 = (TS3Viewer)target;
				return;
			case 7:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.SetupButton_Click);
				return;
			case 8:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.AboutButton_Click);
				return;
			case 9:
				((System.Windows.Controls.Button)target).Click += new RoutedEventHandler(this.CloseButton_Click);
				return;
			case 10:
				this.TitleLabel = (System.Windows.Controls.Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
