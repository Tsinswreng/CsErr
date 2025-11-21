namespace Tsinswreng.CsErr;


/// <summary>
/// 應用基異常接口
/// </summary>
public partial interface IAppErr
	:IAppErrView
	,I_Errors//內ʹ錯
{
	public IErrItem? Type{get;set;}
	/// <summary>
	/// 㕥置 未ToString之原始對象、用于除錯
	/// </summary>
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
