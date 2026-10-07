using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.WebAnss;

// 只測 WebAns.Mk。
public partial class TestWebAns{
	public void RegisterMk(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestWebAns),
			[typeof(WebAns)],
			[nameof(WebAns.Mk)],
			"Mk:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("設定資料", async (o)=>{
			var A = WebAns.Mk("資料");
			T((str?)A.Data == "資料");
			T(A.Errors is null);
			return NIL;
		});

		R("設定錯誤列表", async (o)=>{
			var Errs = new List<ITypedErrView>{
				new TypedErrView{Key = "A"}
				,new TypedErrView{Key = "B"}
			};
			var A = WebAns.Mk(null, Errs);
			T(A.Data is null);
			T(A.Errors is not null);
			T(A.Errors!.Count == 2);
			T(A.Errors[0].Key == "A");
			return NIL;
		});
	}
}