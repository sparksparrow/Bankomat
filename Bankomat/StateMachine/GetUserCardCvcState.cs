using System.Text.RegularExpressions;

namespace Bankomat
{
	internal class GetUserCardCvcState : StateBase
	{
		internal override async Task ExecuteAsync()
		{
			try
			{
				var attempts = 3;
				Console.WriteLine("Введите cvc карты");

				do
				{
					var cardCvc = await Extensions.ReadLineWithTimeoutAsync();
					if (Regex.IsMatch(cardCvc, @"\d{3}"))
					{
						context!.SetCardCvc(cardCvc);
						context!.SetState(new StartWithdrawOperationState());

						return;
					}

					attempts--;
					Console.WriteLine("Неверное значение, повторите попытку");
				}
				while (attempts > 0);

				Console.WriteLine("Возврат к началу. Количество попыток закончилось");
			}
			catch (TimeoutException)
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
