using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    public class MessageController : Controller
    {
        public instant_messageContext _InstantMessageContext;
        public MessageController(instant_messageContext instantMessageContext)
        {
            this._InstantMessageContext = instantMessageContext;
        }

        [HttpPost]
        public async Task<ActionResult> PostMsg([FromBody] dynamic reqMsg)
        {
            using (_InstantMessageContext)
            {
                if (reqMsg != null)
                {
//                    var currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var newMsg = new MessageList();
                    newMsg.UserName = (string)reqMsg.username;
                    newMsg.MsgContent = (string)reqMsg.msgContent;
                    newMsg.MsgTime = DateTime.Now;
                    
                    var userInfo = _InstantMessageContext.User.Where(x => x.Name == newMsg.UserName).FirstOrDefault();
                    newMsg.UserId = userInfo.Id;
                    try
                    {
                        await _InstantMessageContext.MessageList.AddAsync(newMsg);
                        await _InstantMessageContext.SaveChangesAsync();
                        return Ok(new
                        {
                            status = "ok"
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("errorrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr");
                        
                    }
                }

                return Ok(new
                {
                    status = "failed"
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetMsgList()
        {
            using (_InstantMessageContext)
            {
                var msgList = _InstantMessageContext.MessageList.ToList();
                
                return Ok(new
                {
                    status="ok",
                    msgList=msgList
                });
            }
        }

    }
}