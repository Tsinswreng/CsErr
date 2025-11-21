namespace Tsinswreng.CsErr;

public partial struct Answer<T>()
	:IAnswer<T>
{
	public T? Data{get;set;}
	public bool Ok{get;set;}
	public IList<obj?>? Errors{get;set;} = new List<obj?>();
	public obj Status{get;set;} = 0;
	public str? StatusType{get;set;}

}
