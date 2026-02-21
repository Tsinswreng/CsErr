namespace Tsinswreng.CsErr;


/// 常用于 包於列表ⁿ返前端、及視圖ʸ示ᵣ錯。不含I_Errors, IErrItem
/// 勿蔿佢叶IAppSerializable、緣有自定義異常類 恐 同時繼承Exception及叶斯接口
[Doc($@"View interface for errors, used in API responses. Excludes {nameof(I_Errors)} and {nameof(IErrItem)} for serialization.")]
public interface IAppErrView:IErr,I_Tags{
	[Doc($@"Full path key from ")]
	public str? Key{get;set;}
	[Doc($@"Arguments for error message template")]
	public IList<obj?>? Args { get; set; }
}


[Doc($@"Default implementation of {nameof(IAppErrView)}")]
public class AppErrView:IAppErrView
	//, IAppSerializable
{
	public str? Key{get;set;}
	public IList<obj?>? Args { get; set; }
	public ISet<str> Tags { get; set; } = new HashSet<str>();
}
