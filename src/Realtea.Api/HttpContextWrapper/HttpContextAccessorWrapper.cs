using System.Security.Claims;

namespace Realtea.App.HttpContextWrapper
{
	public interface IHttpContextAccessorWrapper
	{
		int GetUserId();
	}

	public class HttpContextAccessorWrapper : IHttpContextAccessorWrapper
	{
		private IHttpContextAccessor _httpContextAccessor;
		public HttpContextAccessorWrapper(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public int GetUserId()
		{
			return Convert.ToInt32(_httpContextAccessor!.HttpContext!.User.FindFirstValue("sub"));
		}
	}
}

