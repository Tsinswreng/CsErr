namespace Tsinswreng.CsErr;

public static class ExtnIAnswer{
	[Doc($@"Adds a string error and sets {nameof(IAnswer<T>.Ok)} to false")]
	public static IAnswer<T> AddErr<T>(this IAnswer<T> z, str s){
		return z.AddErrStr(s);
	}

	[Doc($@"Adds an {nameof(Exception)} error and sets {nameof(IAnswer<T>.Ok)} to false")]
	public static IAnswer<T> AddErr<T>(this IAnswer<T> z, Exception e){
		return z.AddErrException(e);
	}
	[Doc($@"Adds a string error and sets {nameof(IAnswer<T>.Ok)} to false")]
	public static IAnswer<T> AddErrStr<T>(this IAnswer<T> z, str s){
		z.Ok = false;
		z.Errors??= new List<object?>();
		z.Errors.Add(s);
		return z;
	}

	[Doc($@"Adds an {nameof(Exception)} error and sets {nameof(IAnswer<T>.Ok)} to false")]
	public static IAnswer<T> AddErrException<T>(this IAnswer<T> z, Exception e){
		z.Ok = false;
		z.Errors??= new List<object?>();
		z.Errors.Add(e);
		return z;
	}
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

	[Doc($@"Returns {nameof(IAnswer<T>.Data)} or throws {nameof(AppErr)} if not ok")]
	public static T DataOrThrow<T>(this IAnswer<T> z){
		if(!z.Ok){
			throw z.ToAppErr();
		}
		return z.Data!;
	}

}
