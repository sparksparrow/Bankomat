namespace Bankomat
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			await new Bankomat().StartAwait();
		}
	}
}
