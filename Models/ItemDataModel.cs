namespace Frontend.Models
{
	public sealed class ItemDataModel
	{
		public int Id { get; set; }
		public string Model { get; set; }
		public double Price { get; set; }
		public int Amount { get; set; }
		public string Description { get; set; }
		public int ProducerId { get; set; }
	}
}
