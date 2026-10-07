using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.Tags。
public partial class TestTypedErr{
	public void RegisterTags(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.Tags)],
			"Tags:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("取出的標籤集是副本", async (o)=>{
			var K = ErrNode.Mk(null, ["A"], ["Public"]);
			var E = TypedErr.Mk(K);
			var Got = E.Tags!;
			Got.Add("OnlyInCopy");
			// 每次取出都是新集合, 改它不應影響型別上的標籤
			T(E.Tags!.Count == 1);
			T(!E.Tags.Contains("OnlyInCopy"));
			T(!K.Tags!.Contains("OnlyInCopy"));
			return NIL;
		});

		R("型別無標籤時返空集而非空引用", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			K.Tags = null;
			var E = TypedErr.Mk(K);
			T(E.Tags is not null);
			T(E.Tags!.Count == 0);
			return NIL;
		});
	}
}