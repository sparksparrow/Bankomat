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

				do
				{
					var cardNumber = await Extensions.ReadLineWithTimeoutAsync();
					if (Regex.IsMatch(cardNumber, @"\d{4}\s\d{4}\s\d{4}\s\d{4}"))
					{
						context!.SetCardNubmer(cardNumber);
						context!.SetState(new GetUserCardDateState());

						return;
					}

					attempts--;
					Console.WriteLine("Неверное значение, повторите попытку");
				}
				while(attempts > 0);

				Console.WriteLine("Возврат к началу. Количество попыток закончилось");
			}
			catch(TimeoutException)
			{
				Console.WriteLine("Возврат к началу из-за неактивности пользователя");
			}
			catch (Exception)
			{
				Console.WriteLine("Незарегистрированная ошибка");
			}

			context!.SetState(new InitialState());
			return;
		}
	}
}
