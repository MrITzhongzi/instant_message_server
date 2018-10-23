using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController: Controller
    {
        public instant_messageContext _instant_messageContext;
        public LoginController(instant_messageContext instantMessageContext)
        {
            this._instant_messageContext = instantMessageContext;
        }

        [HttpPost]
        public async Task<ActionResult> login([FromBody] User reqUser)
        {
            using (_instant_messageContext)
            {
//                ObjectContext+LINQ( to Entity)的方式 查询数据库 方式一
//                var query = from c in _instant_messageContext.User
//                    where c.Name == "lhw"
//                    select c;
//                foreach (var user in query)
//                {
//                    var str = JsonConvert.SerializeObject(user);
//                    Console.WriteLine(str);
//                }
              
//                方式二
                var query = _instant_messageContext.User.Where(x => x.Name == reqUser.Name).FirstOrDefault();
                if (query != null)
                {
                    if (reqUser.Name == query.Name && reqUser.Password == query.Password)
                    {
                        return Ok(new
                        {
                            status = "ok"
                        });
                    }
                }
                
                return Ok(new
                {
                    status = "failed"
                });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> register([FromBody] User reqUser)
        {
            using (_instant_messageContext)
            {
                var queryUser = _instant_messageContext.User.Where(x => x.Name == reqUser.Name).FirstOrDefault();
                if (queryUser != null)
                {
                    return Ok(new
                    {
                        status= "failed"
                    });
                }

                await _instant_messageContext.User.AddAsync(reqUser);
                await _instant_messageContext.SaveChangesAsync();
                return Ok(new
                {
                    status="ok"
                });
            }
        }
    }
}