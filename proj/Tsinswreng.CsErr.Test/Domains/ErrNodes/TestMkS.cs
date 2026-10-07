using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrNodes;

// 只測 ErrNode.MkS。
// 不測「是否自動帶 Private」: 實作裏那行目前是註釋狀態, 帶不帶尚未定案,
// 寫成用例等於替庫定案。
public partial class TestErrNode{
	public void RegisterMkS(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrNode),
			[typeof(ErrNode)],
			[nameof(ErrNode.MkS)],
			"MkS:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("自動帶系統異常標籤", async (o)=>{
			var N = ErrNode.MkS(null, ["DbDown"]);
			T(N.Tags!.Contains(ErrTags.SysErr));
			return NIL;
		});

		R("調用方額外標籤一併保留", async (o)=>{
			var N = ErrNode.MkS(null, ["DbDown"], ["Custom"]);
			T(N.Tags!.Count == 2);
			T(N.Tags.Contains("Custom"));
			T(N.Tags.Contains(ErrTags.SysErr));
			return NIL;
		});
	}
}