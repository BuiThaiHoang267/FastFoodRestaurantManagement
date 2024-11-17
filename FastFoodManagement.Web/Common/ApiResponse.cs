using static System.Runtime.InteropServices.JavaScript.JSType;

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

		public ApiResponse() 
		{
			Success = false;
			Code = 0;
			Message = "";
			Status = "";
			Errors = new List<string>();
		}

		public ApiResponse(string message, int code, bool success)
		{
			Success = success;
			Code = code;
			Message = message;
			Status = success ? "Ok" : "Error";
		}

		public static ApiResponse<T> SuccessResponse(T? data, string message = "Request succeeded", int code = 200)
		{
			return new ApiResponse<T>
			{
				Status = "OK",
				Code = code,
				Success = true,
				Message = message,
				Data = data,
				Errors = null
			};
		}

		public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null, int code = 400)
		{
			return new ApiResponse<T>
			{
				Status = "Error",
				Code = code,
				Success = false,
				Message = message,
				Data = default,
				Errors = errors ?? new List<string>()
			};
		}
	}
}