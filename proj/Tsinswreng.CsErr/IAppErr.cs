namespace Tsinswreng.CsErr;


/// 應用基異常接口
[Doc($@"Base interface for application errors")]
public partial interface IAppErr
	:IAppErrView
	,I_Errors//內ʹ錯
{
	[Doc($@"Error type item for classification and key generation")]
	public IErrNode? Type{get;set;}
	/// 㕥置 未ToString之原始對象、用于除錯
	[Doc($@"Raw objects for debugging, not shown to end users")]
	public IList<obj?>? DebugArgs{get;set;}
}


public static class ExtnIAppErr{
	public static AppErr ToAppErr(
		this IAppErr z
	){
		var R = new AppErr();
		R.Key = z.Key;
		R.Errors = z.Errors;
		return R;
	}
}
