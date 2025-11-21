namespace Tsinswreng.CsErr;
using Tsinswreng.CsCfg;

public interface IErrItem:ICfgItem, I_Tags{

}


public class ErrItem:CfgItem<nil>, IErrItem {
	public ISet<str> Tags{get;set;} = new HashSet<str>();
	public static IErrItem Mk(IErrItem? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = new ErrItem();
		R.Parent = Parent;
		R.RelaPathSegs = Path;
		if(Tags != null){
			R.Tags.UnionWith(Tags);
		}
		return R;
	}

	public static IErrItem MkB(IErrItem? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = Mk(Parent, Path, Tags);
		R.Tags.Add(ErrTags.BizErr);
		R.Tags.Add(ErrTags.Public);
		return R;
	}

	public static IErrItem MkS(IErrItem? Parent, IList<str> Path, IList<str>? Tags = null){
		var R = Mk(Parent, Path, Tags);
		R.Tags.Add(ErrTags.SysErr);
		//R.Tags.Add(ErrTags.Private);
		return R;
	}
}

public static class ExtnErrItem{
	public static AppErr ToErr(this IErrItem z, params obj?[] Args){
		return AppErr.Mk(z, Args);
	}
}


