using Tsinswreng.CsErr;
using Tsinswreng.CsTreeTest;

namespace CsErr.Test.Domains.TypedErrs;

// 只測 ITypedErrExtn.AsOrToTypedErr。
public partial class TestITypedErrExtn{
	public void RegisterAsOrToTypedErr(ITestNode Node){
		var register = Node.MkTestFnRegister(
			typeof(TestITypedErrExtn),
			[typeof(ITypedErrExtn)],
			[nameof(ITypedErrExtn.AsOrToTypedErr)],
			"AsOrToTypedErr:"
		);
		var R = register.Register;
		var T = Assert.IsTrue;

		R("已是實現類則原樣返回", async (o)=>{
			ITypedErr Z = TypedErr.Mk(ErrNode.Mk(null, ["A"]));
			// 同一個引用, 不再造一個
			T(ReferenceEquals(Z.AsOrToTypedErr(), Z));
			return NIL;
		});

		R("別的實現之各欄位被照搬", async (o)=>{
			var K = ErrNode.Mk(null, ["A"]);
			var Stub = new StubTypedErr{
				Type = K
				,Args = new List<obj?>{"p"}
				,DebugArgs = new List<obj?>{"d"}
				,Errors = new List<obj?>{"e"}
			};
			var E = ((ITypedErr)Stub).AsOrToTypedErr();
			T(!ReferenceEquals(E, Stub));
			T(ReferenceEquals(E.Type, K));
			T(ReferenceEquals(E.Args, Stub.Args));
			T(ReferenceEquals(E.DebugArgs, Stub.DebugArgs));
			T(ReferenceEquals(E.Errors, Stub.Errors));
			return NIL;
		});
	}
}