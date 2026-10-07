using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Answers;

// 只測 IAnswerExtn.DataOrThrow。
// 「未成功時拋出」只驗證有拋, 不鎖異常型別:
// 錯誤若是字串, 內部走 ToTypedErr 會因首項不可適配而拋普通 Exception,
// 那是不是缺陷尚未定案。
public partial class TestIAnswerExtn{
	public void RegisterDataOrThrow(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestIAnswerExtn),
			[typeof(IAnswerExtn)],
			[nameof(IAnswerExtn.DataOrThrow)],
			"DataOrThrow:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("成功時返資料", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			A.OkWith(7);
			T(A.DataOrThrow() == 7);
			return NIL;
		});

		R("未成功時拋出", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			A.AddErr("失敗原因");
			var Threw = false;
			try{
				A.DataOrThrow();
			}catch(Exception){
				Threw = true;
			}
			T(Threw);
			return NIL;
		});
	}
}