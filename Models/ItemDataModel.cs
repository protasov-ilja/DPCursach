namespace Frontend.Models
{
	public sealed class ItemDataModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int Amount { get; set; }
		public string Description { get; set; }
		public int IdProducer { get; set; }
	}
}
