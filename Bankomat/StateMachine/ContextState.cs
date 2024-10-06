namespace Bankomat
{
	internal class ContextState
	{
		private StateBase _stateBase;

		private Bankomat _bankomat;

		internal ContextState(Bankomat bankomat)
		{
			_bankomat = bankomat;
			SetState(new InitialState());			
		}

		internal async Task SetAndExecuteAsync(StateBase state)
		{
			SetState(state);
			await _stateBase.ExecuteAsync();
		}

		internal void SetState(StateBase state)
		{
			ArgumentNullException.ThrowIfNull(state);
			_stateBase = state;
			state.SetContext(this);
		}

		internal async Task ExecuteAsync() => await _stateBase.ExecuteAsync();
	}
}
