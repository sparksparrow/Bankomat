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

		internal void ResetCard() => _bankomat.ResetCard();

		internal void SetCardNubmer(string nubmer) => _bankomat.SetCardNubmer(nubmer);

		internal void SetCardDate(string date) => _bankomat.SetCardDate(date);

		internal void SetCardCvc(string cvc) => _bankomat.SetCardCvc(cvc);

		internal bool IsEnoughMoney(int money) => _bankomat.IsEnoughMoney(money);

		internal bool WithdrawMoney(int money) => _bankomat.WithdrawMoney(money);

		internal string ShowCard() => _bankomat.ShowCard();
	}
}
