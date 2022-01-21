using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        MyDbContext db = new();

        // GET: api/<RecordsController>
        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get(string selDate)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            var myDateArray = selDate.Split('.');

            bool correctInput = int.TryParse(myDateArray[0], out day);

            if (correctInput)
            {
                correctInput = int.TryParse(myDateArray[1], out month);
            }

            if (correctInput)
            {
                correctInput = int.TryParse(myDateArray[2], out year);
            }

            DateTime myDate = DateTime.Now;

            if (correctInput)
            {
                try
                {
                    myDate = new(year, month, day);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    correctInput = false;
                }
            }

            var records = correctInput ? db.Records.Where(r => r.MyDateTime.Date == myDate).OrderBy(r => r.MyDateTime)
                            .Select(r => new
                            {
                                id = r.Id,
                                myDateTime = r.MyDateTime,
                                title = r.Title,
                                status = r.Status.ToString()
                            }) : null;

            return new JsonResult(records);
        }

        // GET api/<RecordsController>/5
        [HttpGet("{recId}")]
        [Produces("application/json")]
        public IActionResult Get(string mydate, string recId)
        {
            bool correctInput = int.TryParse(recId, out int id);
            MyRecord record = null;

            if (correctInput)
            {
                record = db.Records.Where(r => r.Id == id).FirstOrDefault();
            }

            return new JsonResult(new
            {
                id = record.Id,
                myDateTime = record.MyDateTime,
                title = record.Title,
                status = record.Status.ToString(),
                description = record.Description
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] MyRecord myRecord)
        {
            User myUser = db.Users.FirstOrDefault();

            myRecord.MyUser = myUser;

            if (myRecord != null)
            {
                db.Records.Add(myRecord);

                db.SaveChanges();
            }

            return Ok();
        }

        // PUT api/<RecordsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MyRecord myRecord)
        {
            User myUser = db.Users.FirstOrDefault();

            myRecord.MyUser = myUser;

            db.Records.Attach(myRecord);

            db.Entry(myRecord).State = EntityState.Modified;

            db.SaveChanges();

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dbRecord = db.Records.Where(r => r.Id == id).FirstOrDefault();

            db.Records.Remove(dbRecord);

            db.SaveChanges();

            return Ok();
        }
    }
}
