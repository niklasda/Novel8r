using System.Collections.Generic;
using Novel8r.Logic.DomainModel;
using Novel8r.Logic.DomainModel.Search;

namespace Novel8r.Logic.Interfaces
{
    public interface ISearchManager
    {
//        string SearchTable(string tableName, string criteria, bool contains);
        IList<SearchHit> SearchAllTables(Sql8rServer s, Sql8rDatabase db, string criteria, bool exactMatch, bool caseSensitive, bool includeSystemObjects);
        IList<SearchHit> SearchAllViews(Sql8rServer s, Sql8rDatabase db, string criteria, bool exactMatch, bool caseSensitive, bool includeSystemObjects);
//        DataTable GetTableNames();
    }
}