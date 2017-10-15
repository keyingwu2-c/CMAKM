using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Principal;
using System.Threading;
using System.Web;


namespace URE.Common_Code
{
    /// <summary>
    ///Ad authentication
    /// </summary>
    public class NTAuth
    {
        private TcpListener listener;
        private int _port;
        public NTAuth(int port)
        {
            _port = port;
        }

        private void CreateServer(object state)
        {
            try
            {
                var nsServer = new NegotiateStream(listener.AcceptTcpClient().GetStream());
                nsServer.AuthenticateAsServer(CredentialCache.DefaultNetworkCredentials, ProtectionLevel.None, TokenImpersonationLevel.Impersonation);
            }
            catch (Exception ex)
            {
                //LogEvent.LogError(ex);
            }
        }

        public bool Authenticate(NetworkCredential creds)
        {
            listener = new TcpListener(IPAddress.Loopback, _port);
            listener.Start();

            var client = new TcpClient("localhost", _port);

            ThreadPool.QueueUserWorkItem(new WaitCallback(CreateServer));

            var nsClient = new NegotiateStream(client.GetStream(), true);

            using (nsClient)
            {
                try
                {
                    nsClient.AuthenticateAsClient(creds, creds.Domain + "\\" + creds.UserName, ProtectionLevel.None, TokenImpersonationLevel.Impersonation);
                    listener.Stop();
                    return nsClient.IsAuthenticated;
                }
                catch (Exception ex)
                {
                    //LogEvent.LogError(ex);//LogEvent does not exist
                    listener.Stop();
                    return false;
                }
            }
        }
    }
}