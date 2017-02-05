using System.Security.Claims;
using System.Threading.Tasks;
using MicroBlog.Presentation.DependencyResolution;
using MicroBlog.Service;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;

namespace MicroBlog.Presentation.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});
            
            using (var accountService = IoC.Container.GetInstance<IAccountService>())
            {
                var user = await accountService.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
        }

        // TODO: Get the user information populated (http://stackoverflow.com/questions/35849710/webapi-how-to-get-userid-from-token)
        /*public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            context.AdditionalResponseParameters.Add("userId", context.Identity.GetUserId());

            return Task.FromResult<object>(null);
        }*/
    }
}