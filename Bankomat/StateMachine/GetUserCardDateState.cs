using System.Globalization;

namespace Bankomat
{
	internal class GetUserCardDateState : StateBase
	{
		internal override async Task ExecuteAsync()
		{
			try
			{
				var attempts = 3;
				Console.WriteLine("Введите дату карты (формат месяц/год)");

				do
				{
					var cardDate = await Extensions.ReadLineWithTimeoutAsync();
					if (DateTime.TryParseExact(cardDate, "MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
					{
						context!.SetCardDate(date.ToString("MM/yy", CultureInfo.InvariantCulture));
						context!.SetState(new GetUserCardCvcState());

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
