using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Answers;

// 本類的被測類是 Tsinswreng.CsErr.Results.IAnswerExtn。
public partial class TestIAnswerExtn : ITester{
	public TestIAnswerExtn(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterOkWith(Node);
		RegisterErrsToStrs(Node);
		RegisterDataOrThrow(Node);
		return Node;
	}
}