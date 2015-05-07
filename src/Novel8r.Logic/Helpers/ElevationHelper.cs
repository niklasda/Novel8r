using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;

namespace Novel8r.Logic.Helpers
{
    public static class ElevationHelper
    {
        [DllImport("user32")]
        internal static extern UInt32 SendMessage(IntPtr hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);

        private const int BCM_FIRST = 0x1600; //Normal button
        private const int BCM_SETSHIELD = (BCM_FIRST + 0x000C); //Elevated button

        public static bool IsAdmin()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            var p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void AddShieldToButton(Control b)
        {
//            b.FlatStyle = FlatStyle.System;
            SendMessage(b.Handle, BCM_SETSHIELD, 0, 0xFFFFFFFF);
        }

//        public static void RemoveShieldFromButton(Button b)
  //      {
    //        b.FlatStyle = FlatStyle.System;
      //      SendMessage(b.Handle, BCM_FIRST, 0, 0xFFFFFFFF);
        //}

        public static void RestartElevated(Form owner)
        {
            Application.Exit();
            
            var startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = "-connect";

            // Two lines below make the UAC dialog modal to this app
            startInfo.ErrorDialog = true;
            startInfo.ErrorDialogParentHandle = owner.Handle; 
            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return;
            }

        }
    
    }
}