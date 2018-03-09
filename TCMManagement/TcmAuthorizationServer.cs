using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using TCMManagement.BusinessLayer;
using static TCMManagement.BusinessLayer.Constants;
using TCMManagement.Models;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace TCMManagement
{
    public class TcmAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            IEntityServices<Person> service = new PersonService();
            Person person = service.SearchItem(context.UserName);

            if(person == null)
            {
                context.SetError("invalid grant", "provide username and password");
            }
            else if(!context.Password.Equals(person.Password))
            {
                context.SetError("invalid grant", "Invalid password");
            }
            else
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, person.Role.Description));
                identity.AddClaim(new Claim("username", person.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, person.FirstName + " " + person.LastName));
                var props = new AuthenticationProperties(new Dictionary<string, string>
                                                        {
                                                            {
                                                                "role", person.Role.Description
                                                            },
                                                            {
                                                                "id", person.PersonId.ToString()
                                                            }
                                                        });
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}