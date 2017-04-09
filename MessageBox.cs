using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class MessageBox : Window, IComponentConnector
	{
		public enum MESSAGEBOX_ICON
		{
			ICON_ERROR,
			ICON_INFO
		}

		internal MessageBox MessageBoxWnd;

		internal Grid uiGridMain;

		internal SlickToggleButton CloseButton;

		internal Image image1;

		internal TextBlock MessageText;

		internal Label MessageTitel;

		private bool _contentLoaded;

		public MessageBox()
		{
			this.InitializeComponent();
		}

		public void setIcon(MessageBox.MESSAGEBOX_ICON icon)
		{
			if (icon == MessageBox.MESSAGEBOX_ICON.ICON_INFO)
			{
				this.image1.Source = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/dialog-info.png"));
				return;
			}
			this.image1.Source = new BitmapImage(new Uri("pack://application:,,,/Ts3Tray;component/Resources/dialog-error.png"));
		}

		public void setText(string titel, string msg)
		{
			base.Title = titel;
			this.MessageTitel.Content = titel;
			this.MessageText.Text = msg;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}

		public static void ShowDialog(MessageBox.MESSAGEBOX_ICON icon, string titel, string text)
		{
			MessageBox messageBox = new MessageBox();
			messageBox.setIcon(icon);
			messageBox.setText(titel, text);
			messageBox.ShowDialog();
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/Ts3Tray;component/messagebox.xaml", UriKind.Relative);
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
				this.MessageBoxWnd = (MessageBox)target;
				return;
			case 2:
				this.uiGridMain = (Grid)target;
				return;
			case 3:
				this.CloseButton = (SlickToggleButton)target;
				return;
			case 4:
				this.image1 = (Image)target;
				return;
			case 5:
				this.MessageText = (TextBlock)target;
				return;
			case 6:
				((Button)target).Click += new RoutedEventHandler(this.Button_Click);
				return;
			case 7:
				this.MessageTitel = (Label)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}
	}
}
