using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 TypedErr.FromView。
// 不測「視圖的 PathSep 為空引用」這一情形: 那時實作用的是按空白字符拆,
// 與下一行補的預設分隔符不一致, 是否算缺陷尚未定案, 故不寫成用例。
public partial class TestTypedErr{
	public void RegisterFromView(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErr),
			[typeof(TypedErr)],
			[nameof(TypedErr.FromView)],
			"FromView:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("鍵按分隔符拆段且往返不變", async (o)=>{
			var V = new TypedErrView{Key = "User/PasswordNotMatch", PathSep = "/"};
			var E = TypedErr.FromView(V);
			T(E.Key == "User/PasswordNotMatch");
			T(E.Type.RelaPathSegs.Count == 2);
			T(E.Type.RelaPathSegs[0] == "User");
			T(E.Type.RelaPathSegs[1] == "PasswordNotMatch");
			return NIL;
		});

		R("自訂分隔符被保留", async (o)=>{
			var V = new TypedErrView{Key = "A.B.C", PathSep = "."};
			var E = TypedErr.FromView(V);
			T(E.PathSep == ".");
			T(E.Key == "A.B.C");
			return NIL;
		});

		R("參數保留", async (o)=>{
			var V = new TypedErrView{
				Key = "A"
				,Args = new List<obj?>{"p0", 1}
			};
			var E = TypedErr.FromView(V);
			T(E.Args!.Count == 2);
			T((str?)E.Args[0] == "p0");
			T((int?)E.Args[1] == 1);
			return NIL;
		});

		R("標籤被帶過去", async (o)=>{
			var V = new TypedErrView{
				Key = "A"
				,Tags = new HashSet<str>{"Public"}
			};
			var E = TypedErr.FromView(V);
			T(E.Tags!.Contains("Public"));
			return NIL;
		});

		R("視圖無標籤時標籤集為空而非空引用", async (o)=>{
			// 由外部反序列化來的視圖可能不帶標籤
			var V = new TypedErrView{Key = "A"};
			V.Tags = null!;
			var E = TypedErr.FromView(V);
			T(E.Tags is not null);
			T(E.Tags!.Count == 0);
			return NIL;
		});
	}
}