namespace Tsinswreng.CsErr;


/// <summary>
/// 常用于 包于列表ⁿ返前端、及視圖ʸ示ᵣ錯。不含I_Errors, IErrItem
/// 勿蔿佢叶IAppSerializable、緣有自定義異常類 恐 同時繼承Exception及叶斯接口
/// </summary>
public interface IAppErrView:IErr,I_Tags{
	/// <summary>
	/// 即 ErrItem.GetFullPath()
	/// </summary>
	public str? Key{get;set;}
	public IList<obj?>? Args { get; set; }
}


public class AppErrView:IAppErrView
	//, IAppSerializable
{
	public str? Key{get;set;}
	public IList<obj?>? Args { get; set; }
	public ISet<str> Tags { get; set; } = new HashSet<str>();
}
