using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrNodes;

// 一個測試類對應一個被測類; 本類的被測類是 Tsinswreng.CsErr.ErrNode。
// 主檔只組裝, 不在主檔寫用例; 每個被測函數一個分檔, 檔名即函數名。
public partial class TestErrNode : ITester{
	public TestErrNode(){
	}

	public ITestNode RegisterTestsInto(ITestNode? Node){
		Node ??= new TestNode();
		// 錯誤鍵是純數據、無副作用, 本來可並行;
		// 但失敗輸出的路徑按聲明次序讀起來更順, 故設為依序。
		Node.Ordered = true;
		Node.IsParallelRecursive = false;
		RegisterMk(Node);
		RegisterMkB(Node);
		RegisterMkS(Node);
		return Node;
	}
}