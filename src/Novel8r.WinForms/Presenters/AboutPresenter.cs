using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Views;

namespace Novel8r.WinForms.Presenters
{
    public class AboutPresenter : IPresenter, IViewPresenter<AboutForm>
    {
        private AboutForm _view;
        private static AboutPresenter _instance;

        private AboutPresenter()
        {
        }

        #region IPresenter<AboutForm> Members

        public void Init()
        {
            string appName = DialogHelper.Instance.GetApplicationName();
            _view.Text = string.Format("About {0}", appName);
            _view.lblProductName.Text = string.Format("{0}", appName);
            _view.lblCopyright.Text = DialogHelper.Instance.AssemblyCopyright;
            _view.lblCompanyName.LinkClicked += lblCompanyName_LinkClicked;
            _view.lblCompanyName2.LinkClicked += lblCompanyName_LinkClicked;

            var sb = new StringBuilder();
            
            sb.AppendLine("Using");
            sb.AppendLine("Compona SyntaxBox");
            sb.AppendLine("Icons from PI Diagona Pack from pinvoke.com");
            sb.AppendLine("");
            sb.AppendLine("Thanks");
            sb.AppendLine("Visual Studio");
            sb.AppendLine("ReSharper");
            sb.AppendLine("Clip-X");
            sb.AppendLine("TimeSprite");
            sb.AppendLine("VisualSVN");
            sb.AppendLine("TortoiseSVN");
            sb.AppendLine("Beanstalk");
            sb.AppendLine("GetSatisfaction");

            _view.txtDescription.Text = sb.ToString();
        }

        private void lblCompanyName_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
        {
            string url = e.LinkRef;
            Process.Start(url);
        }

        public AboutForm View
        {
            get { return _view; }
            set { _view = value; }
        }

        public static AboutPresenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AboutPresenter();
                }
                return _instance;
            }
        }

        public DialogResult ShowDialog()
        {
            return _view.ShowDialog();
        }

        public void Show()
        {
            _view.Show();
        }

        #endregion
    }
}
