namespace Tsinswreng.CsErr;

using System.Text;
using Tsinswreng.CsCfg;


[Doc($@"Application error class, extends {nameof(Exception)} and implements {nameof(IAppErr)}")]
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
	[Doc($@"Error type item for classification")]
	public IErrItem? Type{get;set;}
	[Doc($@"Full path key derived from {nameof(Type)}")]
	public str? Key { get{
		return Type?.GetFullPath();
	} set{throw new NotImplementedException();} }
	[Doc($@"Nested errors")]
	public IList<obj?> Errors { get; set; } = new List<obj?>();
	[Doc($@"Tags inherited from {nameof(Type)}")]
	public ISet<str> Tags{get{
		return Type?.Tags ?? new HashSet<str>();
	}set{throw new NotImplementedException();}}
	[Doc($@"Arguments for error message template")]
	public IList<obj?>? Args { get; set; } = new List<obj?>();
	[Doc($@"Raw objects for debugging")]
	public IList<obj?>? DebugArgs { get; set; } = new List<obj?>();

	[Doc($@"Creates an {nameof(AppErr)} with given type and arguments")]
	public static AppErr Mk(IErrItem Key, params obj?[] Args){
		var R = new AppErr();
		R.Type = Key;
		R.Args = Args;
		return R;
	}

	[Doc($@"Creates an {nameof(AppErr)} from {nameof(IAppErrView)}")]
	public static AppErr FromView(IAppErrView View){
		var ErrItem = new ErrItem();
		ErrItem.RelaPathSegs = View.Key?.Split(CfgItem<obj>.PathSep).ToList()??[];
		ErrItem.Tags = View.Tags;

		var R = new AppErr();
		R.Type = ErrItem;
		R.Args = View.Args;
		return R;
	}
	[Doc($@"
#Sum[Creates an {nameof(AppErr)} from a list of {nameof(IAppErrView)}]
#Throw[{nameof(Exception)}][Views is empty]
")]
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

	
	/// 把 args 依序填入模板中連續的「__」位置。
	/// 例: FillTemplate("ParseErrorAtFile__Line__Col__", "MyFile", 0, 1)
	///     → "ParseErrorAtFile[MyFile]Line[0]Col[1]"
	
	[Doc($@"Fills template placeholders `__` with args. E.g. `ParseErrorAtFile__Line__` with args [`MyFile`, 0] becomes `ParseErrorAtFile[MyFile]Line[0]`")]
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
