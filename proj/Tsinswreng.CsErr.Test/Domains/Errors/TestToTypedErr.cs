using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.Errors;

// 只測 I_ErrorsExtn.ToTypedErr。
public partial class TestErrorsExtn{
	public void RegisterToTypedErr(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestErrorsExtn),
			[typeof(I_ErrorsExtn)],
			[nameof(I_ErrorsExtn.ToTypedErr)],
			"ToTypedErr:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("錯誤列表為空時拋出", async (o)=>{
			var Z = new StubErrContainer();
			var Threw = false;
			try{
				Z.ToTypedErr();
			}catch(Exception){
				Threw = true;
			}
			T(Threw);
			return NIL;
		});

		R("首項不可適配時拋出", async (o)=>{
			var Z = new StubErrContainer();
			Z.Errors.Add("只是一條人話, 沒有錯誤鍵");
			var Threw = false;
			try{
				Z.ToTypedErr();
			}catch(Exception){
				Threw = true;
			}
			T(Threw);
			return NIL;
		});

		R("首項為錯誤鍵時型別全路徑對得上", async (o)=>{
			var Z = new StubErrContainer();
			var Root = ErrNode.Mk(null, ["User"]);
			Z.Errors.Add(ErrNode.Mk(Root, ["PasswordNotMatch"]));
			var E = Z.ToTypedErr();
			T(E.Type.ToString() == "User/PasswordNotMatch");
			return NIL;
		});

		R("其餘項原樣進返值之錯誤列表", async (o)=>{
			var Z = new StubErrContainer();
			Z.Errors.Add(ErrNode.Mk(null, ["A"]));
			Z.Errors.Add("第二項");
			Z.Errors.Add("第三項");
			var E = Z.ToTypedErr();
			// 餘者不轉換、不攤平, 原樣留著
			T(E.Errors.Count == 2);
			T((str?)E.Errors[0] == "第二項");
			T((str?)E.Errors[1] == "第三項");
			return NIL;
		});
	}
}