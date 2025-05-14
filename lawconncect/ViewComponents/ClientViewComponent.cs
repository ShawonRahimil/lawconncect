using lawconncect.Data;
using lawconncect.Models;
using Microsoft.AspNetCore.Mvc;

namespace lawconncect.ViewComponents
{
    public class ClientViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public ClientViewComponent( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? clientid)
        {
            var client = _context.Clients.OrderBy(c=>c.Name).ToList();
            if(clientid.HasValue)
            {
                client = client.Where(c => c.Id == clientid.Value).ToList();
            }
          

                return View(client);
        }
    }
}
