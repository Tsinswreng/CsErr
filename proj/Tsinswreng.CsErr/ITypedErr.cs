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
	
	public static TSelf AddDebugArgs<TSelf>(
		this TSelf z, params obj?[] Args
	)where TSelf: class, ITypedErr{
		z.DebugArgs ??= new List<object?>();
		z.DebugArgs.AddRange(Args);
		return z;
	}
	
	
	[Doc(@$"把接口適配成實現類。
	因爲 實現類繼承了 Exception, 可直接throw
	")]
	public static TypedErr AsOrToTypedErr(
		this ITypedErr z
	){
		if(z is TypedErr t){
			return t;
		}
		
		var R = new TypedErr{
			Type = z.Type,
			Errors = z.Errors
		};
		return R;
	}
}

