using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Ts3Tray
{
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public class NumericUpDownTextBox : TextBox, IComponentConnector
	{
		private VisualCollection controls;

		private Button upButton;

		private Button downButton;

		private ButtonsProperties ButtonsViewModel;

		private bool showButtons = true;

		private Timer buttonTimer;

		private Button timerButton;

		private double buttonsLeft;

		public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(int), typeof(NumericUpDownTextBox), new UIPropertyMetadata(2147483647, new PropertyChangedCallback(NumericUpDownTextBox.MinMaxValueChangedCallback)));

		public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(int), typeof(NumericUpDownTextBox), new UIPropertyMetadata(0, new PropertyChangedCallback(NumericUpDownTextBox.MinMaxValueChangedCallback)));

		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int?), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ValueChangedCallback)));

		public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register("ButtonBackground", typeof(Brush), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty ButtonForegroundProperty = DependencyProperty.Register("ButtonForeground", typeof(Brush), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty ButtonPressedBackgroundProperty = DependencyProperty.Register("ButtonPressedBackground", typeof(Brush), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty ButtonMouseOverBackgroundProperty = DependencyProperty.Register("ButtonMouseOverBackground", typeof(Brush), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty ButtonBorderBrushProperty = DependencyProperty.Register("ButtonBorderBrush", typeof(Brush), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty ButtonBorderThicknessProperty = DependencyProperty.Register("ButtonBorderThickness", typeof(Thickness?), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty ButtonCornerRadiusProperty = DependencyProperty.Register("ButtonCornerRadiusThickness", typeof(CornerRadius?), typeof(NumericUpDownTextBox), new UIPropertyMetadata(null, new PropertyChangedCallback(NumericUpDownTextBox.ButtonPropertyChangedCallback)));

		public static readonly DependencyProperty DelayProperty = DependencyProperty.Register("Delay", typeof(int), typeof(NumericUpDownTextBox), new UIPropertyMetadata(750));

		public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register("Interval", typeof(int), typeof(NumericUpDownTextBox), new UIPropertyMetadata(100));

		private bool _contentLoaded;

		protected override int VisualChildrenCount
		{
			get
			{
				if (this.showButtons)
				{
					return this.controls.Count + base.VisualChildrenCount;
				}
				return base.VisualChildrenCount;
			}
		}

		public int MaxValue
		{
			get
			{
				return (int)base.GetValue(NumericUpDownTextBox.MaxValueProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.MaxValueProperty, value);
			}
		}

		public int MinValue
		{
			get
			{
				return (int)base.GetValue(NumericUpDownTextBox.MinValueProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.MinValueProperty, value);
			}
		}

		public int? Value
		{
			get
			{
				return (int?)base.GetValue(NumericUpDownTextBox.ValueProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ValueProperty, value);
			}
		}

		public Brush ButtonBackground
		{
			get
			{
				return (Brush)base.GetValue(NumericUpDownTextBox.ButtonBackgroundProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonBackgroundProperty, value);
			}
		}

		public Brush ButtonForeground
		{
			get
			{
				return (Brush)base.GetValue(NumericUpDownTextBox.ButtonForegroundProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonForegroundProperty, value);
			}
		}

		public Brush ButtonPressedBackground
		{
			get
			{
				return (Brush)base.GetValue(NumericUpDownTextBox.ButtonPressedBackgroundProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonPressedBackgroundProperty, value);
			}
		}

		public Brush ButtonMouseOverBackground
		{
			get
			{
				return (Brush)base.GetValue(NumericUpDownTextBox.ButtonMouseOverBackgroundProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonMouseOverBackgroundProperty, value);
			}
		}

		public Brush ButtonBorderBrush
		{
			get
			{
				return (Brush)base.GetValue(NumericUpDownTextBox.ButtonBorderBrushProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonBorderBrushProperty, value);
			}
		}

		public Thickness? ButtonBorderThickness
		{
			get
			{
				return (Thickness?)base.GetValue(NumericUpDownTextBox.ButtonBorderThicknessProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonBorderThicknessProperty, value);
			}
		}

		public CornerRadius? ButtonCornerRadius
		{
			get
			{
				return (CornerRadius?)base.GetValue(NumericUpDownTextBox.ButtonCornerRadiusProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.ButtonCornerRadiusProperty, value);
			}
		}

		public int Delay
		{
			get
			{
				return (int)base.GetValue(NumericUpDownTextBox.DelayProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.DelayProperty, value);
			}
		}

		public int Interval
		{
			get
			{
				return (int)base.GetValue(NumericUpDownTextBox.IntervalProperty);
			}
			set
			{
				base.SetValue(NumericUpDownTextBox.IntervalProperty, value);
			}
		}

		public NumericUpDownTextBox()
		{
			this.InitializeComponent();
			ButtonsProperties buttonsProperties = new ButtonsProperties(this);
			this.ButtonsViewModel = buttonsProperties;
			this.upButton = new Button
			{
				Cursor = Cursors.Arrow,
				DataContext = buttonsProperties,
				Tag = true
			};
			this.upButton.Click += new RoutedEventHandler(this.Button_Click);
			this.upButton.PreviewMouseDown += new MouseButtonEventHandler(this.Button_PreviewMouseDown);
			this.upButton.PreviewMouseUp += new MouseButtonEventHandler(this.Button_PreviewMouseUp);
			this.downButton = new Button
			{
				Cursor = Cursors.Arrow,
				DataContext = buttonsProperties,
				Tag = false
			};
			this.downButton.Click += new RoutedEventHandler(this.Button_Click);
			this.downButton.PreviewMouseDown += new MouseButtonEventHandler(this.Button_PreviewMouseDown);
			this.downButton.PreviewMouseUp += new MouseButtonEventHandler(this.Button_PreviewMouseUp);
			this.controls = new VisualCollection(this);
			this.controls.Add(this.upButton);
			this.controls.Add(this.downButton);
			base.PreviewTextInput += new TextCompositionEventHandler(this.control_PreviewTextInput);
			base.PreviewKeyDown += new KeyEventHandler(this.control_PreviewKeyDown);
			base.LostFocus += new RoutedEventHandler(this.control_LostFocus);
		}

		protected override Size ArrangeOverride(Size arrangeSize)
		{
			double height = arrangeSize.Height;
			double width = arrangeSize.Width;
			this.showButtons = (width > 1.5 * height);
			if (this.showButtons)
			{
				double num = 3.0 * height / 4.0;
				Size size = new Size(num, height / 2.0);
				Size arrangeBounds = new Size(width - num, height);
				this.buttonsLeft = width - num;
				Rect finalRect = new Rect(new Point(this.buttonsLeft, 0.0), size);
				Rect finalRect2 = new Rect(new Point(this.buttonsLeft, height / 2.0), size);
				base.ArrangeOverride(arrangeBounds);
				this.upButton.Arrange(finalRect);
				this.downButton.Arrange(finalRect2);
				return arrangeSize;
			}
			return base.ArrangeOverride(arrangeSize);
		}

		protected override Visual GetVisualChild(int index)
		{
			if (index < base.VisualChildrenCount)
			{
				return base.GetVisualChild(index);
			}
			return this.controls[index - base.VisualChildrenCount];
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Add((sender == this.upButton) ? 1 : -1);
		}

		private void ButtonTimerCallback(object sender)
		{
			this.Add((sender == this.upButton) ? 1 : -1);
		}

		private void control_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if ("0123456789".IndexOf(e.Text) < 0)
			{
				if (e.Text == "-" && this.MinValue < 0 && (base.Text.Length == 0 || (base.CaretIndex == 0 && base.Text[0] != '-')))
				{
					e.Handled = false;
					return;
				}
				e.Handled = true;
				return;
			}
			else
			{
				if (base.Text.Length > 0 && base.CaretIndex == 0 && base.Text[0] == '-' && base.SelectionLength == 0)
				{
					e.Handled = true;
					return;
				}
				StringBuilder stringBuilder = new StringBuilder(base.Text);
				stringBuilder.Remove(base.CaretIndex, base.SelectionLength);
				stringBuilder.Insert(base.CaretIndex, e.Text);
				int value = int.Parse(stringBuilder.ToString());
				if (this.FixValueKeyPress(value))
				{
					e.Handled = false;
					return;
				}
				e.Handled = true;
				return;
			}
		}

		private void control_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Down)
			{
				this.HandleModifiers(-1);
				e.Handled = true;
				return;
			}
			if (e.Key == Key.Up)
			{
				this.HandleModifiers(1);
				e.Handled = true;
				return;
			}
			if (e.Key == Key.Space)
			{
				e.Handled = true;
				return;
			}
			e.Handled = false;
		}

		private void control_LostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(base.Text))
			{
				base.Text = "0";
			}
			this.FixValue();
		}

		private void HandleModifiers(int value)
		{
			if (Keyboard.Modifiers == ModifierKeys.Shift)
			{
				value *= 10;
			}
			this.Add(value);
		}

		private void Add(int value)
		{
			int? num = this.Value;
			if (!num.HasValue)
			{
				if (this.MaxValue < 0)
				{
					num = new int?(this.MaxValue);
				}
				else if (this.MinValue > 0)
				{
					num = new int?(this.MinValue);
				}
				else
				{
					num = new int?(0);
				}
			}
			num += value;
			if (num > this.MaxValue)
			{
				num = new int?(this.MaxValue);
			}
			else if (num < this.MinValue)
			{
				num = new int?(this.MinValue);
			}
			this.Value = num;
		}

		private int FixValue()
		{
			int num;
			if (int.TryParse(base.Text, out num))
			{
				if (num > this.MaxValue)
				{
					this.UpdateText(this.MaxValue);
					num = this.MaxValue;
				}
				else if (num < this.MinValue)
				{
					this.UpdateText(this.MinValue);
					num = this.MinValue;
				}
				else
				{
					this.Value = new int?(num);
				}
			}
			return num;
		}

		private bool FixValueKeyPress(int value)
		{
			if (value > this.MaxValue && this.MaxValue > 0)
			{
				this.UpdateText(this.MaxValue);
				return false;
			}
			if (value < this.MinValue && this.MinValue < 0)
			{
				this.UpdateText(this.MinValue);
				return false;
			}
			return true;
		}

		private void UpdateText(int value)
		{
			base.Text = value.ToString();
			base.CaretIndex = base.Text.Length;
			this.Value = new int?(value);
		}

		private static void ValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDownTextBox numericUpDownTextBox = d as NumericUpDownTextBox;
			numericUpDownTextBox.Text = e.NewValue.ToString();
		}

		private static void MinMaxValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDownTextBox numericUpDownTextBox = d as NumericUpDownTextBox;
			numericUpDownTextBox.FixValue();
		}

		private static void ButtonPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			NumericUpDownTextBox numericUpDownTextBox = (NumericUpDownTextBox)d;
			numericUpDownTextBox.ButtonsViewModel.NotifyPropertyChanged(e.Property.ToString());
		}

		private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.timerButton = (sender as Button);
			this.buttonTimer = new Timer(new TimerCallback(this.RepeatButtonCallback), null, this.Delay, this.Interval);
		}

		private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			this.buttonTimer.Dispose();
			this.buttonTimer = null;
		}

		private void RepeatButtonCallback(object o)
		{
			base.Dispatcher.Invoke(DispatcherPriority.Normal, new Action<int>(this.RepeatButtonDispatched), (this.timerButton == this.upButton) ? 1 : -1);
		}

		private void RepeatButtonDispatched(int value)
		{
			Point position = Mouse.GetPosition(this.timerButton);
			if (position.X >= 0.0 && position.Y >= 0.0 && position.X <= this.timerButton.ActualWidth && position.Y <= this.timerButton.ActualHeight)
			{
				this.Add(value);
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
			Uri resourceLocator = new Uri("/Ts3Tray;component/numericupdowntextbox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		[EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}
	}
}
