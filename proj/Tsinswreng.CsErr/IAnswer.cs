namespace Tsinswreng.CsErr;
/// 返值包裝
/// 㕥代模式芝throw 業務異常+try-catch
/// 至于預料外ʹ異常、則猶用throw+try-catch、不用此㕥包㞢
public partial interface IAnswer<T>:I_Errors{
	public T? Data { get; set; }
	public bool Ok { get; set; }
	/// 可潙string, Exception等
	//public IList<obj?>? Errors { get; set; }
}
