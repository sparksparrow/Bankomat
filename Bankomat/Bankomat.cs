namespace Bankomat
{
	internal class Bankomat
	{
		private ContextState _context;

		private Card _card = new Card();

		private Dictionary<BanknoteType, int> _moneyStorage = new()
		{
			{ BanknoteType.Hundred, 1},
			{ BanknoteType.FiveHundred, 10},
			{ BanknoteType.Thousand, 6},
			{ BanknoteType.TwoThousand, 4},
			{ BanknoteType.FiveThousand, 3},
		};

		internal Bankomat()
		{
			_context = new ContextState(this);
		}

		internal void ResetCard() => _card = new Card();

		internal void SetCardNubmer(string nubmer) => _card.Number = nubmer;

		internal void SetCardDate(string date) => _card.Date = date;

		internal void SetCardCvc(string cvc) => _card.Cvc = cvc;

		internal (bool, Dictionary<BanknoteType, int>) CanWithdraw(int money)
		{
			if (money % 100 != 0)
			{
				return (false, new Dictionary<BanknoteType, int>());
			}

			var banknotes = _moneyStorage.Select(kv => kv.Key).Reverse().ToList();
			var deltaDict = banknotes.ToDictionary(key => key, _ => 0);
			var remainder = money;
			foreach (var note in banknotes)
			{
				var countBanknotes = _moneyStorage[note];
				for (int i = 0; i < countBanknotes; i++)
				{
					var delta = remainder - (int)note;
					if (delta < 0)
					{
						break;
					}

					remainder -= (int)note;
					deltaDict[note] += 1;
				}

				if (remainder == 0)
				{
					return (true, deltaDict);
				}
			}

			return (false, new Dictionary<BanknoteType, int>());
		}

		internal bool WithdrawMoney(int money)
		{
			(bool can, Dictionary<BanknoteType, int> delta) = CanWithdraw(money);
			if (can)
			{
				foreach(var deltaValue in delta)
				{
					_moneyStorage[deltaValue.Key] -= deltaValue.Value;
				}

				return true;
			}

			return false;
		}

		internal string ShowCard() => _card.ToString();

		internal async Task StartAwait()
		{
			Launch();
			while (true)
			{
				await _context.ExecuteAsync();
			}
		}

		private void Launch()
		{

			Console.WriteLine("Запуск банкомата...");
			if (HealthCheck())
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
