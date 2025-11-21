namespace Tsinswreng.CsErr;
[Obsolete]
public partial class ErrArg:AppErr,IErr{
	public ErrArg(string? message, Exception? innerException = null)
		:base(message, innerException)
	{

	}
}
