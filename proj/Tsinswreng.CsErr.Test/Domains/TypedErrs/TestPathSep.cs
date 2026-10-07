using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.PathSep。
public partial class TestTypedErr{
	public void RegisterPathSep(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.PathSep)],
			"PathSep:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("分隔符取自型別", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			var E = TypedErr.Mk(K);
			T(E.PathSep == K.PathSep);
			T(E.PathSep == "/");
			return NIL;
		});

		R("型別的自訂分隔符會被帶出", async (o)=>{
			// 路徑相關成員是 init-only, 只能在建立時指定
			var K = new ErrNode{
				RelaPathSegs = ["A", "B"]
				,PathSep = "."
			};
			var E = TypedErr.Mk(K);
			T(E.PathSep == ".");
			T(E.Key == "A.B");
			return NIL;
		});
	}
}