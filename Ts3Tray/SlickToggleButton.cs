using System;
using System.Windows.Controls.Primitives;

namespace Ts3Tray
{
	public class SlickToggleButton : ToggleButton
	{
		private string cornerRadius;

		private string highlightBackground;

		private string pressedBackground;

		public string CornerRadius
		{
			get
			{
				return this.cornerRadius;
			}
			set
			{
				this.cornerRadius = value;
			}
		}

		public string HighlightBackground
		{
			get
			{
				return this.highlightBackground;
			}
			set
			{
				this.highlightBackground = value;
			}
		}

		public string PressedBackground
		{
			get
			{
				return this.pressedBackground;
			}
			set
			{
				this.pressedBackground = value;
			}
		}
	}
}
