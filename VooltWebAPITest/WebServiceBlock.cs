namespace VooltWebAPITest
{
	public class WebServiceBlock
	{
		public string ID { get; set; }
		public int BlockOrder { get; set; }
		public string HeadlineText { get; set; }
		public List<List<ServiceItem>>? ServiceItems { get; set; }

		//Default Values
		public WebServiceBlock()
		{
			ID = "ServiceBlock1";
			BlockOrder = 2;
			HeadlineText = "Default Headline";
			ServiceItems = new List<List<ServiceItem>>();
		}

	}
}