using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Answers;

// 只測 IAnswerExtn.OkWith。
public partial class TestIAnswerExtn{
	public void RegisterOkWith(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestIAnswerExtn),
			[typeof(IAnswerExtn)],
			[nameof(IAnswerExtn.OkWith)],
			"OkWith:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("設資料並置為成功", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			var Back = A.OkWith(42);
			T(A.Ok);
			T(A.Data == 42);
			T(ReferenceEquals(Back, A));
			return NIL;
		});

		R("不帶資料時用型別預設值", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			A.OkWith();
			T(A.Ok);
			T(A.Data == 0);
			return NIL;
		});
	}
}