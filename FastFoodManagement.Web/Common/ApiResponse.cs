namespace FastFoodManagement.Web.Common
{
	public class ApiResponse<T>
	{
		public string Status { get; set; }
		public int Code { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
		public T? Data { get; set; }
		public List<string>? Errors { get; set; }

		public ApiResponse(string message, T data, bool success = true, int code = 200)
		{
			Status = success ? "OK" : "Error";
			Code = code;
			Success = success;
			Message = message;
			Data = data;
			Errors = null;
		}

		public ApiResponse(string message, List<string> errors, bool success = false, int code = 400)
		{
			Status = success ? "OK" : "Error";
			Code = code;
			Success = success;
			Message = message;
			Data = default;
			Errors = errors;
		}
	}
}