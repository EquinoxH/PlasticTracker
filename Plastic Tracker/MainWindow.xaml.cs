using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Plastic_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		// Events

		private void loaded(object sender, RoutedEventArgs e)
        {
			EnableBlur();
			PlasticManager.load();
			refreshPlastics();
        }

		private void closing(object sender, System.ComponentModel.CancelEventArgs e) {
			PlasticManager.save();
		}

		private void plasticUpdated(object sender, EventArgs e) {
			refreshPlastics();
        }

		private void addClicked(object sender, MouseButtonEventArgs e) {
			Plastic plastic = GetPlasticWindow.getPlastic();
			if(plastic != null) {
				PlasticManager.addNewPlastic(plastic);
				refreshPlastics();
            }
        }

		private void closeClicked(object sender, MouseButtonEventArgs e) {
			Application.Current.Shutdown();
        }

		// Private Functions

		private void refreshPlastics() {
			plasticsPanel.Children.Clear();
			PlasticManager.getAllPlastics().ForEach(addPlastic);
        }

		private void addPlastic(Plastic plastic) {
			PlasticPanel panel = new PlasticPanel(plastic);
			panel.PlasticUpdated += plasticUpdated;
			plasticsPanel.Children.Add(panel);
		}

		#region Blur

		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

		internal void EnableBlur() {
			var windowHelper = new WindowInteropHelper(this);

			var accent = new AccentPolicy();
			accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

			var accentStructSize = Marshal.SizeOf(accent);

			var accentPtr = Marshal.AllocHGlobal(accentStructSize);
			Marshal.StructureToPtr(accent, accentPtr, false);

			var data = new WindowCompositionAttributeData();
			data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
			data.SizeOfData = accentStructSize;
			data.Data = accentPtr;

			SetWindowCompositionAttribute(windowHelper.Handle, ref data);

			Marshal.FreeHGlobal(accentPtr);
		}

		#endregion
	}

	#region Blur

	internal enum AccentState
	{
		ACCENT_DISABLED = 0,
		ACCENT_ENABLE_GRADIENT = 1,
		ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
		ACCENT_ENABLE_BLURBEHIND = 3,
		ACCENT_INVALID_STATE = 4
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct AccentPolicy
	{
		public AccentState AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct WindowCompositionAttributeData
	{
		public WindowCompositionAttribute Attribute;
		public IntPtr Data;
		public int SizeOfData;
	}

	internal enum WindowCompositionAttribute
	{
		// ...
		WCA_ACCENT_POLICY = 19
		// ...
	}

	#endregion
}
