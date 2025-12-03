using System.Security.Claims;
using BlazorTest.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BlazorTest.Components.Account
{
    public class TenantUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        private readonly ApplicationDbContext _db;

        public TenantUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor,
            ApplicationDbContext db
            )
            : base(userManager, optionsAccessor)
        {
            _db = db;
        }

        // クレームの生成ロジックをオーバーライドします
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            // 1. 基底クラスのメソッドを呼び出し、デフォルトのクレームセットを取得
            //    (UserId, UserName, Rolesなど)
            var identity = await base.GenerateClaimsAsync(user);

            int? tenantId = _db.Tenants.FirstOrDefault(x => x.UserId == user.Id)?.Id;

            // 2. カスタムプロパティ (TenantId) がnullでなければクレームとして追加
            if (tenantId.HasValue)
            {
                // ClaimTypes.UserData やカスタム文字列をクレームタイプとして使用できます
                // ここではカスタムのクレームタイプ "TenantId" を使用します
                identity.AddClaim(new Claim("TenantId", tenantId.Value.ToString()));
            }

            // 3. 生成された ClaimsIdentity を返却
            return identity;
        }
    }
}
