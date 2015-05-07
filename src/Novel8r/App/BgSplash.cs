
using System.ComponentModel;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.Factories;

namespace Novel8r.App
{
    public class BgSplash
    {
        private BackgroundWorker backgroundWorker1;

        public void Start()
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IPresenter p = PresenterFactory.Instance.GetSplashPresenter();

            p.ShowDialog();
        }
    }
}
