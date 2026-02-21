namespace Tsinswreng.CsErr;
using Tsinswreng.CsCore;

public static class ExtnAppErr{
	[Doc($@"Adds debug arguments for troubleshooting, not shown to end users")]
	public static TSelf AddDebugArgs<TSelf>(
		this TSelf z, params obj?[] Args
	)where TSelf: class, IAppErr{
		z.DebugArgs ??= new List<object?>();
		z.DebugArgs.AddRange(Args);
		return z;
	}
}
