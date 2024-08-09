using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VendorManagement2.Models;

namespace VendorManagement2.Controllers
{
    public class UserDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Users in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Users in the database.
        /// </returns>
        /// <example>
        /// GET: api/UserData/ListUsers
        /// </example>
        [HttpGet]
        [Route("api/UserData/listUsers")]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult ListUsers()
        {
            List<User> users = db.Users.ToList();
            List<UserDto> userDtos = new List<UserDto>();

            users.ForEach(u => userDtos.Add(new UserDto()
            {
                UserID = u.UserID,
                Username = u.Username,
                Email = u.Email
            }));

            return Ok(userDtos);
        }

        /// <summary>
        /// Returns all Users associated with a particular event.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Users associated with a particular event
        /// </returns>
        /// <param name="id">Event Primary Key</param>
        /// <example>
        /// GET: api/UserData/ListUsersForEvent/1
        /// </example>


        /// <summary>
        /// Returns Users not associated with a particular event.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Users not associated with a particular event
        /// </returns>
        /// <param name="id">Event Primary Key</param>
        /// <example>
        /// GET: api/UserData/ListUsersNotInEvent/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult ListUsersNotInEvent(int id)
        {
            List<User> users = db.Users.Where(
                u => !u.UserEvents.Any(
                    ue => ue.EventID == id)
                ).ToList();
            List<UserDto> userDtos = new List<UserDto>();

            users.ForEach(u => userDtos.Add(new UserDto()
            {
                UserID = u.UserID,
                Username = u.Username,
                Email = u.Email
            }));

            return Ok(userDtos);
        }

        // GET: api/UserData/listeventsforuser/{id}
        [HttpGet]
        [Route("api/UserData/listeventsforuser/{id}")]
        [ResponseType(typeof(IEnumerable<EventDto>))]
        public IHttpActionResult ListEventsForUser(int id)
        {
            var events = db.UserEvents
                           .Where(ue => ue.UserID == id)
                           .Select(ue => new EventDto
                           {
                               EventID = ue.Event.EventID,
                               EventName = ue.Event.EventName,
                               EventDate = ue.Event.EventDate
                           }).ToList();

            return Ok(events);
        }

        /// <summary>
        /// Associates a particular user with a particular event.
        /// </summary>
        /// <param name="userid">The user ID primary key.</param>
        /// <param name="eventid">The event ID primary key.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/UserData/AssociateUserWithEvent/9/1
        /// </example>
        [HttpPost]
        [Route("api/UserData/AssociateUserWithEvent/{userid}/{eventid}")]
        public IHttpActionResult AssociateUserWithEvent(int userid, int eventid)
        {
            User user = db.Users.Find(userid);
            Event @event = db.Events.Find(eventid);

            if (user == null || @event == null)
            {
                return NotFound();
            }

            UserEvent userEvent = new UserEvent
            {
                UserID = userid,
                EventID = eventid,
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
        /// <param name="userid">The user ID primary key.</param>
        /// <param name="eventid">The event ID primary key.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/UserData/UnAssociateUserWithEvent/9/1
        /// </example>
        [HttpPost]
        [Route("api/UserData/UnAssociateUserWithEvent/{userid}/{eventid}")]
        public IHttpActionResult UnAssociateUserWithEvent(int userid, int eventid)
        {
            UserEvent userEvent = db.UserEvents.FirstOrDefault(ue => ue.UserID == userid && ue.EventID == eventid);

            if (userEvent == null)
            {
                return NotFound();
            }

            db.UserEvents.Remove(userEvent);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: api/UserData/FindUser/5
        [HttpGet]
        [Route("api/UserData/finduser/{id}")]
        [ResponseType(typeof(UserDto))]
        public IHttpActionResult FindUser(int id)
        {
            User user = db.Users.Find(id);
            UserDto userDto = new UserDto()
            {
                UserID = user.UserID,
                Username = user.Username,
                Email = user.Email
            };
            if (user == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Adds an user to the system.
        /// </summary>
        /// <param name="user">JSON FORM DATA of an user.</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: user ID, User Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/UserData/AddUser
        /// FORM DATA: User JSON Object
        /// </example>
        [ResponseType(typeof(User))]
        [Route("api/userdata/adduser")]
        [HttpPost]
        public IHttpActionResult AddUser(User @user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            @user.CreatedAt = DateTime.Now;
            @user.UpdatedAt = DateTime.Now;

            db.Users.Add(@user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @user.UserID }, @user);
        }

        /// <summary>
        /// Deletes an user from the system by its ID.
        /// </summary>
        /// <param name="id">The primary key of the user.</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/UserData/DeleteUser/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(User))]
        [Route("api/userdata/deleteuser/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok();
        }

        [ResponseType(typeof(void))]
        [Route("api/userdata/updateuser/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateUser(int id, User @user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @user.UserID)
            {
                return BadRequest();
            }

            var existingUser = db.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update user properties
            existingUser.Username = @user.Username;
            existingUser.Email = @user.Email;
            // Update other properties as needed
            existingUser.UpdatedAt = DateTime.Now;

            db.Entry(existingUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserID == id) > 0;
        }

    }
}