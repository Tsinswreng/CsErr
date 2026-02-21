namespace Tsinswreng.CsErr;

/// 返值包裝
/// 㕥代模式芝throw 業務異常+try-catch
/// 至于預料外ʹ異常、則猶用throw+try-catch、不用此㕥包㞢
[Doc($@"Return value wrapper interface, replaces throw-catch pattern for business errors")]
public partial interface IAnswer<T>:I_Errors{
	[Doc($@"The result data")]
	public T? Data { get; set; }
	[Doc($@"Whether the operation succeeded")]
	public bool Ok { get; set; }
	/// 可潙string, Exception等
	//public IList<obj?>? Errors { get; set; }
}
