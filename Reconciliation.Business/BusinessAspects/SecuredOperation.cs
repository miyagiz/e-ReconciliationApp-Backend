using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Reconciliation.Core.Extensions;
using Reconciliation.Core.Utilities.Interceptors;
using Reconciliation.Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reconciliation.Business.BusinessAspects
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _contextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(",");
            _contextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var token = _contextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            if (token != "")
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var decodeToken = jwtSecurityToken.Claims;
                foreach (var claim in decodeToken)
                {
                    foreach (var role in _roles)
                    {
                        if (claim.ToString().Contains(role))
                        {
                            return;
                        }
                    }
                }
            }
            throw new Exception("Bu İşlem İçin Yetkiniz Bulunmuyor !!!");
        }
    }
}
