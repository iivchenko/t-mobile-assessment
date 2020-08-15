using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stroopwafels.Ordering;
using Stroopwafels.Ordering.Commands;
using Stroopwafels.Ordering.Queries;
using Stroopwafels.Ordering.Services;
using Stroopwafels.Web.Models;

namespace Stroopwafels.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuotesQueryHandler _quotesQueryHandler;
        private readonly OrderCommandHandler _orderCommandHandler;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            var httpClientWrapper = new HttpClientWrapper();
            var suppliers = new IStroopwafelSupplierService[]
            {
                new StroopwafelSupplierAService(httpClientWrapper),
                new StroopwafelSupplierBService(httpClientWrapper),
                new StroopwafelSupplierCService(httpClientWrapper)
            };
            this._quotesQueryHandler = new QuotesQueryHandler(suppliers);
            this._orderCommandHandler = new OrderCommandHandler(suppliers);
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
            var viewModel = new OrderDetailsViewModel();

            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult GetQuotes(OrderDetailsViewModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var orderDetails = this.GetOrderDetails(formModel.OrderRows);
                var quotes = this.GetQuotesFor(orderDetails);

                var viewModel = new QuoteViewModel();
                foreach (var quote in quotes)
                {
                    viewModel.Quotes.Add(new Models.Quote
                    {
                        SupplierName = quote.Supplier.Name,
                        TotalAmount = quote.TotalPricePresentation
                    });
                }

                viewModel.OrderRows = formModel.OrderRows;
                viewModel.SelectedSupplier = quotes.OrderBy(q => q.TotalPrice).First().Supplier.Name;

                return View("_Quotes", viewModel);
            }
            return Index();
        }

        private IList<Ordering.Quote> GetQuotesFor(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var query = new QuotesQuery(orderDetails);

            IList<Ordering.Quote> orders = this._quotesQueryHandler.Handle(query);
            return orders;
        }

        private IList<KeyValuePair<StroopwafelType, int>> GetOrderDetails(IList<OrderRow> orderRows)
        {
            return orderRows.Select(orderRow => new KeyValuePair<StroopwafelType, int>(orderRow.Type, orderRow.Amount)).ToList();
        }

        public IActionResult MakeOrder(QuoteViewModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var orderDetails = this.GetOrderDetails(formModel.OrderRows);

                var command = new OrderCommand(orderDetails, formModel.SelectedSupplier);
                this._orderCommandHandler.Handle(command);

                return View("_OrderSeccessful");
            }

            return Index();
        }
    }
}
