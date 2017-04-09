using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class Password : Window, IComponentConnector
	{
		private string setup = "";

		internal Password TS3TrayPasswordrequired;

		internal Grid uiGridMain;

		internal SlickToggleButton CloseButton;

		internal Label TextLabel;

		internal PasswordBox pwText;

		internal Label TitleLabel;

		private bool _contentLoaded;

		public string Text
		{
			get
			{
				return this.TextLabel.Content.ToString().Substring(0, this.TextLabel.Content.ToString().Length - 1);
			}
			set
			{
				this.TextLabel.Content = value.ToString() + ":";
				base.Title = "TS3Tray " + value.ToString();
				this.TitleLabel.Content = base.Title;
			}
		}

		public string PasswordText
		{
			get
			{
				return this.pwText.Password;
			}
			set
			{
				this.pwText.Password = value;
				this.setup = value;
			}
		}

		public Password()
		{
			this.InitializeComponent();
			base.Left = SystemParameters.WorkArea.Width / 2.0 - base.Width / 2.0;
			base.Top = SystemParameters.WorkArea.Height / 2.0 - base.Height / 2.0;
		}

		private void Ok_Button_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		private void Cancel_Button_Click(object sender, RoutedEventArgs e)
		{
			this.pwText.Password = this.setup;
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
			Uri resourceLocator = new Uri("/Ts3Tray;component/password.xaml", UriKind.Relative);
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
				this.TS3TrayPasswordrequired = (Password)target;
				return;
			case 2:
				this.uiGridMain = (Grid)target;
				return;
			case 3:
				this.CloseButton = (SlickToggleButton)target;
				return;
			case 4:
				this.TextLabel = (Label)target;
				return;
			case 5:
				this.pwText = (PasswordBox)target;
				return;
			case 6:
				((Button)target).Click += new RoutedEventHandler(this.Ok_Button_Click);
				return;
			case 7:
				((Button)target).Click += new RoutedEventHandler(this.Cancel_Button_Click);
				return;
			case 8:
				this.TitleLabel = (Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
