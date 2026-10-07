using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrNodes;

// 只測 ErrNodeExtn.ToErr。
public partial class TestErrNodeExtn{
	public void RegisterToErr(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrNodeExtn),
			[typeof(ErrNodeExtn)],
			[nameof(ErrNodeExtn.ToErr)],
			"ToErr:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("不帶參數時參數列表為空", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			var E = K.ToErr();
			T(E.Args is not null);
			T(E.Args!.Count == 0);
			return NIL;
		});

		R("型別即該錯誤鍵本身", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			var E = K.ToErr();
			// 是同一個引用, 不是複製品
			T(ReferenceEquals(E.Type, K));
			return NIL;
		});

		R("帶參數時依序保留", async (o)=>{
			var K = ErrNode.Mk(null, ["Word", "__NotBelongToLang__"]);
			var E = K.ToErr("ことば", "zh-CN");
			T(E.Args!.Count == 2);
			T((str?)E.Args[0] == "ことば");
			T((str?)E.Args[1] == "zh-CN");
			return NIL;
		});
	}
}