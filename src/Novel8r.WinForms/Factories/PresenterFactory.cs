using System;
using System.Windows.Forms;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Presenters;
using Novel8r.WinForms.Presenters.CustomDialogs;
using Novel8r.WinForms.Presenters.DockedChildren;
using Novel8r.WinForms.Presenters.MdiChildren;
using Novel8r.WinForms.Views;
using Novel8r.WinForms.Views.CustomDialogs;
using Novel8r.WinForms.Views.MdiChildren;
using Novel8r.Logic.Factories;

namespace Novel8r.WinForms.Factories
{
    public class PresenterFactory
    {
        private static PresenterFactory _instance;

        private PresenterFactory()
        {
        }

        public static PresenterFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PresenterFactory();
                }
                return _instance;
            }
        }

        private P makePresenter<P, V>()
            where P : IViewPresenter<V>, new()
            where V : Form, new()
        {
            var view = new V();
            var presenter = new P();
            view.Tag = presenter;
            presenter.View = view;
            return presenter;
        }

        
        private IChildPresenter<V> makeChildPresenter<P, V>(IViewPresenter<V> parentPresenter)
            where P : IChildPresenter<V>, new()
            where V : Form, new()
        {
//            var view = new V();
            var childPresenter = new P();
            childPresenter.View = parentPresenter.View;
            childPresenter.SetParentPresenter(parentPresenter);
            return childPresenter;            
        }

        public ModifiedFilesPresenter GetModifiedFilesPresenter()
        {
//            var view = new ModifiedFilesDialog();
            ModifiedFilesPresenter presenter = makePresenter<ModifiedFilesPresenter, ModifiedFilesDialog>();
            presenter.Init();
            return presenter;
        }

        public AboutPresenter GetAboutPresenter()
        {
            AboutForm view = new AboutForm();
            AboutPresenter presenter = AboutPresenter.Instance;
            presenter.View = view;
            presenter.Init();
            return presenter;
        }

		public IPresenter GetUpdatesPresenter(Version installed, Version available)
		{
			AboutForm view = new AboutForm();
			UpdatesPresenter presenter = UpdatesPresenter.Instance;
			presenter.View = view;
			presenter.Init(installed, available);
			return presenter;
		}

        public MainPresenter GetMainPresenter(string[] args)
        {
            var view = new MainForm();
            var presenter = MainPresenter.Instance;
            presenter.View = view;
            presenter.Init(args);
            return presenter;
        }

        public SplashPresenter GetSplashPresenter()
        {
            var view = new SplashForm();
            var presenter = SplashPresenter.Instance;
            presenter.View = view;
            presenter.Init();
            return presenter;
        }

        public SearchPresenter GetSearchPresenter()
        {
            SearchPresenter presenter = makePresenter<SearchPresenter, SearchChildForm>();
            presenter.Init();
            return presenter;
        }

        public EditorRtfPresenter GetRtfEditor()
        {
//            EditorRtfForm view;
            EditorRtfPresenter presenter = makePresenter<EditorRtfPresenter, EditorRtfForm>();
            presenter.Init();
            return presenter;
        }

        public EditorPresenter GetEditorWithDataGridPresenter()
        {
            EditorPresenter presenter = makePresenter<EditorPresenter, EditorWithDataGridChildForm>();
      //      var editorPresenter = (EditorPresenter) makeChildPresenter<EditorPresenter, EditorWithDataGridChildForm>(presenter);
            //editorPresenter.Init();
            presenter.Init();
            return presenter;
        }

        public ProjectPresenter InitProjectPresenter()
        {
            MainForm view = MainPresenter.Instance.View;
            ProjectPresenter presenter = ProjectPresenter.Instance;
            presenter.View = view;
            presenter.Init();
            return presenter;
        }

        public RibbonPresenter InitRibbonPresenter()
        {
            MainForm view = MainPresenter.Instance.View;
            RibbonPresenter presenter = RibbonPresenter.Instance;
            presenter.View = view;
            presenter.Init();
            return presenter;
        }

    }
}
