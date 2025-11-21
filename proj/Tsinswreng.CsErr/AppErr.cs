namespace Tsinswreng.CsErr;

using System.Text;
using Tsinswreng.CsCfg;


public partial class AppErr
	:Exception
	,IErr
	,IAppErr
{
	public AppErr(string? message, Exception? innerException = null)
		:base(message, innerException)
	{

	}
	public AppErr(){}
	public IErrItem? Type{get;set;}
	public str? Key { get{
		return Type?.GetFullPath();
	} set{throw new NotImplementedException();} }
	public IList<obj?> Errors { get; set; } = new List<obj?>();
	public ISet<str> Tags{get{
		return Type?.Tags ?? new HashSet<str>();
	}set{throw new NotImplementedException();}}
	public IList<obj?>? Args { get; set; } = new List<obj?>();
	public IList<obj?>? DebugArgs { get; set; } = new List<obj?>();

	public static AppErr Mk(IErrItem Key, params obj?[] Args){
		var R = new AppErr();
		R.Type = Key;
		R.Args = Args;
		return R;
	}

	public static AppErr FromView(IAppErrView View){
		var ErrItem = new ErrItem();
		ErrItem.RelaPathSegs = View.Key?.Split(CfgItem<obj>.PathSep).ToList()??[];
		ErrItem.Tags = View.Tags;

		var R = new AppErr();
		R.Type = ErrItem;
		R.Args = View.Args;
		return R;
	}
	public static AppErr FromViews(IList<IAppErrView> Views){
		if(Views.Count == 0){
			throw new Exception("Views.Count == 0");
		}
		AppErr R = null!;
		for(var i = 0; i < Views.Count; i++){
			if(i == 0){
				R = FromView(Views[i]);
			}else{
				R.AddErr(FromView(Views[i]));
			}
		}
		return R;
	}

	public override str ToString(){
		return FillTemplate(Key??"", Args??[]);
	}

	/// <summary>
	/// 把 args 依序填入模板中連續的「__」位置。
	/// 例: FillTemplate("ParseErrorAtFile__Line__Col__", "MyFile", 0, 1)
	///     → "ParseErrorAtFile[MyFile]Line[0]Col[1]"
	/// </summary>
	static string FillTemplate(string template, IList<object?> args){
		if (string.IsNullOrEmpty(template)) return string.Empty;
		if (args == null || args.Count == 0) return template;

		var parts = template.Split(new[] {"__"}, StringSplitOptions.None);
		var sb = new StringBuilder();

		int i = 0;
		for (; i < args.Count && i < parts.Length - 1; i++)
		{
			sb.Append(parts[i]).Append('[').Append(args[i]).Append(']');
		}

		// 把最後一段（或剩餘段）拼回去
		if (i < parts.Length)
			sb.Append(parts[i]);

		return sb.ToString();
	}

}
