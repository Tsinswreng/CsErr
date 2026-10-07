using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrNodes;

// 本類的被測類是 Tsinswreng.CsErr.ErrNodeExtn。
public partial class TestErrNodeExtn : ITester{
	public TestErrNodeExtn(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterToErr(Node);
		return Node;
	}
}