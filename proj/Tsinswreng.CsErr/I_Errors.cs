namespace Tsinswreng.CsErr;

public partial interface I_Errors{
	/// <summary>
	/// 可潙string, Exception等i
	/// </summary>
	public IList<obj?> Errors{get;set;}
}

public static class ExtnI_Errors{
	public static TSelf AddErr<TSelf>(
		this TSelf z, obj Err
	)where TSelf : class, I_Errors
	{
		z.Errors ??= new List<object?>();
		z.Errors.Add(Err);
		return z;
	}

	public static IList<IAppErrView> ToErrViews(this I_Errors z){
		var R = new List<IAppErrView>();
		foreach(var err in z.Errors){
			if(err is IAppErrView View){
				R.Add(View);
			}
			if(err is I_Errors Errs){
				R.AddRange(Errs.ToErrViews());
			}
		}
		return R;
	}

	public static AppErr ToAppErr(this I_Errors z){
		return AppErr.FromViews(z.ToErrViews());
	}
}
