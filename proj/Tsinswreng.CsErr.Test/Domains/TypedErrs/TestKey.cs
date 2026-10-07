using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.Key。
public partial class TestTypedErr{
	public void RegisterKey(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.Key)],
			"Key:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("鍵等於型別之全路徑", async (o)=>{
			var Root = ErrNode.Mk(null, ["User"]);
			var K = ErrNode.Mk(Root, ["InvalidToken"]);
			var E = TypedErr.Mk(K);
			T(E.Key == "User/InvalidToken");
			T(E.Key == K.ToString());
			return NIL;
		});

		R("改了型別則鍵跟著變", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			T(E.Key == "A");
			E.Type = ErrNode.Mk(null, ["B"]);
			// 鍵不是快照, 是由型別現算的
			T(E.Key == "B");
			return NIL;
		});
	}
}