using Microsoft.AspNetCore.Mvc;

public class PagesController : Controller
{
    public IActionResult Contact()
    {
        return View();
    }

    public IActionResult ShippingPolicy()
    {
        return View();
    }
}
