using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrViews;

// 只測 TypedErrView 各成員的預設值。
// 這些預設值是序列化場景的契約, 故合在一檔; 若要測某成員的行為, 另開分檔。
public partial class TestTypedErrView{
	public void RegisterDefaults(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestTypedErrView),
			[typeof(TypedErrView)],
			[
				nameof(TypedErrView.Key)
				,nameof(TypedErrView.PathSep)
				,nameof(TypedErrView.Args)
				,nameof(TypedErrView.Tags)
			],
			"Defaults:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("鍵預設為空字串而非空引用", async (o)=>{
			var V = new TypedErrView();
			T(V.Key == "");
			return NIL;
		});

		R("路徑分隔符預設為斜槓", async (o)=>{
			var V = new TypedErrView();
			T(V.PathSep == "/");
			return NIL;
		});

		R("參數預設為空引用", async (o)=>{
			var V = new TypedErrView();
			T(V.Args is null);
			return NIL;
		});

		R("標籤集預設為空集而非空引用", async (o)=>{
			var V = new TypedErrView();
			var Tags = V.Tags;
			T(Tags is not null);
			T(Tags!.Count == 0);
			return NIL;
		});
	}
}