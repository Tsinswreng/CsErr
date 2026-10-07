using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrViews;

// 本類的被測類是 Tsinswreng.CsErr.ErrView。
public partial class TestErrView : ITester{
	public TestErrView(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterAsOrToErrView(Node);
		return Node;
	}
}