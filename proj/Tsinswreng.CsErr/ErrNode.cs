namespace Tsinswreng.CsErr;
using Tsinswreng.CsKeyNode;

[Doc($@"用作異常種類的標識")]
public interface IErrNode:IKeyNode{
	[Doc($@"Set of string tags for categorization")]
	public ISet<str>? Tags{get;set;}
}



public class ErrNode:KeyNode, IErrNode {
	public const str DfltPathSep = "/";
	public ISet<str>? Tags{get;set;} = new HashSet<str>();
	
	[Doc(@$"創建一個異常標識節點
	#Prm[{nameof(Parent)}][父節點]
	#Prm[{nameof(Path)}][自己的路徑]
	#Prm[{nameof(Tags)}][異常標籤]
	")]
	public static IErrNode Mk(IErrNode? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = new ErrNode{
			Parent = Parent,
			RelaPathSegs = Path,
		};
		
		if(Tags != null){
			if(R.Tags is null){
				R.Tags = new HashSet<str>(Tags);
			}
			R.Tags.UnionWith(Tags);
		}
		return R;
	}

	//TswgNote 是否應該在此庫中提供?
	[Doc($@"
	#See[{nameof(Mk)}]
	在此基礎上增加
	- {nameof(ErrTags.BizErr)}
	- {nameof(ErrTags.Public)}
	兩個標籤
	")]
	public static IErrNode MkB(IErrNode? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = Mk(Parent, Path, Tags);
		R.Tags??=new HashSet<str>();
		R.Tags.Add(ErrTags.BizErr);
		R.Tags.Add(ErrTags.Public);
		return R;
	}

	[Doc($@"Creates a system error item (tagged with {nameof(ErrTags.SysErr)})")]
	public static IErrNode MkS(IErrNode? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = Mk(Parent, Path, Tags);
		R.Tags??=new HashSet<str>();
		R.Tags.Add(ErrTags.SysErr);
		//R.Tags.Add(ErrTags.Private);
		return R;
	}
}

public static class ErrNodeExtn{
	[Doc($"""
	Converts {nameof(IErrNode)} to {nameof(TypedErr)} with arguments
	方便直接拋。
	#let ToErr = {nameof(ToErr)}
	#Eg[
	```cs
	throw KeysErr.User.PasswordNotMatch.ToErr()
	```
	]
	#Eg[
	```cs
	throw KeysErr.Word.__NotBelongToLang__.ToErr("ことば", "zh-CN")
	```
	]
	""")]
	public static TypedErr ToErr(this IErrNode z, params obj?[] Args){
		return TypedErr.Mk(z, Args);
	}
}
