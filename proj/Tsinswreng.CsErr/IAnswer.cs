namespace Tsinswreng.CsErr;

/// 返值包裝
/// 㕥代模式芝throw 業務異常+try-catch
/// 至于預料外ʹ異常、則猶用throw+try-catch、不用此㕥包㞢
[Doc($$"""
Return value wrapper interface.Result/Response Pattern
#Descr[
compare with throw-try-catch pattern:
	#H[situation suitable to use `IAnswer<>`][
		+ when error occurs, no need to break the control
	]
]
#Examples([
```cs
IAnswer<T> fn(){
	var R = new Answer<T>(); // default of R.Ok is false;
	try{
		if(someFailedCond){
			return R.AddErr("Some Reason string");
		}
		T t = new();
		return R.OkWith(t);
	}catch(Exception e){
		return R.AddErr(e);
	}
}
```
])
""")]
public partial interface IAnswer<T>:I_Errors{
	[Doc($@"The result data")]
	public T? Data { get; set; }
	[Doc($@"Whether the operation succeeded")]
	public bool Ok { get; set; }
	/// 可潙string, Exception等
	//public IList<obj?>? Errors { get; set; }
}
