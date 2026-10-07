using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrNodes;

// 只測 ErrNode.Mk。
public partial class TestErrNode{
	public void RegisterMk(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrNode),
			[typeof(ErrNode)],
			[nameof(ErrNode.Mk)],
			"Mk:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("無父節點時全路徑即自身段", async (o)=>{
			var A = ErrNode.Mk(null, ["User"]);
			T(A.ToString() == "User");
			T(A.Parent is null);
			return NIL;
		});

		R("有父節點時全路徑父先子後", async (o)=>{
			var Root = ErrNode.Mk(null, ["User"]);
			var Leaf = ErrNode.Mk(Root, ["PasswordNotMatch"]);
			T(Leaf.ToString() == "User/PasswordNotMatch");
			T(ReferenceEquals(Leaf.Parent, Root));
			return NIL;
		});

		R("多段相對路徑按序展開", async (o)=>{
			var N = ErrNode.Mk(null, ["A", "B", "C"]);
			T(N.ToString() == "A/B/C");
			return NIL;
		});

		R("不傳標籤時標籤集為空", async (o)=>{
			var N = ErrNode.Mk(null, ["A"]);
			// 標籤集不應為空引用, 否則使用方處處要判空
			T(N.Tags is not null);
			T(N.Tags!.Count == 0);
			return NIL;
		});

		R("傳入標籤時標籤集含之", async (o)=>{
			var N = ErrNode.Mk(null, ["A"], ["Public", "BizErr"]);
			T(N.Tags!.Count == 2);
			T(N.Tags.Contains("Public"));
			T(N.Tags.Contains("BizErr"));
			return NIL;
		});

		R("標籤集不與傳入列表共用", async (o)=>{
			var Tags = new List<str>{"Public"};
			var N = ErrNode.Mk(null, ["A"], Tags);
			// 建立之後再改調用方的列表, 不應影響節點自己的標籤集
			Tags.Add("LateAdded");
			T(N.Tags!.Count == 1);
			T(!N.Tags.Contains("LateAdded"));
			return NIL;
		});

		R("路徑分隔符預設為斜槓", async (o)=>{
			var N = ErrNode.Mk(null, ["A"]);
			T(N.PathSep == ErrNode.DfltPathSep);
			T(ErrNode.DfltPathSep == "/");
			return NIL;
		});
	}
}