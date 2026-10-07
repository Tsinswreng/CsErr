namespace Tsinswreng.CsErr;

using System.Text;
using Tsinswreng.CsKeyNode;


[Doc($@"Application error class, extends {nameof(Exception)} and implements {nameof(ITypedErr)}")]
public partial class TypedErr
	:Exception
	,IErr
	,ITypedErr
{
	public TypedErr(string? message, Exception? innerException = null)
		:base(message, innerException)
	{

	}
	public TypedErr(){}
	
	#region Impl-ITypedErr
	[Impl(typeof(ITypedErr))]
	public IErrNode Type{get;set;} = null!;
	
	[Impl(typeof(ITypedErr))]
	public IList<obj?>? DebugArgs { get; set; } = new List<obj?>();
	
	#endregion Impl-ITypedErr
	
	
	#region Impl-ITypedErrView
	
	[Impl(typeof(ITypedErrView))]
	public str Key {
		get=>Type.ToString()!;
	}
	
	[Impl(typeof(ITypedErrView))]
	public str PathSep{
		get=>Type.PathSep;
	}
	
	[Impl(typeof(ITypedErrView))]
	public IList<obj?>? Args { get; set; } = new List<obj?>();
	
	[Impl(typeof(ITypedErrView))]
	public ISet<str>? Tags{
		get => new HashSet<str>(Type?.Tags??new HashSet<str>());
	}
	
	#endregion Impl-ITypedErrView
	
	[Impl(typeof(I_Errors))]
	public IList<obj?> Errors { get; set; } = new List<obj?>();
	
	[Doc($@"Creates an {nameof(TypedErr)} with given type and arguments")]
	public static TypedErr Mk(IErrNode Key, params obj?[] Args){
		var R = new TypedErr();
		R.Type = Key;
		R.Args = Args;
		return R;
	}

	[Doc($@"Creates an {nameof(TypedErr)} from {nameof(ITypedErrView)}")]
	public static TypedErr FromView(
		ITypedErrView View
	){
		var PathSep = View.PathSep;
		var errNode = new ErrNode(){
			RelaPathSegs = View.Key?.Split(PathSep).ToList()??[],
			Tags = View.Tags,
			PathSep = PathSep??ErrNode.DfltPathSep
		};
		
		var R = new TypedErr();
		R.Type = errNode;
		R.Args = View.Args;
		return R;
	}
	[Doc($@"
	把 異常視圖列表適配 成 {nameof(TypedErr)}
	
	取{nameof(Views)}的第一項 作 返值的錯誤。
	餘者加入{nameof(TypedErr.Errors)}中
	
	#Throw[{nameof(Exception)}][Views is empty]
	
")]
	public static TypedErr FromViews(
		IList<ITypedErrView> Views
	){
		if(Views.Count == 0){
			throw new Exception("Views.Count == 0");
		}
		TypedErr R = null!;
		for(var i = 0; i < Views.Count; i++){
			if(i == 0){
				R = FromView(Views[i]);
			}else{
				R.AddErr(FromView(Views[i]));
			}
		}
		return R;
	}

	[Doc("")]
	public override str ToString(){
		var z = this;
		var R = new List<str> {
			z.Message,
			z?.Source ?? "",
			z?.StackTrace ?? "",
			z?.InnerException?.ToString() ?? ""
		};
		foreach(var Err in z?.Errors??[]){
			R.Add(Err?.ToString()??"");
		}
		return string.Join("\n", R);
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

// public class OptParseView{
// 	public str PathSep{get;set;} = "/";
// }
