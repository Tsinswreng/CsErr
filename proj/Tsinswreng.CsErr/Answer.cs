namespace Tsinswreng.CsErr;

[Doc($@"Result wrapper struct implementing {nameof(IAnswer<T>)}")]
public partial struct Answer<T>()
	:IAnswer<T>
{
	[Doc($@"The result data if successful")]
	public T? Data{get;set;}
	[Doc($@"Whether the operation succeeded")]
	public bool Ok{get;set;}
	[Doc($@"List of errors if failed
	item can be any including Exception, string, etc
	")]
	public IList<obj?> Errors{get;set;} = new List<obj?>();

}
