using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrViews;

// 本類的被測類是 Tsinswreng.CsErr.TypedErrView。
public partial class TestTypedErrView : ITester{
	public TestTypedErrView(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterDefaults(Node);
		return Node;
	}
}