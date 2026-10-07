using Tsinswreng.CsErr;
namespace CsErr.Test.Domains.TypedErrs;

/// 測試替身: 一個不是 TypedErr 的 ITypedErr 實現。
/// 被測的庫裏只有 TypedErr 一個實現, 而 AsOrToTypedErr 的分支之一
/// 正是「別的實現」, 故在此自備一個, 用來驗證欄位是否被照搬。
public class StubTypedErr : ITypedErr{
	public IErrNode Type{get;set;} = null!;
	public IList<obj?>? DebugArgs{get;set;}
	public IList<obj?>? Args{get;set;}
	public IList<obj?> Errors{get;set;} = new List<obj?>();

	public str Key => Type.ToString()!;
	public str? PathSep => Type.PathSep;
	public ISet<str>? Tags => Type.Tags;
}