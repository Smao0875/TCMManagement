using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMManagement.Test.TestPersonController
{
    class IntegrationTestPersonController
    {
        private string accessToken;


        private void GetAccessToken()
        {
            var message = server
                .WithHttpRequestMessage(req => req
                    .WithRequestUri("/token")
                    .WithMethod(HttpMethod.Post)
                    .WithFormUrlEncodedContent("username=TestAuthor@test.com&password=testpass&grant_type=password"))
                .ShouldReturnHttpResponseMessage()
                .AndProvideTheHttpResponseMessage();

            var result = JObject.Parse(message.Content.ReadAsStringAsync().Result);
            this.accessToken = (string)result["access_token"];
        }
    }
}
