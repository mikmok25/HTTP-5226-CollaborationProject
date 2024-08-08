using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using VendorManagement2.Models;
using System.Data.Entity;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace VendorManagement2.Controllers
{
    public class EventDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all events in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all events in the database.
        /// </returns>
        /// <example>
        /// GET: api/EventData/ListEvents
        /// </example>
        [HttpGet]
        [Route("api/EventData/listEvents")]
        [ResponseType(typeof(EventDto))]
        public IHttpActionResult ListEvents()
        {
            List<Event> events = db.Events.ToList();
            List<EventDto> eventDtos = new List<EventDto>();

            events.ForEach(e => eventDtos.Add(new EventDto()
            {
                EventID = e.EventID,
                EventName = e.EventName,
                EventDescription = e.EventDescription,
                EventLocation = e.EventLocation,
                EventType = e.EventType,
                EventDate = e.EventDate
            }));

            return Ok(eventDtos);
        }

        [HttpGet]
        [Route("api/EventData/listusersforevent/{id}")]
        [ResponseType(typeof(IEnumerable<UserDto>))]
        public IHttpActionResult ListUsersForEvent(int id)
        {
            var users = db.UserEvents
                          .Where(ue => ue.EventID == id)
                          .Select(ue => new UserDto
                          {
                              UserID = ue.User.UserID,
                              Username = ue.User.Username,
                              Email = ue.User.Email
                          }).ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("api/EventData/listusersnotinevent/{id}")]
        [ResponseType(typeof(IEnumerable<UserDto>))]
        public IHttpActionResult ListUsersNotInEvent(int id)
        {
            var users = db.Users
                          .Where(u => !db.UserEvents.Any(ue => ue.EventID == id && ue.UserID == u.UserID))
                          .Select(u => new UserDto
                          {
                              UserID = u.UserID,
                              Username = u.Username,
                              Email = u.Email
                          }).ToList();

            return Ok(users);
        }



        /// <summary>
        /// Returns a specific event by ID.
        /// </summary>
        /// <param name="id">The primary key of the event.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An event in the system matching the event ID.
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// GET: api/EventData/FindEvent/5
        /// </example>
        [ResponseType(typeof(EventDto))]
        [Route("api/EventData/findevent/{id}")]
        [HttpGet]
        public IHttpActionResult FindEvent(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            EventDto eventDto = new EventDto()
            {
                EventID = @event.EventID,
                EventName = @event.EventName,
                EventDescription = @event.EventDescription,
                EventLocation = @event.EventLocation,
                EventType = @event.EventType,
                EventDate = @event.EventDate
            };

            return Ok(eventDto);
        }

        /// <summary>
        /// Associates a particular user with a particular event.
        /// </summary>
        /// <param name="eventid">The event ID primary key.</param>
        /// <param name="userid">The user ID primary key.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/EventData/AssociateEventWithUser/9/1
        /// </example>
        [HttpPost]
        [Route("api/EventData/AssociateEventWithUser/{eventid}/{userid}")]
        public IHttpActionResult AssociateEventWithUser(int eventid, int userid)
        {
            Event selectedEvent = db.Events.Find(eventid);
            User selectedUser = db.Users.Find(userid);

            if (selectedEvent == null || selectedUser == null)
            {
                return NotFound();
            }

            UserEvent userEvent = new UserEvent
            {
                EventID = eventid,
                UserID = userid,
                Status = "Associated",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            db.UserEvents.Add(userEvent);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Removes an association between a particular user and a particular event.
        /// </summary>
        /// <param name="eventid">The event ID primary key.</param>
        /// <param name="userid">The user ID primary key.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/EventData/UnAssociateEventWithUser/9/1
        /// </example>
        [HttpPost]
        [Route("api/EventData/UnAssociateEventWithUser/{eventid}/{userid}")]
        public IHttpActionResult UnAssociateEventWithUser(int eventid, int userid)
        {
            UserEvent userEvent = db.UserEvents
                .FirstOrDefault(ue => ue.EventID == eventid && ue.UserID == userid);

            if (userEvent == null)
            {
                return NotFound();
            }

            db.UserEvents.Remove(userEvent);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Adds an event to the system.
        /// </summary>
        /// <param name="event">JSON FORM DATA of an event.</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Event ID, Event Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/EventData/AddEvent
        /// FORM DATA: Event JSON Object
        /// </example>
        [ResponseType(typeof(Event))]
        [Route("api/eventdata/addevent")]
        [HttpPost]
        public IHttpActionResult AddEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            @event.CreatedAt = DateTime.Now;
            @event.UpdatedAt = DateTime.Now;

            db.Events.Add(@event);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @event.EventID }, @event);
        }

        /// <summary>
        /// Updates a particular event in the system with POST Data input.
        /// </summary>
        /// <param name="id">Represents the Event ID primary key.</param>
        /// <param name="event">JSON FORM DATA of an event.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/EventData/UpdateEvent/5
        /// FORM DATA: Event JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/eventdata/updateevent/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.EventID)
            {
                return BadRequest();
            }

            @event.UpdatedAt = DateTime.Now;

            db.Entry(@event).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        /// <summary>
        /// Deletes an event from the system by its ID.
        /// </summary>
        /// <param name="id">The primary key of the event.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/EventData/DeleteEvent/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Event))]
        [Route("api/eventdata/deleteevent/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return NotFound();
            }

            db.Events.Remove(@event);
            db.SaveChanges();

            return Ok(@event);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventExists(int id)
        {
            return db.Events.Count(e => e.EventID == id) > 0;
        }
    }
}
