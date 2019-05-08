using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeus_MVC.Areas.Admin.Models;
using Zeus_MVC.Models;

namespace Zeus_MVC.Areas.Admin.Controllers
{
    [AdminAuthorization(Roles = "Admin")]
    public class MessageController : Controller
    {
        private ApplicationDbContext _context;

        public MessageController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin/Message
        public ActionResult Index()
        {
            try
            {
                var MessageList = _context.ContactMessages.OrderByDescending(p => p.Id).ToList();

                return View(MessageList);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Message/Details/5
        public ActionResult Details(int id)
        {
            var MessageInDb = _context.ContactMessages.SingleOrDefault(m => m.Id == id);

            if (MessageInDb == null)
                return HttpNotFound();

            return View(MessageInDb);
        }


        // POST: Admin/Message/ToggleRead/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ToggleRead(int id)
        {
            try
            {
                var messageInDb = _context.ContactMessages.SingleOrDefault(c => c.Id == id);

                if (messageInDb == null)
                    return HttpNotFound();

                if (messageInDb.Status == MessageStatus.Read)
                    messageInDb.Status = MessageStatus.UnRead;
                else if (messageInDb.Status == MessageStatus.UnRead)
                    messageInDb.Status = MessageStatus.Read;

                _context.SaveChanges();

                return RedirectToAction("Details", "Message", new { id = id });
            }
            catch
            {
                return RedirectToAction("Index", "Message");
            }
        }


        // POST: Admin/Message/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var messageInDb = _context.ContactMessages.SingleOrDefault(c => c.Id == id);

                _context.ContactMessages.Remove(messageInDb);
                _context.SaveChanges();

                return RedirectToAction("Index", "Message");
            }
            catch
            {
                return RedirectToAction("Details", "Message", new { id = id });
            }
        }

    }
}