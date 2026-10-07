using Tsinswreng.CsErr;
using CsErr.Test.Domains.Answers;
using CsErr.Test.Domains.ErrNodes;
using CsErr.Test.Domains.Errors;
using CsErr.Test.Domains.ErrViews;
using CsErr.Test.Domains.TypedErrViews;
using CsErr.Test.Domains.TypedErrs;
using CsErr.Test.Domains.WebAnss;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test;

/// 本測試專案的測試管理器。
/// 每個測試 csproj 都要有一個, 它收編本專案裏所有測試類;
/// 被測的庫有兩個: Tsinswreng.CsErr 與 Tsinswreng.CsErr.Results。
public class CsErrTestMgr : DiEtTestMgr{
	public static CsErrTestMgr Inst = new();

	public override ITestNode RegisterTestsInto(ITestNode? Node){
		Node = this.TestNode;
		// 錯誤鍵與錯誤視圖
		this.RegisterTester<TestErrNode>();
		this.RegisterTester<TestErrNodeExtn>();
		this.RegisterTester<TestErrView>();
		this.RegisterTester<TestTypedErrView>();
		// 錯誤容器
		this.RegisterTester<TestErrorsExtn>();
		// 具型別異常
		this.RegisterTester<TestTypedErr>();
		this.RegisterTester<TestITypedErrExtn>();
		// 返值包裝
		this.RegisterTester<TestAnswer>();
		this.RegisterTester<TestIAnswerExtn>();
		this.RegisterTester<TestWebAns>();
		this.RegisterTester<TestWebAnsExtn>();
		return Node;
	}
}