using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Answers;

// 只測 Answer<T> 的預設值。
public partial class TestAnswer{
	public void RegisterDefaults(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestAnswer),
			[typeof(Answer<int>)],
			[
				nameof(Answer<int>.Ok)
				,nameof(Answer<int>.Data)
				,nameof(Answer<int>.Errors)
			],
			"Defaults:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("新建時未成功", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			// 默認未成功: 調用方不檢查就會漏掉錯誤, 這是刻意的取捨
			T(!A.Ok);
			return NIL;
		});

		R("新建時資料為預設值", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			T(A.Data == 0);
			return NIL;
		});

		R("新建時錯誤列表為空集而非空引用", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			var Errs = A.Errors;
			T(Errs is not null);
			T(Errs!.Count == 0);
			return NIL;
		});
	}
}