﻿namespace WindowChromeMultiscreen.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            RefreshMaximizeRestoreButton();
            MouseLeftButtonDown += (_, _) => { DragMove(); };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            ((HwndSource)PresentationSource.FromVisual(this))?.AddHook(NativeMethods.HookProc);
        }

        #region Minimize, Maximize, Restore and Close buttons

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnMinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnMaximizeRestoreButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void RefreshMaximizeRestoreButton()
        {
            var isMaximized = WindowState == WindowState.Maximized;

            MaximizeButton.Visibility = isMaximized ? Visibility.Collapsed : Visibility.Visible;
            RestoreButton.Visibility = isMaximized ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion // End of: Minimize, Maximize, Restore and Close buttons

        private void Window_StateChanged(object sender, EventArgs e) => RefreshMaximizeRestoreButton();
    }
}