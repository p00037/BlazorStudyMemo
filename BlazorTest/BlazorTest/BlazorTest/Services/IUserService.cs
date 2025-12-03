namespace BlazorTest.Services
{
    // ユーザー情報にアクセスするためのインターフェース
    public interface IUserService
    {
        string GetCurrentUserId();
        string GetCurrentUserName();
        string GetCurrentUserRole();
        public string GetTenantId();
    }
}
