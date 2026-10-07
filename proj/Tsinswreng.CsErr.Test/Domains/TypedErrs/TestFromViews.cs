using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.FromViews。
public partial class TestTypedErr{
	public void RegisterFromViews(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.FromViews)],
			"FromViews:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("空列表拋出", async (o)=>{
			var Threw = false;
			try{
				TypedErr.FromViews([]);
			}catch(Exception){
				Threw = true;
			}
			T(Threw);
			return NIL;
		});

		R("首項成返值", async (o)=>{
			var Views = new List<ITypedErrView>{
				new TypedErrView{Key = "A"}
				,new TypedErrView{Key = "B"}
			};
			var E = TypedErr.FromViews(Views);
			T(E.Key == "A");
			return NIL;
		});

		R("餘者進返值之錯誤列表", async (o)=>{
			var Views = new List<ITypedErrView>{
				new TypedErrView{Key = "A"}
				,new TypedErrView{Key = "B"}
				,new TypedErrView{Key = "C"}
			};
			var E = TypedErr.FromViews(Views);
			T(E.Errors.Count == 2);
			T(((TypedErr)E.Errors[0]!).Key == "B");
			T(((TypedErr)E.Errors[1]!).Key == "C");
			return NIL;
		});
	}
}