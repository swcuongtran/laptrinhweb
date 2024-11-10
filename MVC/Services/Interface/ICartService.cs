using MVC.Models;
using System.Collections.Generic;
namespace MVC.Services.Interface
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        void AddToCart(CartItem item);
        void UpdateCart(int productId, int quantity);
        void RemoveFromCart(int productId);
        void ClearCart();
        decimal GetCartTotal();
        int GetCartItemCount();
    }
}
