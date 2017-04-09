using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Ts3Tray
{
	public class ButtonsProperties : INotifyPropertyChanged
	{
		private Brush isPressedDefault = new SolidColorBrush(Colors.LightBlue);

		private Brush isMouseOverDefault = new SolidColorBrush(Colors.Blue);

		private CornerRadius cornerRadiusDefault = new CornerRadius(3.0);

		public event PropertyChangedEventHandler PropertyChanged;

		private NumericUpDownTextBox parent
		{
			get;
			set;
		}

		public Brush Background
		{
			get
			{
				return this.parent.ButtonBackground ?? this.parent.Background;
			}
		}

		public Brush Foreground
		{
			get
			{
				return this.parent.ButtonForeground ?? this.parent.Foreground;
			}
		}

		public Brush BorderBrush
		{
			get
			{
				return this.parent.ButtonBorderBrush ?? this.parent.BorderBrush;
			}
		}

		public Thickness BorderThickness
		{
			get
			{
				Thickness? buttonBorderThickness = this.parent.ButtonBorderThickness;
				if (!buttonBorderThickness.HasValue)
				{
					return this.parent.BorderThickness;
				}
				return buttonBorderThickness.GetValueOrDefault();
			}
		}

		public CornerRadius CornerRadius
		{
			get
			{
				CornerRadius? buttonCornerRadius = this.parent.ButtonCornerRadius;
				if (!buttonCornerRadius.HasValue)
				{
					return this.cornerRadiusDefault;
				}
				return buttonCornerRadius.GetValueOrDefault();
			}
		}

		public Brush IsPressedBackground
		{
			get
			{
				return this.parent.ButtonPressedBackground ?? this.isPressedDefault;
			}
		}

		public Brush IsMouseOverBackground
		{
			get
			{
				return this.parent.ButtonMouseOverBackground ?? this.isMouseOverDefault;
			}
		}

		public ButtonsProperties(NumericUpDownTextBox textBox)
		{
			this.parent = textBox;
		}

		internal void NotifyPropertyChanged(string info)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
