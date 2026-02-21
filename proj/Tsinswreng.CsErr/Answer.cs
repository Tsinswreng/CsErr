namespace Tsinswreng.CsErr;

[Doc($@"Result wrapper struct implementing {nameof(IAnswer<T>)}")]
public partial struct Answer<T>()
	:IAnswer<T>
{
	[Doc($@"The result data if successful")]
	public T? Data{get;set;}
	[Doc($@"Whether the operation succeeded")]
	public bool Ok{get;set;}
	[Doc($@"List of errors if failed")]
	public IList<obj?>? Errors{get;set;} = new List<obj?>();
	[Doc($@"Status code or value")]
	public obj Status{get;set;} = 0;
	[Doc($@"Status type descriptor")]
	public str? StatusType{get;set;}

}
