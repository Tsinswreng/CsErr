using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.WebAnss;

// 本類的被測類是 Tsinswreng.CsErr.Results.WebAns<T> 與 WebAns 工廠。
public partial class TestWebAns : ITester{
	public TestWebAns(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterDefaults(Node);
		RegisterMk(Node);
		return Node;
	}
}