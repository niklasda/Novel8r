using Infragistics.Win.UltraWinTree;

namespace Novel8r.WinForms.NodeTypes.Interfaces
{
	internal interface IHaveParentNode
	{
		UltraTreeNode ParentNode { get; }
	}
}