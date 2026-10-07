using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.WebAnss;

// 只測 WebAns<T> 的預設值。
public partial class TestWebAns{
	public void RegisterDefaults(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestWebAns),
			[typeof(WebAns<obj>)],
			[
				nameof(WebAns<obj>.Data)
				,nameof(WebAns<obj>.Errors)
			],
			"Defaults:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("新建時資料與錯誤皆為空引用", async (o)=>{
			IWebAns<obj> A = new WebAns<obj>();
			T(A.Data is null);
			// 錯誤列表為空引用而不是空集: 序列化時不出現該字段
			T(A.Errors is null);
			return NIL;
		});
	}
}