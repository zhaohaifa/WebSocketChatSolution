using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSocketChatItem
{
    public partial class _Default : Page
    {
      public string Ip ;
        protected void Page_Load(object sender, EventArgs e)
        {
            var x = Session;
            Ip = GetUserIP();
            foreach (var item in Request.Params.AllKeys)
            {
               System.Diagnostics.Debug.WriteLine(Request[item]);
            }

            foreach (var item in Request.ServerVariables.AllKeys)
            {
                System.Diagnostics.Debug.WriteLine(Request.ServerVariables[item]);
            }
        }
        private string GetUserIP()
        {
            string _userIP;
            if (Request.ServerVariables["HTTP_VIA"] == null)//未使用代理
            {
                _userIP = Request.UserHostAddress;
            }else
            if (Request.ServerVariables["REMOTE_ADDR"] ==null)
            {
                _userIP = Request.UserHostAddress;
            }
            else//使用代理服务器
            {
                _userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return _userIP;
        }

    }

}