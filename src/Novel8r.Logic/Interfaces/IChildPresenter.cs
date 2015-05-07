
namespace Novel8r.Logic.Interfaces
{
    public interface IChildPresenter<V>
    {
        V View { set; }
        void SetParentPresenter(IViewPresenter<V> parentPresenter);
    }
}