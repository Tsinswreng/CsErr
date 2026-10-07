namespace Tsinswreng.CsErr.Results;

public static class IAnswerExtn{
	[Doc($@"Sets {nameof(IAnswer<T>.Data)} and {nameof(IAnswer<T>.Ok)} to true")]
	public static IAnswer<T> OkWith<T> (this IAnswer<T> z, T data = default!){
		z.Data = data;
		z.Ok = true;
		return z;
	}

	[Doc($@"Converts errors to list of strings")]
	public static IList<str> ErrsToStrs<T>(this IAnswer<T> z){
		z.Errors??= new List<object?>();
		return z.Errors.Select(e =>{
			return e?.ToString()??"";
		}).ToList();
	}

	[Doc($@"Returns {nameof(IAnswer<T>.Data)} or throws {nameof(TypedErr)} if not ok")]
	public static T DataOrThrow<T>(this IAnswer<T> z){
		if(!z.Ok){
			throw z.ToTypedErr();
		}
		return z.Data!;
	}

}
