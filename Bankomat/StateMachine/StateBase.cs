namespace Bankomat
{
	internal abstract class StateBase
	{
		protected ContextState? context;		

		internal void SetContext(ContextState context)
		{
			ArgumentNullException.ThrowIfNull(context);
			this.context = context;
		}		

		internal abstract Task ExecuteAsync();
	}
}
