using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("list", Name = "admin-contact-list")]
        public async Task<IActionResult> List()
        {
            return View(await _contactService.GetContactList());
        }
        [HttpGet("item/{id}", Name = "admin-contact-item")]
        public async Task<IActionResult> Item(int id)
        {
            var contact = await _contactService.GetContact(id);
            if(contact == null) return NotFound();
            return View(contact);
        }
        [HttpPost("delete/{id}", Name = "admin-contact-delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _contactService.DeleteContact(id);
            if (deleteResult == false) return NotFound();
            return RedirectToRoute("admin-contact-list");
        }
    }
}
