namespace Tsinswreng.CsErr;

using System.Text.Json;


/// <summary>
/// 除IWebAns<obj>外 他者勿用、緣不支持AOT下json序列化
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IWebAns<T>{
	public T? Data{get;set;}
	public IList<IAppErrView>? Errors{get;set;}
}

public class WebAns<T>:IWebAns<T>{
	public T? Data{get;set;}
	public IList<IAppErrView>? Errors{get;set;}
}

public class WebAns:WebAns<obj>
//, IAppSerializable
{
	public static IWebAns<obj> Mk(obj? Data, IList<IAppErrView>? Errors=null){
		return new WebAns<obj>(){
			Data = Data,
			Errors = Errors,
		};
	}

#if false
	public static IWebAns<T> Deserialize<T>(str Json){
		using var doc = JsonDocument.Parse(Json);
		var root = doc.RootElement;

		// 1. 反序列化 Errors（始终用源生成器）
		IList<AppErrView>? errors = null;
		if (root.TryGetProperty(nameof(Errors), out var errEl) && errEl.ValueKind != JsonValueKind.Null){
			errors = errEl.Deserialize(AppJsonCtx.Default.IListAppErrView);
		}

		// 2. 反序列化 Data（按需用默认源生成器或调用方提供的选项）
		T? data = default;
		if (root.TryGetProperty(nameof(Data), out var dataEl) && dataEl.ValueKind != JsonValueKind.Null){
			//“AppJsonCtx”未包含“GetRequiredTypeInfo”的定义，并且找不到可接受第一个“AppJsonCtx”类型参数的可访问扩展方法“GetRequiredTypeInfo”(是否缺少 using 指令或程序集引用?)CS1061
			data = (T?)dataEl.Deserialize(AppJsonCtx.Default.GetTypeInfo(typeof(T))!);
		}

		var R = new WebAns<T>();
		R.Data = data;
		R.Errors = errors?.Select(x=>(IAppErrView)x).ToList();
		return R;
	}
#endif
}

public static class ExtnWebAns{
	// public static str ToJson<T>(this IWebAns<T> z){
	// }
	public static T? DataOrThrow<T>(this IWebAns<T> z){
		if(z.Errors is not null && z.Errors.Count > 0){
			throw AppErr.FromViews(z.Errors).ToAppErr();
		}
		return z.Data;
	}

}
