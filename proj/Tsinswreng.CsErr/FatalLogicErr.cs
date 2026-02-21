namespace Tsinswreng.CsErr;

/// 預料外ʹ理則謬
[Doc($@"Unexpected logic error that should never occur in normal operation")]
public partial class FatalLogicErr : Exception{
	public FatalLogicErr(str? msg):base(msg){

	}
}
