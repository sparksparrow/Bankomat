namespace Bankomat
{
	internal class Bankomat
	{
		private ContextState _state = new ContextState();

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
