namespace Bankomat
{
	internal class Bankomat
	{
		private ContextState _state;

		private Card _card = new Card();

		private int _moneyStorage = 10000;

		internal Bankomat()
		{
			_state = new ContextState(this);
		}

		internal void ResetCard() => _card = new Card();

		internal void SetCardNubmer(string nubmer) => _card.Number = nubmer;

		internal void SetCardDate(string date) => _card.Date = date;

		internal void SetCardCvc(string cvc) => _card.Cvc = cvc;

		internal bool IsEnoughMoney(int money) => _moneyStorage >= money;

		internal bool WithdrawMoney(int money)
		{
			if(IsEnoughMoney(money))
			{
				_moneyStorage -= money;

				return true;
			}

			return false;
		}

		internal string ShowCard() => _card.ToString();

		internal async Task StartAwait()
		{
			Launch();

			await _state.ExecuteAsync();
		}

		private void Launch()
		{

			Console.WriteLine("Запуск банкомата...");
			if(HealthCheck())
			{
				Console.WriteLine("Банкомата готов к работе");
			}
			else
			{
				throw new BankomatStartException("Ошибка модулей банкомата. Требуется технический специалист");
			}
		}

		private bool HealthCheck() => true;
	}
}
