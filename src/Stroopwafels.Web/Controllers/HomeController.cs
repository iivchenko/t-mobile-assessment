using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stroopwafels.Application.Commands.PlaceOrder;
using Stroopwafels.Web.Models;
using MediatR;
using Stroopwafels.Application.Queries.GetQuotes;

namespace Stroopwafels.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediator mediator, ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Order()
        {
            var viewModel = new NewOrderViewModel();

            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateOrder(NewOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var query = new GetQuotesQuery
                {
                    Items = model.OrderLines.Select(orderRow => new QuotesItem { Amount = orderRow.Amount, Type = orderRow.Type }),
                    Customer = new QuotesCustomer
                    {
                        Name = model.CustomerName,
                        WishDate = model.WishDate
                    }
                };

                var quote = await _mediator.Send(query);

                var viewModel = new OrderViewModel
                {
                    CustomerName = quote.CustomerName,
                    TotalPrice = quote.TotalPrice,
                    WishDate = quote.WishDate,
                    OrderLines = quote.Items.Select(x => new OrderLineViewModel
                    {
                        Amount = x.Amount,
                        Type = x.Type,
                        Supplier = x.Supplier
                    }).ToArray()
                };

                return PartialView("_AcceptOrder", viewModel);
            }
            return Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new PlaceOrderCommand
                {
                    Customer = new PlaceOrderCustomer
                    {
                        Name = model.CustomerName,
                        WishDate = model.WishDate
                    },
                    Items = model.OrderLines.Select(x => new PlaceOrderItem
                    {
                        Amount = x.Amount,
                        Type = x.Type,
                        Supplier = x.Supplier
                    })
                };

                await _mediator.Send(command);

                return PartialView("_OrderSeccessful");
            }

            return Index();
        }
    }
}
