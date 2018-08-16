using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using Minifutbol.BL.Extensions;
using Minifutbol.BL.Logics;
using Minifutbol.BL.Models.RealTimeConnection;

namespace Minifutbol.BL.Hubs
{
  public class MinifutbolHub : Hub
  {
    private readonly RealTimeConnectionLogic _realTimeConnectionLogic;

    public MinifutbolHub()
    {
      _realTimeConnectionLogic = new RealTimeConnectionLogic();
    }
    public override async Task OnConnected()
    {
      var at=HttpContext.Current.GetOwinContext().Authentication.User.GetUserId();
      var identity = (ClaimsIdentity)Context.User.Identity;
      var tmp = identity.FindFirst(ClaimTypes.NameIdentifier);
      if (Context.QueryString["UserId"] != null)
      {
        var userId = int.Parse(Context.QueryString["UserId"]);

        Clients.Caller.KeepAlive("UserId:" + userId + " Id: " + Context.ConnectionId);
       var result= _realTimeConnectionLogic.Add(new RealTimeConnectionCreateModel
        {
          ConnectionId = Context.ConnectionId,
          UserId = userId,
          Description = "qosuldu"
        });
      }

      await base.OnConnected();
    }

    public override async Task OnDisconnected(bool stopCalled)
    {

      var  result= _realTimeConnectionLogic.Remove(new RealTimeConnectionDeleteModel
      {
        ConnectionId = Context.ConnectionId
      });
      await base.OnDisconnected(stopCalled);
    }


    #region Methods

    public void RemoteRequestUpdate( JObject at)
    {
      Clients.Others.remoterequestupdate(at);
    }
    

    #endregion
  }
}
