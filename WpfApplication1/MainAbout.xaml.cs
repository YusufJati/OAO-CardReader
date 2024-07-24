using System;
using System.Windows;

namespace WpfApplication1
{
    public partial class MainAbout : Window
    {
        private bool isClosing = false;

        public MainAbout()
        {
            InitializeComponent();
            this.Deactivated += MainAbout_Deactivated;
            this.Closing += MainAbout_Closing;
        }

        private void MainAbout_Deactivated(object sender, EventArgs e)
        {
            if (!isClosing)
            {
                try
                {
                    isClosing = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MainAbout_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosing)
            {
                isClosing = true;
            }
        }
    }
}