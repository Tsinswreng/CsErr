#if false
namespace Tsinswreng.CsErr;

using K = IErrNode;
using static ErrNode;


public static class KeysErr{
	public static class Common{
		public static K _R = Mk(null, [nameof(Common)]);
		public static K ArgErr = Mk(_R, [nameof(ArgErr)]);
		public static K UnknownErr = Mk(_R, [nameof(UnknownErr)]);
	}
	public class User{
		public static K _R = Mk(null, [nameof(User)]);
		public static K UserNotExist = Mk(_R, [nameof(UserNotExist)]);
		public static K UserAlreadyExist = Mk(_R, [nameof(UserAlreadyExist)]);
		public static K PasswordNotMatch = Mk(_R, [nameof(PasswordNotMatch)]);
		public static K InvalidToken = Mk(_R, [nameof(InvalidToken)]);
		public static K TokenExpired = Mk(_R, [nameof(TokenExpired)]);
		public static K AuthenticationFailed = Mk(_R, [nameof(AuthenticationFailed)]);
	}
	public class Word{
		public static K _R = Mk(null, [nameof(Word)]);
		public static K LoadWordListFailed = Mk(_R, [nameof(LoadWordListFailed)]);
		public static K SaveWordListFailed = Mk(_R, [nameof(SaveWordListFailed)]);
		public static K __NotBelongToLang__ = Mk(_R, [nameof(__NotBelongToLang__)]);
		public static K __And__IsNotSameUserWord = Mk(_R, [nameof(__And__IsNotSameUserWord)]);
		public static K BackgroundImageServiceFailedToInit = Mk(_R, [nameof(BackgroundImageServiceFailedToInit)]);
	}
	public class Sync{
		public static K _R = Mk(null, [nameof(Sync)]);
		public static K BatchGetByIdCountMismatch = Mk(_R, [nameof(BatchGetByIdCountMismatch)]);
		public static K SyncShouldUseSameId = Mk(_R, [nameof(SyncShouldUseSameId)]);
	}
}

#endif

