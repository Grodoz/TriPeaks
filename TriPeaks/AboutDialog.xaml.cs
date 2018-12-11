﻿using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace TriPeaks
{
    /// <summary>
    /// Interaktionslogik für AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void CloseExecuted(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void Window_SourceInitialized(object sender, System.EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        private async void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            await Dispatcher.InvokeAsync(() =>
            {
                var procStart = Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
                procStart.Dispose();
            });
        }
    }

    internal class AboutDialogViewModel
    {
        private readonly string assemblyVersion = "Version 1.0";
        private readonly string assemblyCopyright = "&#169; dfragment.net 2015-2017";

        public AboutDialogViewModel()
        {
            var thisAssembly = GetType().Assembly;
            var copyrightAttr = thisAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            var version = thisAssembly.GetName().Version.ToString(2);

            if (copyrightAttr != null)
                assemblyCopyright = copyrightAttr.Copyright;
            assemblyVersion = $"Version {version}";
        }

        public string Version => assemblyVersion;

        public string Copyright => assemblyCopyright;
    }
}
