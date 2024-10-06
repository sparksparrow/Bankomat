using System.Text.RegularExpressions;

namespace Bankomat
{
	internal class GetUserCardNumberState : StateBase
	{
		internal async override Task ExecuteAsync()
		{			
			try
			{
				var attempts = 3;
				Console.WriteLine("Введите номер карты");
				Console.ReadLine();

				do
				{
					var cardNumber = await Extensions.ReadLineWithTimeoutAsync();
					if (Regex.IsMatch(cardNumber, @"\d{4}\s\d{4}\s\d{4}\s\d{4}"))
					{
						await context!.SetAndExecuteAsync(new InitialState());

						return;
					}

					attempts--;
					Console.WriteLine("Неверное значение, повторите попытку");
				}
				while(attempts > 0);

				Console.WriteLine("Возврат к началу. Количество попыток закончилось");

				await context!.SetAndExecuteAsync(new InitialState());
				return;
			}
			catch(TimeoutException)
			{
				Console.WriteLine("Возврат к началу из-за неактивности пользователя");
				await context!.SetAndExecuteAsync(new InitialState());
				return;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Незарегистрированная ошибка");
				await context!.SetAndExecuteAsync(new InitialState());
				return;
			}
		}
	}
}
