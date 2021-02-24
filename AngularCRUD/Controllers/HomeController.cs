using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularCRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetContacts()
        {
            List<Contact> contactList = new List<Contact>();
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                contactList = dc.Contacts.OrderBy(a => a.ContactName).ToList();
            }
            return new JsonResult { Data = contactList, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //fetch contact by id
        public JsonResult GetContact(int contactID)
        {
            Contact contact = null;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                contact = dc.Contacts.Where(a => a.ContactID.Equals(contactID)).FirstOrDefault();
            }
            return new JsonResult { Data = contact, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // save contact
        [HttpPost]
        public JsonResult SaveContact(Contact contact)
        {
            bool status = false;
            string message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    using (MyDatabaseEntities dc = new MyDatabaseEntities())
                    {
                        if (contact.ContactID > 0)
                        {
                            //Update
                            var c = dc.Contacts.Where(a => a.ContactID.Equals(contact.ContactID)).FirstOrDefault();
                            if (c != null)
                            {
                                c.ContactName = contact.ContactName;
                                c.ContactNo = contact.ContactNo;
                                c.EmailID = contact.EmailID;
                                c.Address = contact.Address;
                            }
                        }
                        else
                        {
                            //create
                            dc.Contacts.Add(contact);
                        }
                        dc.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new JsonResult { Data = new  { status = status, message = message } };
        }

        // delete
        [HttpPost]
        public JsonResult DeleteContact(int contactID)
        {
            bool status = false;
            string message = "";
            try
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    var v = dc.Contacts.Where(a => a.ContactID.Equals(contactID)).FirstOrDefault();
                    if (v != null)
                    {
                        dc.Contacts.Remove(v);
                        dc.SaveChanges();
                        status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new JsonResult { Data = new { status = status, message = message } };
        }
	}
}