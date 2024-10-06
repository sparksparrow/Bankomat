namespace Bankomat
{
	internal class ContextState
	{
		private StateBase _stateBase;

		internal ContextState()
		{
			SetState(new InitialState());
		}

		internal void SetState(StateBase state)
		{
			ArgumentNullException.ThrowIfNull(state);			
			_stateBase = state;
			state.SetContext(this);
		}

		internal async Task ExecuteAsync() => await _stateBase.ExecuteAsync();

		internal async Task SetAndExecuteAsync(StateBase state)
		{
			SetState(state);
			await _stateBase.ExecuteAsync();
		}
	}
}
