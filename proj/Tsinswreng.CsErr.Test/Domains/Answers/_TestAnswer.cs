using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Answers;

// 本類的被測類是 Tsinswreng.CsErr.Results.Answer<T>。
// 注意: Answer<T> 是結構體, 用例一律以 IAnswer<T> 變數持有,
// 走的是接口(裝箱後的那一份), 否則擴展方法改的是副本。
public partial class TestAnswer : ITester{
	public TestAnswer(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterDefaults(Node);
		return Node;
	}
}