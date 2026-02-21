namespace Tsinswreng.CsErr;

[Doc($@"Standard error tags for categorization")]
public static class ErrTags{
	/// 業務異常  如參數不合法
	[Doc($@"Business error, e.g. invalid parameters")]
	public static str BizErr = nameof(BizErr);
	/// 系統異常 如 數據庫異常
	[Doc($@"System error, e.g. database exception")]
	public static str SysErr = nameof(SysErr);
	/// 公開、可示予用戶
	[Doc($@"Public, can be shown to end users")]
	public static str Public = nameof(Public);
	/// 叵示予用戶
	[Doc($@"Private, should not be shown to end users")]
	public static str Private = nameof(Private);
}
