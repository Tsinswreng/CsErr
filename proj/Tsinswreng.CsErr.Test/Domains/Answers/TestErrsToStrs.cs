using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Answers;

// 只測 IAnswerExtn.ErrsToStrs。
public partial class TestIAnswerExtn{
	public void RegisterErrsToStrs(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestIAnswerExtn),
			[typeof(IAnswerExtn)],
			[nameof(IAnswerExtn.ErrsToStrs)],
			"ErrsToStrs:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("逐項轉成字串", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			A.AddErr("甲");
			A.AddErr(new Exception("乙"));
			var Strs = A.ErrsToStrs();
			T(Strs.Count == 2);
			T(Strs[0] == "甲");
			T(Strs[1].Contains("乙"));
			return NIL;
		});

		R("空錯誤列表返空列表", async (o)=>{
			IAnswer<int> A = new Answer<int>();
			T(A.ErrsToStrs().Count == 0);
			return NIL;
		});
	}
}