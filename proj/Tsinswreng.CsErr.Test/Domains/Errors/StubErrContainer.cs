using Tsinswreng.CsErr;
namespace CsErr.Test.Domains.Errors;

/// 測試替身: 一個只裝錯誤、自身不是錯誤視圖的容器。
/// 用來驗證 I_ErrorsExtn 對「非視圖容器」的遞歸攤平、以及環路防護;
/// 被測的庫裏沒有這種純容器, 故在此自備一個。
public class StubErrContainer : I_Errors{
	public IList<obj?> Errors{get;set;} = new List<obj?>();
}