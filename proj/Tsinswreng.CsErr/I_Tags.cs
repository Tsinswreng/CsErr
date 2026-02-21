namespace Tsinswreng.CsErr;

[Doc($@"Interface for tagged objects")]
public interface I_Tags{
	[Doc($@"Set of string tags for categorization")]
	public ISet<str> Tags{get;set;}
}
