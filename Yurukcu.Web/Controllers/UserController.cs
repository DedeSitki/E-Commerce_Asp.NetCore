using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using Yurukcu.Web.Data;
using Yurukcu.Web.Entity;
using Yurukcu.Web.Models;

namespace Yurukcu.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ProductContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserController( ProductContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet]
        public IActionResult UserLogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogIn(UserLogInViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.EMail == model.EMail);

            if (user != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                if (isPasswordCorrect)
                {
                    HttpContext.Session.SetString("UserEMail", user.EMail);
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    return RedirectToAction("Index", "Home");
                }               
            }

            ModelState.AddModelError("", "Bilgilerinizi kontol ediniz");

            return View();
        }

        [HttpGet]
        public IActionResult UserSignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserSignUp(UserCreateViewModel model)
        {
            if (_context.Users.Any(u => u.EMail == model.EMail))
            {
                ModelState.AddModelError("EMail", "Bu E-Posta adresi daha önce alınmış");
            }

            if (ModelState.IsValid)
            {
                var entity = new User
                {
                    EMail = model.EMail,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    CreatedDate = DateTime.Now
                };
                _context.Users.Add(entity);
                _context.SaveChanges();
                return RedirectToAction("UserLogIn", "User");
            }

            return View();
        }

        [HttpGet]
        public IActionResult UserAccountInfo()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                HttpContext.Session.Clear(); 
                return RedirectToAction("UserLogIn", "User");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UserMyProfile()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                HttpContext.Session.Clear(); 
                return RedirectToAction("UserLogIn", "User");
            }

            var model = new UserMyProfileViewModel
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.Birthday
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UserMyProfile(UserMyProfileViewModel model)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == model.UserId);

            if (existingUser != null)
            {
                existingUser.FullName = model.FullName;
                existingUser.Gender = model.Gender;
                existingUser.PhoneNumber = model.PhoneNumber;
                existingUser.Birthday = model.Birthday;

                _context.SaveChanges();
            }

            return RedirectToAction("UserAccountInfo", new { userId = model.UserId });
        }

        [HttpGet]
        public IActionResult UserMyOrders()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogIn", "User");
            }

            var orders = _context.OrderDetails
                .Where(o => o.User.UserId == userId)
                .ToList();

            var orderViewModels = orders.Select(order => new OrderDetailViewModel
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                OrderAddress = order.OrderAddress,
                TotalPrice = order.TotalPrice,
                PaymentMethod = order.PaymentMethod,
                OrderStatus = order.OrderStatus,
                OrderTrackingCode = order.OrderTrackingCode
            }).ToList();

            return View(orderViewModels); 
        }

        [HttpGet]
        public IActionResult UserMyOrdersDetail(int id)
        {
            var orderDetail = _context.OrderDetails
                                      .FirstOrDefault(o => o.OrderId == id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            var orderDetailViewModel = new OrderDetailViewModel
            {
                OrderId = orderDetail.OrderId,
                OrderDate = orderDetail.OrderDate,
                OrderAddress = orderDetail.OrderAddress,
                TotalPrice = orderDetail.TotalPrice,
                PaymentMethod = orderDetail.PaymentMethod,
                OrderStatus = orderDetail.OrderStatus,
                OrderTrackingCode = orderDetail.OrderTrackingCode
            };

            return View(orderDetailViewModel);
        }

        [HttpGet]
        public IActionResult UserAddress()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var address = _context.Addresses
                .Where(a => a.UserId == userId)
                .Select(a => new UserAddressViewModel
                {
                    AddressId = a.AddressId,
                    AddressTitle = a.AddressTitle,
                    City = a.City,
                    DeliveryAddress = a.DeliveryAddress,
                    ZipCode = a.ZipCode,
                    IsBillingAddress = a.IsBillingAddress
                }).ToList();
            return View(address);
        }

        [HttpPost]
        public IActionResult UserAddress(List<UserAddressViewModel> addresses)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("UserLogIn", "User");

            foreach (var viewModel in addresses)
            {
                var address = new Address
                {
                    AddressId = viewModel.AddressId,
                    UserId = userId.Value,
                    AddressTitle = viewModel.AddressTitle,
                    DeliveryAddress = viewModel.DeliveryAddress,
                    City = viewModel.City,
                    ZipCode = viewModel.ZipCode,
                    IsBillingAddress = viewModel.IsBillingAddress
                };

                if (address.AddressId == 0)
                    _context.Addresses.Add(address);
                else
                    _context.Addresses.Update(address);
            }

            _context.SaveChanges();
            return RedirectToAction("UserAddress","User");
        }

        [HttpGet]
        public IActionResult UserAddressDelete(int id)
        {
            var address = _context.Addresses.FirstOrDefault(a => a.AddressId == id);
            if(address != null)
            {
                _context.Addresses.Remove(address);
                _context.SaveChanges();
            }
            return RedirectToAction("UserAddress", "User");
        }

        [HttpGet]
        public IActionResult UserContactAndSupport()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult UserShoppingBag()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogIn", "User");
            }

            var cartItems = _context.ShoppingBags
                                    .Where(c => c.UserId == userId)
                                    .Include(c => c.Product)
                                    .ToList();

            var cartItemCount = cartItems.Sum(c => c.Quantity);
            HttpContext.Session.SetInt32("CartItemCount", cartItemCount);

            var viewModel = cartItems.Select(c => new UserShoppingBagViewModel
            {
                ProductId = c.ProductId,
                ProductName = c.Product.ProductName,
                Price = c.Product.Price,
                Quantity = c.Quantity
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var cartItem = _context.ShoppingBags.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    _context.ShoppingBags.Remove(cartItem);
                }

                _context.SaveChanges();

                var cartItemCount = _context.ShoppingBags
                                            .Where(c => c.UserId == userId)
                                            .Sum(c => c.Quantity);
                HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
            }

            return RedirectToAction("UserShoppingBag");
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                TempData["Message"]= "Giriş yapmanız gerekiyor.";
                return  RedirectToAction("UserLogIn","User");
            }
            var cartItem = _context.ShoppingBags.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            if(cartItem != null)
            {
                cartItem.Quantity++;

                _context.SaveChanges();

                var cartItemCount = _context.ShoppingBags
                    .Where(c => c.UserId == userId)
                    .Sum(c => c.Quantity);
                HttpContext.Session.SetInt32("CartItemCount", cartItemCount);

            }

            return RedirectToAction("UserShoppingBag", "User");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if(userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }
            var cartItem = _context.ShoppingBags.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            if(cartItem != null)
            {
                _context.ShoppingBags.Remove(cartItem);
                _context.SaveChanges();

                var cartItemCount = _context.ShoppingBags
                    .Where(c => c.UserId == userId)
                    .Sum(c => c.Quantity);
                HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
            }
            return RedirectToAction("UserShoppingBag", "User");
        }

        [HttpPost]
        public IActionResult UserAddShoppingBag(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogIn", "User");
            }

            var product = _context.DiscountProducts.FirstOrDefault(p => p.DiscountProductId == productId);
            if (product == null)
            {
                TempData["Message"] = "Ürün bulunamadı.";
                return RedirectToAction("UserShoppingBag");
            }

            var cartItem = _context.ShoppingBags.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                _context.ShoppingBags.Add(new ShoppingBag
                {
                    UserId = userId.Value,
                    ProductId = product.DiscountProductId,
                    ProductName = product.ProductName,
                    Price = product.LowPrice,
                    Quantity = 1
                });
            }

            _context.SaveChanges();

            var cartItemCount = _context.ShoppingBags
                                        .Where(c => c.UserId == userId)
                                        .Sum(c => c.Quantity);
            HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
            TempData["Message"] = "Ürün Sepete Eklendi";

            return RedirectToAction("Index", "Home");
        }



    }

}