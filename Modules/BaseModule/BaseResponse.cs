namespace Application.Entity
{

    // This class is made the base response class, because this has the two
    // basic things that every response should have. i.e. the status code and the .
    public class BaseResponse
    {
    	
		public int status { get; set; }
		
		public string developerMessage { get; set; }
	}
}