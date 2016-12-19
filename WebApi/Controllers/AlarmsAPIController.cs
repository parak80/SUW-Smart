using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AlarmsAPIController : ApiController
    {
        private SmartEntities1 db = new SmartEntities1();

        // GET: api/AlarmsAPI
        public IQueryable<Alarm> GetAllAlarm()
        {
            return db.Alarms;
        }

        // GET: api/AlarmsAPI/5
        [ResponseType(typeof(Alarm))]
        public IHttpActionResult GetAlarm(int id)
        {
            Alarm alarm = db.Alarms.Find(id);
            if (alarm == null)
            {
                return NotFound();
            }

            return Ok(alarm);
        }
   // }
        // PUT: api/AlarmsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAlarm(int id, Alarm alarm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alarm.Id)
            {
                return BadRequest();
            }

            db.Entry(alarm).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlarmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AlarmsAPI
        [ResponseType(typeof(Alarm))]
        public IHttpActionResult PostAlarm(Alarm alarm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alarms.Add(alarm);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = alarm.Id }, alarm);
        }

        // DELETE: api/AlarmsAPI/5
        [ResponseType(typeof(Alarm))]
        public IHttpActionResult DeleteAlarm(int id)
        {
            Alarm alarm = db.Alarms.Find(id);
            if (alarm == null)
            {
                return NotFound();
            }

            db.Alarms.Remove(alarm);
            db.SaveChanges();

            return Ok(alarm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlarmExists(int id)
        {
            return db.Alarms.Count(e => e.Id == id) > 0;
        }
    }
}