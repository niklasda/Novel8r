using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.SqlServer.MessageBox;

namespace Novel8r.Logic.Helpers
{
    public class DialogHelper
    {
        private static DialogHelper _instance;

        //
        // Constructors
        //

        private DialogHelper()
        {
        }

        public static DialogHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DialogHelper();
                }
                return _instance;
            }
        }

        public Version GetAssemblyVersion()
        {
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            return v;
        }

        public string GetApplicationName()
        {
            object[] attrO = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
            if (attrO.Length > 0)
            {
                var attr = (AssemblyConfigurationAttribute) attrO[0];

                return string.Format("{0} v{1} - {2}", Application.ProductName, Application.ProductVersion, attr.Configuration);
            }
        	return string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public void ShowExceptionDialog(Exception ex)
        {
            if (ex != null)
            {
                var box = new ExceptionMessageBox(ex);
                box.Caption = GetApplicationName();
                box.Show(null);
            }
        }
    }
}