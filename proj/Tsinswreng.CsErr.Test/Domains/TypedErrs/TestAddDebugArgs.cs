using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 ITypedErrExtn.AddDebugArgs。
public partial class TestITypedErrExtn{
	public void RegisterAddDebugArgs(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestITypedErrExtn),
			[typeof(ITypedErrExtn)],
			[nameof(ITypedErrExtn.AddDebugArgs)],
			"AddDebugArgs:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("追加而非覆蓋", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			E.AddDebugArgs("甲");
			E.AddDebugArgs("乙", 3);
			T(E.DebugArgs!.Count == 3);
			T((str?)E.DebugArgs[0] == "甲");
			T((str?)E.DebugArgs[1] == "乙");
			return NIL;
		});

		R("除錯參數為空引用時自動建立", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			E.DebugArgs = null;
			E.AddDebugArgs("甲");
			T(E.DebugArgs is not null);
			T(E.DebugArgs!.Count == 1);
			return NIL;
		});

		R("返回自身以便鏈式", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			T(ReferenceEquals(E.AddDebugArgs("甲"), E));
			return NIL;
		});
	}
}