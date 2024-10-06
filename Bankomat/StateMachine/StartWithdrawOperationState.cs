namespace Bankomat
{
	internal class StartWithdrawOperationState : StateBase
	{
		internal override async Task ExecuteAsync()
		{
			try
			{
				var attempts = 3;
				Console.WriteLine(context!.ShowCard());
				Console.WriteLine("Введите количество денег для списания");

				do
				{
					var money = await Extensions.ReadLineWithTimeoutAsync();
					if (int.TryParse(money, out var sumMoney))
					{
						(bool can, Dictionary<BanknoteType, int> _) = context!.CanWithdraw(sumMoney);
						if (can)
						{
							if (context!.WithdrawMoney(sumMoney))
							{
								Console.WriteLine($"Успешно списано {sumMoney} рублей");

								context!.SetState(new InitialState());

								return;
							}
						}
						else
						{
							Console.WriteLine("Количество денег в банкомате недостаточно или введена неверная сумма");

							continue;
						}
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
