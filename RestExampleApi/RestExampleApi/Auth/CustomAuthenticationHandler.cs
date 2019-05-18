using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RestExampleApi.Auth
{
    public class CustomAuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                if (IsValidKey(request))
                {
                    return base.SendAsync(request, cancellationToken);
                }
                else
                {
                    return FailAuthorization();
                }
            } catch (Exception e)
            {
                return FailAuthorization();
            }
        }

        private bool IsValidKey(HttpRequestMessage message)
        {
            var authorizationHeader = message.Headers.Authorization;
            if (authorizationHeader != null)
            {
                if (authorizationHeader.Scheme.Equals("basic", System.StringComparison.OrdinalIgnoreCase) && authorizationHeader.Parameter != null)
                {
                    var credentials = authorizationHeader.Parameter;

                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    credentials = encoding.GetString(Convert.FromBase64String(credentials));

                    int separator = credentials.IndexOf(':');
                    string name = credentials.Substring(0, separator);
                    string password = credentials.Substring(separator + 1);

                    // Hardcode username and password for this exercise
                    if (name == "jb" && password == "hifi")
                    {
                        var identity = new GenericIdentity(name);
                        SetPrincipal(new GenericPrincipal(identity, null));
                    } else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private static Task<HttpResponseMessage> FailAuthorization()
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            var taskCompletionSrc = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletionSrc.SetResult(response);

            return taskCompletionSrc.Task;
        }
    }
}