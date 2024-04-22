using System.Drawing;
using System.Windows;
using System.Windows.Automation;
using Forms = System.Windows.Forms;

namespace Desktop.Services
{
    public class NotificationService
    {
        public Forms.NotifyIcon notifyIcon = default!;
        Window window = default!;

        public NotificationService() {}

        public NotificationService(Window window)
        {
            this.window = window!;
            this.window.Closed += HandleWindowClosed!;
        }

        #region // Notification Icon

        public void LoadNotificationIcon()
        {
            try
            {
                notifyIcon = new Forms.NotifyIcon();

                notifyIcon.Icon = new System.Drawing.Icon("../../../Ressources/Images/AppIcon.ico");
                notifyIcon.Text = "Alpha App";
                notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
                notifyIcon.ContextMenuStrip.Items.Add("Alpha App");
                notifyIcon.ContextMenuStrip.Items.Add("Quitter", Image.FromFile("../../../Ressources/Images/AppIcon.ico"), OnExistAppClicked!);
                notifyIcon.Visible = true;

                notifyIcon.DoubleClick += OnNotificationIconDoubleClick!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void OnExistAppClicked(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void HandleWindowClosed(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
        }

        // Action sur l'icon (à ajouter dans un event a l'appel)
        public void ToggleDisplayWindow(Window window)
        {
            if (window.WindowState != WindowState.Minimized)
            {
                window.WindowState = WindowState.Minimized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
                window.Activate();
            }
        }

        private void OnNotificationIconDoubleClick(object sender, EventArgs e)
        {
            ToggleDisplayWindow(this.window);
        }

        #endregion
    }
}
