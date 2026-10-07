using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 本類的被測類是 Tsinswreng.CsErr.ITypedErrExtn。
public partial class TestITypedErrExtn : ITester{
	public TestITypedErrExtn(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterAddDebugArgs(Node);
		RegisterAsOrToTypedErr(Node);
		return Node;
	}
}