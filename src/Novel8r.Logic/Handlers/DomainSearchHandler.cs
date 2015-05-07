//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Composition;
//using Novel8r.Logic.Common;
//using Novel8r.Logic.DomainModel;
//using Novel8r.Logic.DomainModel.Search;
//using Novel8r.Logic.Interfaces;

//namespace Novel8r.Logic.Handlers
//{
//    [Export(typeof(ISearchManager))]
//    public class DomainSearchHandler : ISearchManager
//    {

//        #region ISearchManager Members

//        public IList<SearchHit> SearchAllTables(Sql8rServer s, Sql8rDatabase db, string criteria, bool exactMatch, bool caseSensitive, bool includeSystemObjects)
//        {
//            return GetTableSearchResults(s, db, criteria, exactMatch, caseSensitive, includeSystemObjects);
//        }

//        public IList<SearchHit> SearchAllViews(Sql8rServer s, Sql8rDatabase db, string criteria, bool exactMatch, bool caseSensitive, bool includeSystemObjects)
//        {
//            return GetViewSearchResults(s, db, criteria, exactMatch, caseSensitive, includeSystemObjects);
//        }

//        #endregion

//        private IList<SearchHit> GetTableSearchResults(Sql8rServer s, Sql8rDatabase db, string criteria, bool exactMatch, bool caseSensitive, bool includeSystemObjects)
//        {
//            var hits = new List<SearchHit>();

//            if (db != null)
//            {
//                foreach (Sql8rTable t in db.Tables)
//                {
//                    if (!t.IsSystemObject || (t.IsSystemObject && includeSystemObjects))
//                    {
//                        IList<SearchHit> hit = checkForHit(s, db, t.IsSystemObject, SearchAreas.Table, t, criteria, exactMatch, caseSensitive);
//                        hits.AddRange(hit);
//                    }
//                }
//            }
//            else
//            {
//                foreach (Sql8rDatabase d in s.Databases)
//                {
//                    foreach (Sql8rTable t in d.Tables)
//                    {
//                        if (!t.IsSystemObject || (t.IsSystemObject && includeSystemObjects))
//                        {
//                            IList<SearchHit> hit = checkForHit(s, d, t.IsSystemObject, SearchAreas.Table, t, criteria, exactMatch, caseSensitive);
//                            hits.AddRange(hit);
//                        }
//                    }
//                }
//            }

//            return hits;
//        }

//        private IList<SearchHit> GetViewSearchResults(Sql8rServer s, Sql8rDatabase db, string criteria, bool exactMatch, bool caseSensitive, bool includeSystemObjects)
//        {
//            var hits = new List<SearchHit>();

//            if (db != null)
//            {
//                foreach (Sql8rView v in db.Views)
//                {
//                    if (!v.IsSystemObject || (v.IsSystemObject && includeSystemObjects))
//                    {
//                        IList<SearchHit> hit = checkForHit(s, db, v.IsSystemObject, SearchAreas.View, v, criteria, exactMatch, caseSensitive);
//                        hits.AddRange(hit);
//                    }
//                }
//            }
//            else
//            {
//                foreach (Sql8rDatabase d in s.Databases)
//                {
//                    foreach (Sql8rView v in d.Views)
//                    {
//                        if (!v.IsSystemObject || (v.IsSystemObject && includeSystemObjects))
//                        {
//                            IList<SearchHit> hit = checkForHit(s, db, v.IsSystemObject, SearchAreas.View, v, criteria, exactMatch, caseSensitive);
//                            hits.AddRange(hit);
//                        }
//                    }
//                }
//            }

//            return hits;
//        }

//        private IList<SearchHit> checkForHit(Sql8rServer s, Sql8rDatabase db, bool isSystemObject, string objectType, IColumnedObject o, string criteria, bool exactMatch, bool caseSensitive)
//        {
//            IList<SearchHit> hits = new List<SearchHit>();
//            StringComparison cases = StringComparison.OrdinalIgnoreCase;

//            if(caseSensitive)
//            {
//                cases = StringComparison.Ordinal;
//            }


//            foreach (Sql8rColumn c in o.Columns.Values)
//            {
//                bool IsMatch = false;
//                if (exactMatch)
//                {
//                    if (c.Name.Equals(criteria, cases))
//                    {
//                        IsMatch = true;
//                    }
//                }
//                else
//                {
//                    if (c.Name.IndexOf(criteria, cases) >= 0)
//                    {
//                        IsMatch = true;
//                    }
//                }

//                if (IsMatch)
//                {
//                    var hit = new SearchHit(s.Name, db.Name, isSystemObject, objectType, o.Schema, o.Name, c.Name);
//                    hits.Add(hit);
//                }
//            }
//            return hits;
//        }
//    }
//}