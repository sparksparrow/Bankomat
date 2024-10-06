namespace Bankomat
{
	internal class Bankomat
	{
		private ContextState _state;

		private Card card;

		private int MoneyStorage = 10000;

		internal Bankomat()
		{
			_state = new ContextState(this);
		}

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
