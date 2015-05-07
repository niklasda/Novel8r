using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Novel8r.Logic.Common;
using Novel8r.Logic.DomainModel;
using Novel8r.Logic.DomainModel.Search;
using Novel8r.Logic.Factories;
using Novel8r.Logic.Interfaces;
using Novel8r.WinForms.NodeTypes;
using Novel8r.WinForms.Views.MdiChildren;

namespace Novel8r.WinForms.Presenters.MdiChildren
{
    public class SearchPresenter : IPresenter, IViewPresenter<SearchChildForm>
    {
        //
        // Members
        //

        private SearchChildForm _view;

        #region IPresenter<SearchChildForm> Members

        public void Init()
        {
    //        _view = view;

            _view.MdiParent = MainPresenter.Instance.View;


            IList<ValueListItem> areas = new List<ValueListItem>();
            areas.Add(new ValueListItem(SearchAreas.Table, SearchAreas.Table));
            areas.Add(new ValueListItem(SearchAreas.View, SearchAreas.View));
            _view.cboSearchAreaType.DataSource = areas;
            _view.cboSearchAreaType.SelectedIndex = 0;

            _view.btnSearch.Click += btnSearch_Click;
            _view.txtSearchArea.KeyUp += btnSearch_KeyUp;
            _view.MouseEnter += _view_MouseEnter;
            _view.grpWhat.MouseEnter += _view_MouseEnter;
            _view.grpCriteria.MouseEnter += _view_MouseEnter;

            _view.grdResults.InitializeRow += grdData_InitializeRow;
            _view.grdResults.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
            _view.grdResults.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
            _view.grdResults.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
            _view.grdResults.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
            _view.grdResults.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
            _view.grdResults.DoubleClickCell += grdResults_DoubleClickCell;
        }

        public SearchChildForm View
        {
            get { return _view; }
            set { _view = value; }
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

        private void grdResults_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            var hit = e.Cell.Row.ListObject as SearchHit;
            //string colHeader = e.Cell.Column.Header.Caption;

//            MainPresenter.Instance.SelectNode(hit);
        }


        private void grdData_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Index%2 == 0)
            {
                e.Row.Appearance.BackColor = Color.FromKnownColor(KnownColor.ControlLight);
            }
            else
            {
                e.Row.Appearance.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight);
            }
        }

        private void _view_MouseEnter(object sender, EventArgs e)
        {
            SetCurrentDatabase();
        }

        public void SetCurrentDatabase()
        {
            ServerNode sn = MainPresenter.Instance.GetServerNode();
            DatabaseNode dbn = MainPresenter.Instance.GetDatabaseNode();
            string serverName;
            string databaseName;
            string sdb = string.Empty;

            if (sn != null && sn.ServerObject != null)
            {
                serverName = sn.ServerObject.Name;
            }
            else
            {
                serverName = "<none>";
            }

            if (dbn != null && dbn.DatabaseObject != null)
            {
                databaseName = dbn.DatabaseObject.Name;
                sdb = string.Format("{0}.{1}", serverName, databaseName);
            }
            else
            {
                databaseName = "<all>";
            }

            _view.lblWhat.Text = string.Format("Column names in {0}.{1}", serverName, databaseName);
            _view.Text = string.Format("{0}: {1}", MainPresenter.MdiTabKeys.Search, sdb);

        }

        private void btnSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                _view.btnSearch.Select();
                search();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
        }

        private void search()
        {
            string searchArea = _view.cboSearchAreaType.Text;
            string searchCriteria = _view.txtSearchArea.Text;
            bool caseSensitive = _view.chkCaseSensitive.Checked;
            bool exactMatch = _view.chkExactMatch.Checked;
            bool includeSystemObjects = _view.chkIncludeSystemObjects.Checked;

            ServerNode sn = MainPresenter.Instance.GetServerNode();
            DatabaseNode dbn = MainPresenter.Instance.GetDatabaseNode();


            if (sn != null)
            {
                Sql8rServer s = sn.ServerObject;
                Sql8rDatabase db = null;
                if (dbn != null)
                {
                    db = dbn.DatabaseObject;
                }

                var factory = SearchManagerFactory.Instance;
                ISearchManager man = factory.GetDomainSearchManager();

                _view.grdResults.DataSource = null;
                if (searchArea == SearchAreas.Table)
                {
                    _view.grdResults.DataSource = man.SearchAllTables(s, db, searchCriteria, exactMatch, caseSensitive, includeSystemObjects);
                }
                else if (searchArea == SearchAreas.View)
                {
                    _view.grdResults.DataSource = man.SearchAllViews(s, db, searchCriteria, exactMatch, caseSensitive, includeSystemObjects);
                }

                if (_view.grdResults.DisplayLayout.Bands.Count > 0)
                {
                    autoSizeAllColumns(_view.grdResults.DisplayLayout.Bands[0]);
                }
            }
            else
            {
                MainPresenter.Instance.SetError("No active connection");
            }
        }

        private void autoSizeAllColumns(UltraGridBand b)
        {
            foreach (UltraGridColumn c in b.Columns)
            {
                c.PerformAutoResize(PerformAutoSizeType.VisibleRows, true);
            }
        }

        
    }
}