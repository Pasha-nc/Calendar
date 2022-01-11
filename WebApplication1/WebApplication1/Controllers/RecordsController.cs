using Microsoft.AspNetCore.Mvc;
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
        UnitOfWork unitOfWork;
        public RecordsController()
        {
            unitOfWork = new();
        }
        
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

            var records = correctInput ? unitOfWork.RecordRepo.Get(r => r.MyDateTime.Date == myDate).OrderBy(r => r.MyDateTime)
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
                record = unitOfWork.RecordRepo.Get(id);
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

        // POST api/<RecordsController>
        [HttpPost]
        public IActionResult Post(CreateRecordBindingModel inputData)
        {            
            bool correctInput = DateTime.TryParse(inputData.MyDate, out DateTime myDateTime);

            User myUser = unitOfWork.UserRepo.Get().FirstOrDefault();

            MyRecord myRecord = null;

            if (correctInput)
            {
                myRecord = new(myUser, myDateTime, inputData.Title, (RecordStatus)inputData.Status);
            }
            else
            {
                return BadRequest();
            }

            if (myRecord != null)
            {
                unitOfWork.RecordRepo.Insert(myRecord);

                unitOfWork.Save();
            }

            return Ok();
        }

        // PUT api/<RecordsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            unitOfWork.RecordRepo.Delete(id);

            unitOfWork.Save();

            return Ok();
        }
    }
}
