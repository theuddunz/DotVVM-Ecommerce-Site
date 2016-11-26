using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using System.Threading.Tasks;
using Stripe;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCF.ViewModels
{
    [Authorize]
    public class CartViewModel : MasterpageViewModel
    {
        public double total { get; set; }
        public string Message { get; set; } = "Your Cart Is Empty.";
        public bool Enabled { get; set; } = false;
        public bool CheckOutVisible { get; set; } = true;
        //Variables For The Modal
        public bool Modal { get; set; } = false;
        public string HeadText { get; set; }
        //Variables For The ShippingAddress
        [Required(ErrorMessage="The Address Is Required")]
        public string AddressL1 { get; set; }
        public string AddressL2 { get; set; }
        [Required(ErrorMessage="The City is Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "The State is Required")]
        public string State { get; set; }
        [Required(ErrorMessage = "The PostalCode is Required")]
        public string PostalCode { get; set; }
        public bool EnabledAddress { get; set; } = false;
        public bool EnabledPayment { get; set; } = false;
        //Variables For The Payment
        public string CardNumber { get; set; }
        public string EMonth { get; set; } //"E" = Expired
        public string EYear { get; set; }
        public string CVC { get; set; }
        public string ErrorMessage { get; set; }

        public GridViewDataSet<CartItem> CartItems { get; set; } = new GridViewDataSet<CartItem>
        {
            SortExpression = nameof(CartItem.CartItemID),
            PageSize = 5,
            SortDescending = false
        };

        public void Remove(int itemid)
        {
            using (var db = new Database())
            {
                var cartitem = db.CartItems.Find(itemid);
                var cart = db.Carts.Find(cartitem.CartID);
                cart.Count--;
                db.CartItems.Remove(cartitem);
                db.SaveChanges();
                CartService.LoadDataCart(CartItems);
                total = Convert.ToDouble(CartService.GetTotal());
            }
        }

        public void CreateAddress()
        {
            using (var db = new Database())
            {
                var Address = new Address();
                Address.AddressLine1 = AddressL1;
                Address.AddressLine2 = AddressL2;
                Address.City = City;
                Address.PostCode = PostalCode;
                Address.State = State;
                Address.UserID = Convert.ToInt32(UserService.GetCurrentUserId());
                db.ShippingAddress.Add(Address);
                db.SaveChanges();
                EnabledAddress = false;
                EnabledPayment = true;
                HeadText = "Payment Information.";
            }
        }

        public void CallModal()
        {
            Modal = true;
            EnabledPayment = false;
            EnabledAddress = true;
            HeadText = "Shipping Address.";
        }

        public void MakePayment()
        {
            //var planService = new StripePlanService("sk_test_6ipiRVhLjC6JrGJsu3Cc2nTZ"); //YouSet The Key of Your Account For Get Paid
            var Charge = new StripeChargeCreateOptions(); //You Make The Option Of the Charge

            Charge.Amount = Convert.ToInt32(total);
            Charge.Currency = "eur";

            Charge.SourceCard = new SourceCard()
            {
                Number = CardNumber,
                ExpirationMonth = EMonth,
                ExpirationYear = EYear,
                Cvc = CVC,
                AddressLine1 = AddressL1,
                AddressLine2 = AddressL2,
                AddressCity = City,
                AddressState = State
            };
            Charge.Capture = true;
            var ChargeService = new StripeChargeService();
            try
            {
                StripeCharge stripeCharge = ChargeService.Create(Charge);
                if (stripeCharge.Paid == true)
                {
                    using (var db = new Database())
                    {
                        var cart = db.Carts.Find(Convert.ToInt32(CartService.GetCartID()));


                        var ncartitem = from p in db.CartItems
                                        where p.CartID == cart.CartID
                                        select p;

                        var order = new Order();
                        order.Total = total;
                        order.OrderDate = DateTime.Now;
                        order.AddressLine1 = AddressL1;
                        order.AddressLine2 = AddressL2;
                        order.City = City;
                        order.State = State;
                        order.PostalCode = PostalCode;
                        order.Status = 0;
                        order.UserID = Convert.ToInt32(UserService.GetCurrentUserId());

                        foreach (var item in ncartitem)
                        {
                            var orderitem = new OrderItem();
                            orderitem.Name = item.Name;
                            orderitem.Quantity = item.Quantity;
                            orderitem.ProductID = item.ProductID;
                            orderitem.OrderID = order.OrderID;
                            order.OrderItems.Add(orderitem);
                            db.CartItems.Remove(item);
                        }
                        cart.Count = 0;
                        db.Orders.Add(order);
                        db.SaveChanges();
                    }
                    Context.RedirectToRoute("ProfilePage");
                }
            }
            catch (StripeException ex)
            {

                ErrorMessage = ex.Message;
            }

        }

        public override Task PreRender()
        {
            if (CartService.GetTotal() == 0)
            {
                Enabled = true;
                CheckOutVisible = false;
            }
            total = Convert.ToDouble(CartService.GetTotal());
            CartService.LoadDataCart(CartItems);
            return base.PreRender();

        }
    }
}

