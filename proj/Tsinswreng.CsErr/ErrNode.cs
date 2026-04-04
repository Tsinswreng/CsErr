namespace Tsinswreng.CsErr;
using Tsinswreng.CsCfg;

[Doc($@"Error item interface")]
public interface IErrNode:ICfgNode, I_Tags{

}


[Doc($@"Error item for classification and key generation")]
public class ErrNode:CfgNode<nil>, IErrNode {
	[Doc($@"Tags for categorization")]
	public ISet<str> Tags{get;set;} = new HashSet<str>();
	[Doc($@"Creates an {nameof(IErrNode)} with parent, path, and optional tags")]
	public static IErrNode Mk(IErrNode? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = new ErrNode();
		R.Parent = Parent;
		R.RelaPathSegs = Path;
		if(Tags != null){
			R.Tags.UnionWith(Tags);
		}
		return R;
	}

	[Doc($@"Creates a business error item (tagged with {nameof(ErrTags.BizErr)} and {nameof(ErrTags.Public)})")]
	public static IErrNode MkB(IErrNode? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = Mk(Parent, Path, Tags);
		R.Tags.Add(ErrTags.BizErr);
		R.Tags.Add(ErrTags.Public);
		return R;
	}

	[Doc($@"Creates a system error item (tagged with {nameof(ErrTags.SysErr)})")]
	public static IErrNode MkS(IErrNode? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = Mk(Parent, Path, Tags);
		R.Tags.Add(ErrTags.SysErr);
		//R.Tags.Add(ErrTags.Private);
		return R;
	}
}

public static class ExtnErrItem{
	[Doc($@"Converts {nameof(IErrNode)} to {nameof(AppErr)} with arguments")]
	public static AppErr ToErr(this IErrNode z, params obj?[] Args){
		return AppErr.Mk(z, Args);
	}
}
