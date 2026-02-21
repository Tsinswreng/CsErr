namespace Tsinswreng.CsErr.Test;
using K = Tsinswreng.CsErr.IErrItem;
using static Tsinswreng.CsErr.ErrItem;
using Tsinswreng.CsCore;
public class Sample{
	/// 異常條目枚舉
	public static class ItemsErr{
		public static class Common{
			public static K _R = Mk(null, [nameof(Common)]);
			[Doc(@$"{nameof(MkB)} 自動加上標籤 {nameof(ErrTags.BizErr)} 表示業務異常
			若需添加其他標籤則往{nameof(Mk)}第三個參數傳字符串列表")]
			public static K ArgErr = MkB(_R, [nameof(ArgErr)]);
			public static K UnknownErr = MkB(_R, [nameof(UnknownErr)]);
		}
		/// 內部類按領域劃分
		public static class User{
			public static K _R = Mk(null, [nameof(User)]);
			public static K PasswordNotMatch = MkB(_R, [nameof(PasswordNotMatch)]);
			public static K InvalidToken = MkB(_R, [nameof(InvalidToken)]);
			public static K TokenExpired = MkB(_R, [nameof(TokenExpired)]);
		}
		public class Word{
			public static K _R = Mk(null, [nameof(Word)]);
			[Doc(@$"約定用__作參數佔位符")]
			public static K __And__IsNotSameUserWord = MkB(_R, [nameof(__And__IsNotSameUserWord)]);
		}
	}

	public void ExsampleThrow(){
		//轉 AppErr 示例。AppErr是Exception的子類，所以可以直接用throw來拋出。
		AppErr e1 = ItemsErr.Common.ArgErr.ToErr();
		//帶參數轉 AppErr示例
		throw ItemsErr.Word.__And__IsNotSameUserWord.ToErr("Word1", "Word2");
	}
}




