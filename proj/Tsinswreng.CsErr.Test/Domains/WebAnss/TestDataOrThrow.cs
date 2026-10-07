using Tsinswreng.CsErr;
using Tsinswreng.CsErr.Results;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.WebAnss;

// 只測 WebAnsExtn.DataOrThrow。
// 只測無錯誤那條路: 有錯誤時實作先 FromViews 再 ToTypedErr,
// 錯誤恰好一條時返值的錯誤列表為空, ToTypedErr 會拋「列表為空」,
// 這是不是缺陷尚未定案, 故不寫成用例。
public partial class TestWebAnsExtn{
	public void RegisterDataOrThrow(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestWebAnsExtn),
			[typeof(WebAnsExtn)],
			[nameof(WebAnsExtn.DataOrThrow)],
			"DataOrThrow:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("無錯誤時返資料", async (o)=>{
			IWebAns<obj> A = WebAns.Mk("資料");
			T((str?)A.DataOrThrow() == "資料");
			return NIL;
		});

		R("錯誤列表為空集時返資料", async (o)=>{
			IWebAns<obj> A = WebAns.Mk("資料", []);
			T((str?)A.DataOrThrow() == "資料");
			return NIL;
		});
	}
}