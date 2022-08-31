using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HangfireNet6.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HangfireDemoController : ControllerBase
    {
        // GET: api/<HangfireDemoController>
        [HttpGet]
        public IActionResult OneNigh()
        {
            // 第一招射後不理
            // fire and got:站台啟動後只會執行一次
            BackgroundJob.Enqueue(() => Debug.WriteLine("fire and got"));
            return Ok("射後不理");
        }
        [HttpGet]
        public IActionResult DelaySchedule()
        {
            Debug.WriteLine($"API現在時間：{DateTime.Now}");

            BackgroundJob.Schedule(
                () => Debug.WriteLine($"由HangFire排程發送，時間:{DateTime.Now}"),
                TimeSpan.FromSeconds(3));

            return Ok();
        }
        [HttpGet]
        public IActionResult TimedRepeat()
        {
            Debug.WriteLine($"API現在時間：{DateTime.Now}");
            //【 秒 分 時 日 月 周 年 】，其中年是可選型別，也就是說他如果在不設定年分的情況下是每年。
            RecurringJob.AddOrUpdate(
                 () => Debug.WriteLine($"API現在時間：{DateTime.Now}")
                 , "15 7 * * *");

            return Ok();

        }
        // GET: api/<HangfireDemoController>
        [HttpGet]
        public IActionResult Continuations()
        {
            // 第一招射後不理
            // fire and got:站台啟動後只會執行一次
            var jobid = BackgroundJob.Enqueue(() => Debug.WriteLine("fire and got"));
           
            BackgroundJob.ContinueJobWith(jobid,()=> Debug.WriteLine("two and got"));

            return Ok("接續射後不理");
        }
    }
}
