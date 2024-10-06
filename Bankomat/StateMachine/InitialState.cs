namespace Bankomat
{
	internal class InitialState : StateBase
	{
		internal override async Task ExecuteAsync()
		{
			context!.ResetCard();
			Console.WriteLine("Нажмите любую кнопку, чтобы снять деньги");
			Console.Read();
			context!.SetState(new GetUserCardNumberState());
		}
	}
}
