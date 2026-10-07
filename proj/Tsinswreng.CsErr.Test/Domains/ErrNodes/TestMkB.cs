using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrNodes;

// 只測 ErrNode.MkB。
public partial class TestErrNode{
	public void RegisterMkB(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrNode),
			[typeof(ErrNode)],
			[nameof(ErrNode.MkB)],
			"MkB:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("自動帶業務異常與公開兩個標籤", async (o)=>{
			var N = ErrNode.MkB(null, ["ArgErr"]);
			T(N.Tags!.Count == 2);
			T(N.Tags.Contains(ErrTags.BizErr));
			T(N.Tags.Contains(ErrTags.Public));
			return NIL;
		});

		R("調用方額外標籤一併保留", async (o)=>{
			var N = ErrNode.MkB(null, ["ArgErr"], ["Custom"]);
			// 約定標籤與調用方標籤是併集, 不是替代
			T(N.Tags!.Count == 3);
			T(N.Tags.Contains("Custom"));
			T(N.Tags.Contains(ErrTags.BizErr));
			return NIL;
		});

		R("路徑與父鏈與 Mk 相同", async (o)=>{
			var Root = ErrNode.Mk(null, ["User"]);
			var N = ErrNode.MkB(Root, ["PasswordNotMatch"]);
			T(N.ToString() == "User/PasswordNotMatch");
			T(ReferenceEquals(N.Parent, Root));
			return NIL;
		});
	}
}