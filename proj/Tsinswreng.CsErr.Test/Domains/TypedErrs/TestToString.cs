using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.ToString。
// 不鎖首行文案: Mk 不設 Message, 首行是 CLR 的默認訊息, 不是本庫的契約。
public partial class TestTypedErr{
	public void RegisterToString(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.ToString)],
			"ToString:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("內層錯誤之文字出現於輸出", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			E.AddErr("內層錯誤甲");
			var S = E.ToString();
			T(S.Contains("內層錯誤甲"));
			return NIL;
		});

		R("無內容時亦不拋出", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			var S = E.ToString();
			T(S is not null);
			return NIL;
		});
	}
}