using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;

namespace RestaurantApp.Web.Models.IP
{
    public class TraceIP : ActionFilterAttribute
    {
        IPDetailModel model = new IPDetailModel();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress.ToString();
            if (context.HttpContext.Session.GetString(remoteIp) == null)
            {
                model.Count = 1;
                model.IPAddress = remoteIp;
                model.Time = DateTime.Now;
                context.HttpContext.Session.SetString(remoteIp, JsonConvert.SerializeObject(model));
            }
            else
            {
                var _record = JsonConvert.DeserializeObject<IPDetailModel>(context.HttpContext.Session.GetString(remoteIp));
                if (DateTime.Now.Subtract(_record.Time).TotalMinutes < 1 && _record.Count > 1)
                {
                    context.Result = new JsonResult("Acces denined for ip "+model.IPAddress+"!");
                }
                else
                {
                    _record.Count = _record.Count + 1;
                    context.HttpContext.Session.Remove(remoteIp);
                    context.HttpContext.Session.SetString(remoteIp, JsonConvert.SerializeObject(_record));
                }
            }
        }
    }
}
