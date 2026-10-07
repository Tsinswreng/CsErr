using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Errors;

// 只測 I_ErrorsExtn.ExtractErrViews。
public partial class TestErrorsExtn{
	public void RegisterExtractErrViews(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrorsExtn),
			[typeof(I_ErrorsExtn)],
			[nameof(I_ErrorsExtn.ExtractErrViews)],
			"ExtractErrViews:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("空容器返回空列表", async (o)=>{
			var Z = new StubErrContainer();
			T(Z.ExtractErrViews().Count == 0);
			return NIL;
		});

		R("視圖按原序返回", async (o)=>{
			var Z = new StubErrContainer();
			Z.Errors.Add(new TypedErrView{Key = "A"});
			Z.Errors.Add(new TypedErrView{Key = "B"});
			var Views = Z.ExtractErrViews();
			T(Views.Count == 2);
			T(Views[0].Key == "A");
			T(Views[1].Key == "B");
			return NIL;
		});

		R("嵌套容器被攤平", async (o)=>{
			var Root = new StubErrContainer();
			var Inner = new StubErrContainer();
			Inner.Errors.Add(new TypedErrView{Key = "Inner/Err"});
			Root.Errors.Add(Inner);
			var Views = Root.ExtractErrViews();
			T(Views.Count == 1);
			T(Views[0].Key == "Inner/Err");
			return NIL;
		});

		R("起點容器自己不入列表", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["Self"]));
			E.AddErr(new TypedErrView{Key = "Child/Err"});
			var Views = E.ExtractErrViews();
			// 只收容器*內容*, 起點自己不會被收
			T(Views.Count == 1);
			T(Views[0].Key == "Child/Err");
			return NIL;
		});

		R("容器自身是視圖時自己也入列表", async (o)=>{
			// TypedErr 既是視圖又是容器。
			// 必須把它放進另一個容器, 才看得到「先按視圖收自己、再遞歸收內層」這兩步
			var Root = new StubErrContainer();
			var E = TypedErr.Mk(ErrNode.Mk(null, ["Self"]));
			E.AddErr(new TypedErrView{Key = "Child/Err"});
			Root.Errors.Add(E);
			var Views = Root.ExtractErrViews();
			T(Views.Count == 2);
			T(Views[0].Key == "Self");
			T(Views[1].Key == "Child/Err");
			return NIL;
		});

		R("錯誤鍵亦被收為視圖", async (o)=>{
			var Z = new StubErrContainer();
			var Root = ErrNode.Mk(null, ["User"]);
			Z.Errors.Add(ErrNode.Mk(Root, ["InvalidToken"]));
			var Views = Z.ExtractErrViews();
			T(Views.Count == 1);
			T(Views[0].Key == "User/InvalidToken");
			return NIL;
		});

		R("不可適配之項被忽略", async (o)=>{
			var Z = new StubErrContainer();
			Z.Errors.Add("數據庫連線失敗");
			Z.Errors.Add(new Exception("爆了"));
			Z.Errors.Add(null);
			Z.Errors.Add(new TypedErrView{Key = "A"});
			var Views = Z.ExtractErrViews();
			// 認不出的錯誤被靜默丟掉是既定職責, 不是缺陷
			T(Views.Count == 1);
			T(Views[0].Key == "A");
			return NIL;
		});

		R("自引用不成環", async (o)=>{
			var E = TypedErr.Mk(ErrNode.Mk(null, ["SelfCycle"]));
			E.Errors.Add(E);
			var Views = E.ExtractErrViews();
			T(Views.Count == 1);
			T(Views[0].Key == "SelfCycle");
			return NIL;
		});

		R("兩容器互指不成環", async (o)=>{
			var A = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			var B = TypedErr.Mk(ErrNode.Mk(null, ["B"]));
			A.Errors.Add(B);
			B.Errors.Add(A);
			var Views = A.ExtractErrViews();
			T(Views.Count == 2);
			T(Views[0].Key == "B");
			T(Views[1].Key == "A");
			return NIL;
		});

		R("同一容器出現於兩分支時各展開一次", async (o)=>{
			var Root = new StubErrContainer();
			var Shared = TypedErr.Mk(ErrNode.Mk(null, ["Shared"]));
			var X = TypedErr.Mk(ErrNode.Mk(null, ["X"]));
			X.Errors.Add(Shared);
			var Y = TypedErr.Mk(ErrNode.Mk(null, ["Y"]));
			Y.Errors.Add(Shared);
			Root.Errors.Add(X);
			Root.Errors.Add(Y);
			var Views = Root.ExtractErrViews();
			// 防環只認「當前這條路徑」, 故兩條不相干的分支各收一次
			T(Views.Count == 4);
			T(Views[0].Key == "X");
			T(Views[1].Key == "Shared");
			T(Views[2].Key == "Y");
			T(Views[3].Key == "Shared");
			return NIL;
		});
	}
}