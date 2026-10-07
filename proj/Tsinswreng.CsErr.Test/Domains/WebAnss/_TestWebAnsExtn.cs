using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.WebAnss;

// 本類的被測類是 Tsinswreng.CsErr.Results.WebAnsExtn。
public partial class TestWebAnsExtn : ITester{
	public TestWebAnsExtn(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterDataOrThrow(Node);
		return Node;
	}
}