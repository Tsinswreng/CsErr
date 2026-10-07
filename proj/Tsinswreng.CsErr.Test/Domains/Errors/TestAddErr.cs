using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Errors;

// 只測 I_ErrorsExtn.AddErr。
public partial class TestErrorsExtn{
	public void RegisterAddErr(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrorsExtn),
			[typeof(I_ErrorsExtn)],
			[nameof(I_ErrorsExtn.AddErr)],
			"AddErr:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("加入後錯誤列表含該項", async (o)=>{
			var Z = new StubErrContainer();
			Z.AddErr("數據庫連線失敗");
			T(Z.Errors.Count == 1);
			T((str?)Z.Errors[0] == "數據庫連線失敗");
			return NIL;
		});

		R("錯誤列表為空引用時自動建立", async (o)=>{
			var Z = new StubErrContainer();
			Z.Errors = null!;
			Z.AddErr("甲");
			var Errs = Z.Errors;
			T(Errs is not null);
			T(Errs!.Count == 1);
			return NIL;
		});

		R("返回自身以便鏈式", async (o)=>{
			var Z = new StubErrContainer();
			T(ReferenceEquals(Z.AddErr("甲"), Z));
			return NIL;
		});

		R("對 Answer 加錯誤只記理由, 不動 Ok", async (o)=>{
			// Ok 只由調用方宣告成功: OkWith 或直接賦值。
			// 故先設成功再加錯誤, Ok 應仍為 true。
			IAnswer<int> A = new Answer<int>();
			A.OkWith(7);
			A.AddErr("事後記下的錯誤");
			T(A.Ok);
			T(A.Errors.Count == 1);
			return NIL;
		});
	}
}