namespace Tsinswreng.CsErr;


/// 應用基異常接口
[Doc($@"Base interface for application errors")]
public partial interface ITypedErr
	:ITypedErrView
	,I_Errors//內ʹ錯
{
	[Doc($@"Error type item for classification and key generation")]
	public IErrNode? Type{get;set;}
	/// 㕥置 未ToString之原始對象、用于除錯
	[Doc($@"Raw objects for debugging, not shown to end users")]
	public IList<obj?>? DebugArgs{get;set;}
}


public static class ITypedErrExtn{
	public static TypedErr ToTypedErr(
		this ITypedErr z
	){
		var R = new TypedErr();
		R.Key = z.Key;
		R.Errors = z.Errors;
		return R;
	}
}
