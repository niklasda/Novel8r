using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using Infragistics.Win.UltraWinToolbars;
using Novel8r.Logic.Connection;
using Novel8r.Logic.Exceptions;
using Novel8r.Logic.Helpers;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Factories;

namespace Novel8r.WinForms.Presenters.DockedChildren
{
	internal static class RibbonEventHandlers
	{
		internal static void btAbout_ToolClick(object sender, ToolClickEventArgs e)
		{
			IPresenter p = PresenterFactory.Instance.GetAboutPresenter();
			p.ShowDialog();
		}

		internal static void btExit_ToolClick(object sender, ToolClickEventArgs e)
		{
			Application.Exit();
		}

		internal static void btUpgradeCheck_ToolClick(object sender, ToolClickEventArgs e)
		{
			try
			{
				var url = new Uri("http://sql8r.se/updates/latestversion.xml");
				WebRequest r = WebRequest.Create(url);
				WebResponse response = r.GetResponse();
				Stream s = response.GetResponseStream();
				var sr = new StreamReader(s);

				string xml = sr.ReadToEnd();
				response.Close();

				var doc = new XmlDocument();
				doc.LoadXml(xml);
				int major = int.Parse(doc.GetElementsByTagName("major")[0].InnerText);
				int minor = int.Parse(doc.GetElementsByTagName("minor")[0].InnerText);
				//int bulid = int.Parse(doc.GetElementsByTagName("build")[0].InnerText);
				var av = new Version(major, minor);

				var iv = DialogHelper.Instance.GetAssemblyVersion();

				IPresenter p = PresenterFactory.Instance.GetUpdatesPresenter(iv, av);
				p.ShowDialog();
			}
			catch (WebException wex)
			{
				throw new SQL8rException("Unable to perform version check", wex);
			}
		}

		internal static void btSvr_ToolClick(object sender, ToolClickEventArgs e)
		{
			var settings = (Logic.DomainModel.Project.Sql8rProjectFile)e.Tool.Tag;
			//MainPresenter.Instance.ConnectToServer(settings);
			ProjectPresenter.Instance.OpenProject(settings.Path);
            MainPresenter.Instance.OpenPane(MainPresenter.DockedPaneKeys.Files);
		}

		internal static void btExternalTool_ToolClick(object sender, ToolClickEventArgs e)
		{
			if (e.Tool.OwningRibbonGroup != null && e.Tool.OwningRibbonGroup.Tab.Key == "rtTools")
			{
				executeCommand(e.Tool.Tag.ToString());
			}
		}

		private static void executeCommand(string toolPath)
		{
			Process.Start(toolPath);
		}
	}
}