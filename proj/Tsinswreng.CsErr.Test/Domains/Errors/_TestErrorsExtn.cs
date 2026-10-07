using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Errors;

// 本類的被測類是 Tsinswreng.CsErr.I_ErrorsExtn。
public partial class TestErrorsExtn : ITester{
	public TestErrorsExtn(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterAddErr(Node);
		RegisterExtractErrViews(Node);
		RegisterToTypedErr(Node);
		return Node;
	}
}