namespace Tsinswreng.CsErr;


/// 常用于 包於列表ⁿ返前端、及視圖ʸ示ᵣ錯。不含I_Errors, IErrItem
/// 勿蔿佢叶ISerializable、緣有自定義異常類 恐 同時繼承Exception及叶斯接口
[Doc($@"
爲序列化場景考慮
View interface for errors, used in API responses.
Excludes {nameof(I_Errors)} and {nameof(IErrNode)} for serialization.")]
public interface ITypedErrView:IErr{
	[Doc($@"Full path key from ")]
	public str? Key{get;}
	[Doc($@"Arguments for error message template")]
	public IList<obj?>? Args { get; }
	[Doc($@"Set of string tags for categorization")]
	public ISet<str> Tags{get;}
}


[Doc($@"Default implementation of {nameof(ITypedErrView)}")]
public class TypedErrView:ITypedErrView
{
	[Impl(typeof(ITypedErrView))]
	public str? Key{get;set;}
	
	[Impl(typeof(ITypedErrView))]
	public IList<obj?>? Args { get; set; }
	
	[Impl(typeof(ITypedErrView))]
	public ISet<str> Tags { get; set; } = new HashSet<str>();
}
