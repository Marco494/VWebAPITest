namespace VooltWebAPITest
{
	public class WebHeaderBlock
	{
		public string ID { get; set; }
		public int BlockOrder { get; set; }
		public string BusinessName { get; set; }
		public bool DisplayLogo { get; set; }
		public List<List<MenuItem>>? MenuItems { get; set; }
		public List<ButtonItem>? CTAButton { get; set; }
		
		//Default Values
		public WebHeaderBlock()
		{
			ID = "HeaderBlock";
			BlockOrder = 0;
			BusinessName = "Default Business Name";
			DisplayLogo = false;
			MenuItems = new List<List<MenuItem>>();
			CTAButton = new List<ButtonItem>();
		}

	}

}