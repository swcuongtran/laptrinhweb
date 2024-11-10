using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;

namespace MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public ActionResult Index()
        {
            var cartItem = _cartService.GetCartItems();
            ViewBag.CartTotal = _cartService.GetCartTotal();
            return View(cartItem);
        }
        [Route("Cart/AddToCart/{productId}")]
        public IActionResult AddToCart(int productId)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            _cartService.AddToCart(cartItem);
            return RedirectToAction("Index","Cart");
        }
        public IActionResult UpdateCart(int productID, int quantity)
        {
            _cartService.UpdateCart(productID, quantity);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int productID)
        {
            _cartService.RemoveFromCart(productID);
            return RedirectToAction("Index");
        }
    }
}
