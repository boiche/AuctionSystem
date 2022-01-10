using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Models.Http.Responses
{
    public class BlackListResponse : HttpResponse
    {
        public override Stream Body { get => null; set => Body = value; }
        public override long? ContentLength { get => Body.Length; set => ContentLength = value; }
        public override string ContentType { get => "text/html"; set => ContentType = value; }

        public override IResponseCookies Cookies { get => null; }

        public override bool HasStarted { get => false; }

        public override IHeaderDictionary Headers { get => Headers; }

        public override HttpContext HttpContext => throw new NotImplementedException();

        public override int StatusCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}
