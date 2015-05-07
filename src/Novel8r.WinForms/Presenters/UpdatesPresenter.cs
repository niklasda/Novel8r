using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Views;

namespace Novel8r.WinForms.Presenters
{
	public class UpdatesPresenter : IPresenter, IViewPresenter<AboutForm>
	{
		private AboutForm _view;
		private static UpdatesPresenter _instance;

		private UpdatesPresenter()
		{
		}

		#region IPresenter<AboutForm> Members

		public void Init(Version iv, Version av)
		{
			string appName = DialogHelper.Instance.GetApplicationName();
			_view.Text = string.Format("Update check for {0}", appName);
			_view.lblProductName.Text = string.Format("{0}", appName);
			_view.lblCopyright.Text = DialogHelper.Instance.AssemblyCopyright;
			_view.lblCompanyName.LinkClicked += lblCompanyName_LinkClicked;
			_view.lblCompanyName2.LinkClicked += lblCompanyName_LinkClicked;

			var sb = new StringBuilder();

			if (av.Major > iv.Major || (av.Major == iv.Major && av.Minor > iv.Minor))
			{
				sb.AppendLine("A new version is available for download!");
				sb.Append("Installed version: ");
				sb.AppendLine(iv.ToString(2));
				sb.Append("Available version: ");
				sb.AppendLine(av.ToString());
			}
			else
			{
				sb.AppendLine("You are up-to-date!");
			}

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

		public static UpdatesPresenter Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new UpdatesPresenter();
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