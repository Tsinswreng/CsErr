using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.ErrViews;

// 只測 ErrView.AsOrToErrView。
public partial class TestErrView{
	public void RegisterAsOrToErrView(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrView),
			[typeof(ErrView)],
			[nameof(ErrView.AsOrToErrView)],
			"AsOrToErrView:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("本來就是視圖的原樣返回", async (o)=>{
			var V = new TypedErrView{Key = "A/B"};
			// 同一個引用, 不再造一個
			T(ReferenceEquals(ErrView.AsOrToErrView(V), V));
			return NIL;
		});

		R("錯誤鍵之全路徑成為視圖之鍵", async (o)=>{
			var Root = ErrNode.Mk(null, ["User"]);
			var K = ErrNode.Mk(Root, ["PasswordNotMatch"]);
			var V = ErrView.AsOrToErrView(K);
			T(V is not null);
			T(V!.Key == "User/PasswordNotMatch");
			return NIL;
		});

		R("錯誤鍵之標籤被複製", async (o)=>{
			var K = ErrNode.Mk(null, ["A"], ["Public"]);
			var V = ErrView.AsOrToErrView(K)!;
			T(V.Tags!.Contains("Public"));
			// 改視圖不應連帶改到那些靜態錯誤鍵
			V.Tags.Add("OnlyInView");
			T(!K.Tags!.Contains("OnlyInView"));
			return NIL;
		});

		R("錯誤鍵無標籤時視圖之標籤為空集", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			K.Tags = null;
			var V = ErrView.AsOrToErrView(K)!;
			var Tags = V.Tags;
			T(Tags is not null);
			T(Tags!.Count == 0);
			return NIL;
		});

		R("字串不可適配", async (o)=>{
			T(ErrView.AsOrToErrView("數據庫連線失敗") is null);
			return NIL;
		});

		R("裸異常不可適配", async (o)=>{
			T(ErrView.AsOrToErrView(new Exception("爆了")) is null);
			return NIL;
		});

		R("空值不可適配", async (o)=>{
			T(ErrView.AsOrToErrView(null) is null);
			return NIL;
		});
	}
}