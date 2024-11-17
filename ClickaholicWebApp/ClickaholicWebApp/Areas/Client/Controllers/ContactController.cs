using ClickaholicWebApp.BLL.ServiceLayer.Dtos.Client;
using ClickaholicWebApp.BLL.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClickaholicWebApp.Areas.Client.Controllers
{
    [Area("client")]
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("index", Name = "client-contact-index")]
        public IActionResult Index()
        {
            return View(new ContactAdd());
        }
        [HttpPost("post", Name = "client-contact-post")]
        public async Task<IActionResult> PostContact(ContactAdd model)
        {
            if (!ModelState.IsValid) return View(model);
            await _contactService.AddContact(model);
            return RedirectToRoute("client-contact-index");
        }
    }
}
