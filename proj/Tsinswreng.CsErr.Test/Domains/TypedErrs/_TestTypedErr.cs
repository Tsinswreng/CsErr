using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 本類的被測類是 Tsinswreng.CsErr.TypedErr。
public partial class TestTypedErr : ITester{
	public TestTypedErr(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterMk(Node);
		RegisterFromView(Node);
		RegisterFromViews(Node);
		RegisterKey(Node);
		RegisterPathSep(Node);
		RegisterTags(Node);
		RegisterToString(Node);
		return Node;
	}
}