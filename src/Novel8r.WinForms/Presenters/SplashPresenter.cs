using System;
using System.Windows.Forms;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Views;

namespace Novel8r.WinForms.Presenters
{
    public class SplashPresenter : IPresenter, IViewPresenter<SplashForm>
    {
        private SplashForm _view;
        private static SplashPresenter _instance;
        private bool stop;

        private SplashPresenter()
        {
        }

        #region IPresenter<SplashForm> Members

        public void Init()
        {
            _view.Load += _view_Load;
            _view.FormClosing += _view_FormClosing;
            _view.timer.Tick += timer_Tick;
        }

        public SplashForm View
        {
            get { return _view; }
            set { _view = value; }
        }

        public static SplashPresenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SplashPresenter();
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

        private void _view_Load(object sender, EventArgs e)
        {
        //    _view.TopMost = true;
            _view.progressBar.Minimum = 0;
            _view.progressBar.Maximum = 100;
            _view.progressBar.Value = 1;
            _view.timer.Interval = 30;
            _view.timer.Enabled = true;
            _view.timer.Start();
     //       Application.DoEvents();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!stop)
            {
                if (_view.progressBar.Value < _view.progressBar.Maximum)
                {
                    _view.progressBar.Value++;
                }
                else if (_view.progressBar.Value == _view.progressBar.Maximum)
                {
                    _view.progressBar.Value = 0;
                }
            }
            else
            {
                _view.timer.Stop();
                _view.timer.Enabled = false;
                _view.Close();
            }
            // Application.DoEvents();
        }

        private void _view_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_view.timer.Enabled)
            {
                _view.timer.Stop();
                _view.timer.Enabled = false;
            }
        }

        public void Close()
        {
            stop = true;
        }
    }
}