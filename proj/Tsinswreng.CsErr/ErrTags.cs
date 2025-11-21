namespace Tsinswreng.CsErr;
public static class ErrTags{
	/// <summary>
	/// 業務異常  如參數不合法
	/// </summary>
	public static str BizErr = nameof(BizErr);
	/// <summary>
	/// 系統異常 如 數據庫異常
	/// </summary>
	public static str SysErr = nameof(SysErr);
	/// <summary>
	/// 公開、可示予用戶
	/// </summary>
	public static str Public = nameof(Public);
	/// <summary>
	/// 叵示予用戶
	/// </summary>
	public static str Private = nameof(Private);
}

