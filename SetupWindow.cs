using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Ts3Tray.Properties;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class SetupWindow : Window, IComponentConnector
	{
		internal SetupWindow TS3TrayOptions;

		internal Grid uiGridMain;

		internal SlickToggleButton CloseButton;

		internal TextBox tsAddress;

		internal NumericUpDownTextBox tsQueryPort;

		internal TextBox tsQueryname;

		internal PasswordBox tsQuerypw;

		internal NumericUpDownTextBox tsServerNr;

		internal CheckBox checkBox1;

		internal CheckBox checkBox2;

		internal NumericUpDownTextBox tsUpdateIntervall;

		internal TextBox tsNickname;

		internal PasswordBox tsServerpw;

		internal CheckBox checkBoxNotification1;

		internal CheckBox checkBoxNotification2;

		internal ComboBox comboBoxNotificationType;

		internal Label TitleLabel;

		private bool _contentLoaded;

		public SetupWindow()
		{
			this.InitializeComponent();
			base.Left = SystemParameters.WorkArea.Width - base.Width - 10.0 - 300.0;
			base.Top = SystemParameters.WorkArea.Height - base.Height;
			Settings settings = new Settings();
			string[] array = Ts3Tray.Properties.Resources.SETUP_NOTIFICATION_TYPE_DATA.Split(new char[]
			{
				'|'
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string newItem = array2[i];
				this.comboBoxNotificationType.Items.Add(newItem);
			}
			this.comboBoxNotificationType.SelectedIndex = 0;
			this.tsAddress.Text = settings.TS3Address;
			this.tsQueryPort.Value = new int?(settings.TS3QueryPort);
			this.tsQueryname.Text = settings.TS3QueryUser;
			this.tsQuerypw.Password = settings.TS3QueryPw;
			this.tsServerNr.Value = new int?(settings.TS3ServerNr);
			this.checkBox1.IsChecked = new bool?(settings.DisplayServerInfo);
			this.checkBox2.IsChecked = new bool?(settings.Pined);
			this.tsUpdateIntervall.Value = new int?((int)settings.Update);
			this.tsNickname.Text = settings.Nickname;
			this.tsServerpw.Password = settings.ServerPw;
			this.checkBoxNotification1.IsChecked = new bool?(settings.NotificationJoin);
			this.checkBoxNotification2.IsChecked = new bool?(settings.NotificationLeave);
			try
			{
				this.comboBoxNotificationType.SelectedIndex = (int)settings.NotificationStyle;
			}
			catch (Exception)
			{
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			new Settings
			{
				TS3Address = this.tsAddress.Text.Trim(),
				TS3QueryPort = this.tsQueryPort.Value.Value,
				TS3QueryUser = this.tsQueryname.Text,
				TS3QueryPw = this.tsQuerypw.Password,
				TS3ServerNr = this.tsServerNr.Value.Value,
				DisplayServerInfo = this.checkBox1.IsChecked.Value,
				Pined = this.checkBox2.IsChecked.Value,
				Update = (byte)this.tsUpdateIntervall.Value.Value,
				Nickname = this.tsNickname.Text.Trim(),
				ServerPw = this.tsServerpw.Password.Trim(),
				NotificationJoin = this.checkBoxNotification1.IsChecked.Value,
				NotificationLeave = this.checkBoxNotification2.IsChecked.Value,
				NotificationStyle = (byte)this.comboBoxNotificationType.SelectedIndex
			}.Save();
			base.Close();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/Ts3Tray;component/setupwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
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
				this.TS3TrayOptions = (SetupWindow)target;
				return;
			case 2:
				this.uiGridMain = (Grid)target;
				return;
			case 3:
				this.CloseButton = (SlickToggleButton)target;
				return;
			case 4:
				this.tsAddress = (TextBox)target;
				return;
			case 5:
				this.tsQueryPort = (NumericUpDownTextBox)target;
				return;
			case 6:
				this.tsQueryname = (TextBox)target;
				return;
			case 7:
				this.tsQuerypw = (PasswordBox)target;
				return;
			case 8:
				this.tsServerNr = (NumericUpDownTextBox)target;
				return;
			case 9:
				this.checkBox1 = (CheckBox)target;
				return;
			case 10:
				this.checkBox2 = (CheckBox)target;
				return;
			case 11:
				this.tsUpdateIntervall = (NumericUpDownTextBox)target;
				return;
			case 12:
				this.tsNickname = (TextBox)target;
				return;
			case 13:
				this.tsServerpw = (PasswordBox)target;
				return;
			case 14:
				this.checkBoxNotification1 = (CheckBox)target;
				return;
			case 15:
				this.checkBoxNotification2 = (CheckBox)target;
				return;
			case 16:
				this.comboBoxNotificationType = (ComboBox)target;
				return;
			case 17:
				((Button)target).Click += new RoutedEventHandler(this.Button_Click);
				return;
			case 18:
				((Button)target).Click += new RoutedEventHandler(this.CloseButton_Click);
				return;
			case 19:
				this.TitleLabel = (Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
