namespace Tsinswreng.CsErr;

[Doc($@"Container for error collection")]
public partial interface I_Errors{
	[Doc($@"List of errors, can be string, Exception, etc")]
	public IList<obj?> Errors{get;set;}
}

public static class I_ErrorsExtn{
	[Doc($@"Adds an error to {nameof(I_Errors.Errors)} and returns self for fluent chaining")]
	public static TSelf AddErr<TSelf>(
		this TSelf z, obj Err
	)where TSelf : class, I_Errors
	{
		z.Errors ??= new List<object?>();
		z.Errors.Add(Err);
		return z;
	}

	[Doc($@"
	把錯誤列表中所有能適配成{nameof(ITypedErrView)}的
	都適配 然後組成扁平的 一唯的 錯誤視圖列表
	")]
	public static IList<ITypedErrView> ExtractErrViews(this I_Errors z){
		var R = new List<ITypedErrView>();
		foreach(var err in z.Errors){
			if(err is ITypedErrView View){
				R.Add(View);
			}
			if(err is I_Errors Errs){
				R.AddRange(Errs.ExtractErrViews());
			}
		}
		return R;
	}

	
	[Doc(@$"
	若z無內容則拋異常
	取首元素 試圖轉換爲 {nameof(ITypedErrView)}
	若失敗亦拋。
	若成功 則使首元素之Type作返值之類型
	z.Errors中之餘者即照加入返值之Errors
	")]
	public static TypedErr ToTypedErr(this I_Errors z, OptParseView Opt){

	}
}
