namespace Bankomat
{
	internal static class Extensions
	{
		internal static async Task<string> ReadLineWithTimeoutAsync()
		{
			var readTask = new Task(() => Console.Read());
			var readTimeout = new TimeSpan(hours: 0, minutes: 0, seconds: 30);
			do
			{
				var readlineTimeout = new TimeSpan(hours: 0, minutes: 0, seconds: 30);
				var readlineTask = Task.Run(() => Console.ReadLine());
				if (await Task.WhenAny(readlineTask, Task.Delay(readlineTimeout)) == readlineTask)
				{
					return (await readlineTask)!.Trim();
				}

				Console.WriteLine("Нажмите любую кнопку, иначе обработка прекратиться...");
			}
			while (await Task.WhenAny(readTask, Task.Delay(readTimeout)) == readTask);

			throw new TimeoutException("Пользователь не ввел данные в течение 1 минуты");
		}
	}
}
