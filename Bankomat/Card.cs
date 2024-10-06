namespace Bankomat
{
	internal class Card
	{
		internal string Number { get; set; }

		internal string Date { get; set; }

		internal string Cvc { get; set; }

		public override string ToString() => $"Карта{Environment.NewLine}Номер: {Number}{Environment.NewLine}Дата: {Date}{Environment.NewLine}CVC: {Cvc}{Environment.NewLine}";
	}
}
