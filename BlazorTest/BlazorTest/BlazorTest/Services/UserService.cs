using System.Security.Claims;

namespace BlazorTest.Services
{
    // 認証情報に依存するサービスの実装
    public class CurrentUserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // DI による IHttpContextAccessor の注入
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            // 1. HttpContext から User (ClaimsPrincipal) を取得
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
            {
                // 認証されていない場合は null または適切な値を返す
                return null;
            }

            // 2. クレームから NameIdentifier (ユーザーID) を取得
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string GetCurrentUserName()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            // 役割クレームを取得する (複数ある場合は FindAll を使う)
            return user?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public string GetCurrentUserRole()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            // 役割クレームを取得する (複数ある場合は FindAll を使う)
            return user?.FindFirst(ClaimTypes.Role)?.Value;
        }

        public string GetTenantId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            // 役割クレームを取得する (複数ある場合は FindAll を使う)
            return user?.FindFirst("TenantId")?.Value;
        }
    }
}
