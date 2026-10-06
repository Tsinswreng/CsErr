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

	[Doc($@"Flattens nested errors into a list of {nameof(ITypedErrView)}")]
	public static IList<ITypedErrView> ToErrViews(this I_Errors z){
		var R = new List<ITypedErrView>();
		foreach(var err in z.Errors){
			if(err is ITypedErrView View){
				R.Add(View);
			}
			if(err is I_Errors Errs){
				R.AddRange(Errs.ToErrViews());
			}else{//字符串等
				//TswgNote
			}
		}
		return R;
	}

	[Doc($@"Converts errors to an {nameof(TypedErr)} instance")]
	public static TypedErr ToTypedErr(this I_Errors z, OptParseView Opt){
		return TypedErr.FromViews(z.ToErrViews(), Opt);
	}
}
