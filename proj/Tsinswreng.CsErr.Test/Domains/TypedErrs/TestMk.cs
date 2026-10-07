using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.Mk。
public partial class TestTypedErr{
	public void RegisterMk(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.Mk)],
			"Mk:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("型別與參數皆設好", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			var E = TypedErr.Mk(K, "x", 1);
			T(ReferenceEquals(E.Type, K));
			T(E.Args!.Count == 2);
			T((str?)E.Args[0] == "x");
			T((int?)E.Args[1] == 1);
			return NIL;
		});

		R("不帶參數時參數列表為空", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			var E = TypedErr.Mk(K);
			T(E.Args is not null);
			T(E.Args!.Count == 0);
			return NIL;
		});
	}
}