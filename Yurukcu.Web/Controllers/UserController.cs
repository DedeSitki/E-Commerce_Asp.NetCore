using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
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
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration, ProductContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult UserLogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogIn(UserLogInViewModel model)
        {
            var user = _context.Users.AsNoTracking().FirstOrDefault(u => u.EMail == model.EMail);
            bool isPasswordCorrect = false;

            if (user != null)
            {
                isPasswordCorrect = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            }

            if (isPasswordCorrect)
            {
                HttpContext.Session.Clear(); // Eski session verilerini temizle
                HttpContext.Session.SetString("UserEMail", user.EMail);
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Geçersiz e-posta veya şifre");

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

            if (!userId.HasValue)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = _context.Users
                               .Where(u => u.UserId == userId.Value)
                               .Select(u => new UserAccountViewModel
                               {
                                   EMail = u.EMail,
                                   CreatedDate = u.CreatedDate
                               })
                               .SingleOrDefault();

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

            var user = _context.Users
                .Where(u => u.UserId == userId.Value)
                .Select(u => new UserMyProfileViewModel
                {
                    UserId = u.UserId,
                    Birthday = u.Birthday,
                    FullName = u.FullName,
                    Gender = u.Gender,
                    PhoneNumber = u.PhoneNumber
                })
                .SingleOrDefault();


            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogIn", "User");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UserMyProfile(UserMyProfileViewModel model)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.UserId == model.UserId);

            if (ModelState.IsValid)
            {
                if (existingUser != null)
                {
                    existingUser.FullName = model.FullName;
                    existingUser.Gender = model.Gender;
                    existingUser.PhoneNumber = model.PhoneNumber;
                    existingUser.Birthday = model.Birthday;

                    _context.SaveChanges();
                }
                TempData["Message"] = "Bilgiler Güncellendi";
                return RedirectToAction("UserMyProfile", new { userId = model.UserId });
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult UserMyOrders()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            bool userExists = _context.Users.Any(u => u.UserId == userId.Value);

            if (!userExists)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogIn", "User");
            }

            var orderViewModels = _context.OrderDetails
                .Where(u => u.UserId == userId.Value)
                .Select(o => new OrderDetailViewModel
                {
                    OrderAddress = o.OrderAddress,
                    OrderId = o.OrderId,
                    OrderBillingAddress = o.OrderBillingAddress,
                    OrderDate = o.OrderDate,
                    OrderStatus = o.OrderStatus,
                    OrderTrackingCode = o.OrderTrackingCode,
                    PaymentMethod = o.PaymentMethod,
                    TotalPrice = o.TotalPrice
                })
                .AsNoTracking()
                .ToList();

            return View(orderViewModels);
        }

        [HttpGet]
        public IActionResult UserMyOrdersDetail(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var orderDetailViewModel = _context.OrderDetails
                .Where(u => u.UserId == userId.Value)
                .Select(order => new OrderDetailViewModel
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    OrderAddress = order.OrderAddress,
                    TotalPrice = order.TotalPrice,
                    PaymentMethod = order.PaymentMethod,
                    OrderStatus = order.OrderStatus,
                    OrderTrackingCode = order.OrderTrackingCode
                })
                .AsNoTracking()
                .SingleOrDefault();

            return View(orderDetailViewModel);
        }

        [HttpGet]
        public IActionResult UserAddress()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
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
                })
                .AsNoTracking()
                .ToList();
            return View(address);
        }

        [HttpPost]
        public IActionResult UserAddress(List<UserAddressViewModel> addresses)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var userAddressIds = _context.Addresses
                .Where(u => u.UserId == userId.Value)
                .Select(a => a.AddressId)
                .ToHashSet();

            var newAddresses = new List<Address>();
            var updateAddresses = new List<Address>();

            foreach (var viewModel in addresses)
            {
                if (viewModel.AddressId == 0)
                {
                    newAddresses.Add(new Address
                    {
                        UserId = userId.Value,
                        AddressTitle = viewModel.AddressTitle,
                        DeliveryAddress = viewModel.DeliveryAddress,
                        City = viewModel.City,
                        IsBillingAddress = viewModel.IsBillingAddress,
                        ZipCode = viewModel.ZipCode
                    });
                }
                else if (userAddressIds.Contains(viewModel.AddressId))
                {
                    updateAddresses.Add(new Address
                    {
                        AddressId = viewModel.AddressId,
                        UserId = userId.Value,
                        AddressTitle = viewModel.AddressTitle,
                        DeliveryAddress = viewModel.DeliveryAddress,
                        City = viewModel.City,
                        IsBillingAddress = viewModel.IsBillingAddress,
                        ZipCode = viewModel.ZipCode
                    });
                }
            }

            if (newAddresses.Any())
                _context.Addresses.AddRange(newAddresses);

            if (updateAddresses.Any())
                _context.Addresses.UpdateRange(updateAddresses);

            _context.SaveChanges();
            return RedirectToAction("UserAddress", "User");
        }

        [HttpGet]
        public IActionResult UserAddressDelete(int id)
        {

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                RedirectToAction("UserLogIn", "User");
            }


            var address = _context.Addresses.FirstOrDefault(a => a.AddressId == id && a.UserId == userId.Value);

            if (address == null)
            {
                return NotFound(); // Yetkisiz işlem engelleniyor!
            }

            _context.Addresses.Remove(address);
            _context.SaveChanges();

            return RedirectToAction("UserAddress", "User");
        }

        [HttpGet]
        public IActionResult UserContactAndSupport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserContactAndSupport(SupportRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.SubmittedDate = DateTime.Now; 

            var smtpHost = _configuration["SmtpSettings:Host"];
            var smtpPort = int.Parse(_configuration["SmtpSettings:Port"]);
            var smtpEmail = _configuration["SmtpSettings:Email"];
            var smtpPassword = _configuration["SmtpSettings:Password"];
            var enableSsl = bool.Parse(_configuration["SmtpSettings:EnableSsl"]);

            var toEmail = "yurukcualuminyum@gmail.com";
            var subject = "Yeni İletişim Formu Mesajı";
            var body = $@"
                <h3>Yeni mesaj alındı</h3>
                <p><strong>Ad:</strong> {model.Name}</p>
                <p><strong>E-Posta:</strong> {model.EMail}</p>
                <p><strong>Mesaj:</strong><br>{model.Message}</p>
                <p><strong>Gönderim Zamanı:</strong> {model.SubmittedDate}</p>";

            var message = new MailMessage
            {
                From = new MailAddress(model.EMail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(toEmail);

            using var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                EnableSsl = enableSsl
            };

            try
            {
                await smtpClient.SendMailAsync(message);
                TempData["Message"] = "Mesajınız başarıyla gönderildi!";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Mesaj gönderilemedi";
            }

            return RedirectToAction("UserContactAndSupport", "User");
        }

        [HttpGet]
        public async Task<IActionResult> UserShoppingBagAsync()
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

            var cartItems = await _context.ShoppingBags
                                    .Where(c => c.UserId == userId)
                                    .Include(c => c.Product)
                                    .ToListAsync();

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
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var cartItem = await _context.ShoppingBags
                                          .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

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

                await _context.SaveChangesAsync();

                await UpdateCartItemCount(userId.Value);
            }

            return RedirectToAction("UserShoppingBag");
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Message"] = "Giriş yapmanız gerekiyor.";
                return RedirectToAction("UserLogIn", "User");
            }

            var cartItem = await _context.ShoppingBags
                                          .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                await _context.SaveChangesAsync();
                await UpdateCartItemCount(userId.Value);
            }

            return RedirectToAction("UserShoppingBag");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var cartItem = await _context.ShoppingBags
                                          .FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null)
            {
                _context.ShoppingBags.Remove(cartItem);
                await _context.SaveChangesAsync();

                await UpdateCartItemCount(userId.Value);
            }

            return RedirectToAction("UserShoppingBag", "User");
        }

        [HttpPost]
        public async Task<IActionResult> UserAddShoppingBag(int productId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("UserLogIn", "User");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("UserLogIn", "User");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                TempData["Message"] = "Ürün bulunamadı.";
                return RedirectToAction("UserShoppingBag");
            }

            var cartItem = await _context.ShoppingBags.FirstOrDefaultAsync(c => c.ProductId == productId && c.UserId == userId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                _context.ShoppingBags.Add(new ShoppingBag
                {
                    UserId = userId.Value,
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            await _context.SaveChangesAsync();

            var cartItemCount = await _context.ShoppingBags
                                        .Where(c => c.UserId == userId)
                                        .SumAsync(c => c.Quantity);
            HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
            TempData["Message"] = "Ürün Sepete Eklendi";

            return RedirectToAction("Index", "Home");
        }

        private async Task UpdateCartItemCount(int userId)
        {
            var cartItemCount = await _context.ShoppingBags
                                               .Where(c => c.UserId == userId)
                                               .SumAsync(c => c.Quantity);

            HttpContext.Session.SetInt32("CartItemCount", cartItemCount);
        }
    }
}