namespace Tsinswreng.CsErr;

[Doc($@"Container for error collection")]
public partial interface I_Errors{
	[Doc($@"List of errors, can be string, Exception, etc")]
	public IList<obj?> Errors{get;set;}
}

public static class ExtnI_Errors{
	[Doc($@"Adds an error to {nameof(I_Errors.Errors)} and returns self for fluent chaining")]
	public static TSelf AddErr<TSelf>(
		this TSelf z, obj Err
	)where TSelf : class, I_Errors
	{
		z.Errors ??= new List<object?>();
		z.Errors.Add(Err);
		return z;
	}

	[Doc($@"Flattens nested errors into a list of {nameof(IAppErrView)}")]
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

	[Doc($@"Converts errors to an {nameof(AppErr)} instance")]
	public static AppErr ToAppErr(this I_Errors z){
		return AppErr.FromViews(z.ToErrViews());
	}
}
