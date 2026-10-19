namespace Tsinswreng.CsErr.Test;
using K = Tsinswreng.CsErr.IErrNode;
using static Tsinswreng.CsErr.ErrNode;
using Tsinswreng.CsCore;
public class Sample{
	/// 異常條目枚舉
	public static class ItemsErr{
		public static class Common{
			public static K _R = Mk(null, [nameof(Common)]);
			[Doc(@$"{nameof(Mk)} 不帶任何標籤 ＝ 公開可見的業務異常（默認語義）；要標成不可示人／系統異常時，往第三個參數傳自己的標籤")]
			public static K ArgErr = Mk(_R, [nameof(ArgErr)]);
			public static K UnknownErr = Mk(_R, [nameof(UnknownErr)]);
		}
		/// 內部類按領域劃分
		public static class User{
			public static K _R = Mk(null, [nameof(User)]);
			public static K PasswordNotMatch = Mk(_R, [nameof(PasswordNotMatch)]);
			public static K InvalidToken = Mk(_R, [nameof(InvalidToken)]);
			public static K TokenExpired = Mk(_R, [nameof(TokenExpired)]);
		}
		public class Word{
			public static K _R = Mk(null, [nameof(Word)]);
			[Doc(@$"約定用__作參數佔位符")]
			public static K __And__IsNotSameUserWord = Mk(_R, [nameof(__And__IsNotSameUserWord)]);
		}
	}

	public void ExsampleThrow(){
		//轉 AppErr 示例。AppErr是Exception的子類，所以可以直接用throw來拋出。
		TypedErr e1 = ItemsErr.Common.ArgErr.ToErr();
		//帶參數轉 AppErr示例
		throw ItemsErr.Word.__And__IsNotSameUserWord.ToErr("Word1", "Word2")
		.AddDebugArgs("AddDebugArgs 可選、可加任意多個 任意object")
		;
	}
}





