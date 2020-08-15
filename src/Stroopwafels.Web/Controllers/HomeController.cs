using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stroopwafels.Application.Domain;
using Stroopwafels.Application.Commands;
using Stroopwafels.Application.Queries;
using Stroopwafels.Web.Models;
using MediatR;
using Stroopwafels.Application.Domain;

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
            var viewModel = new OrderDetailsViewModel();

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetQuotes(OrderDetailsViewModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var orderDetails = GetOrderDetails(formModel.OrderRows);
                var quotes = await GetQuotesFor(orderDetails);

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

        private async Task<IEnumerable<Stroopwafels.Application.Domain.Quote>> GetQuotesFor(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var query = new QuotesQuery(orderDetails);

            return await _mediator.Send(query);
        }

        private IList<KeyValuePair<StroopwafelType, int>> GetOrderDetails(IList<OrderRow> orderRows)
        {
            return orderRows.Select(orderRow => new KeyValuePair<StroopwafelType, int>(orderRow.Type, orderRow.Amount)).ToList();
        }

        public async Task<IActionResult> MakeOrder(QuoteViewModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var orderDetails = this.GetOrderDetails(formModel.OrderRows);

                var command = new OrderCommand(orderDetails, formModel.SelectedSupplier);
                await _mediator.Send(command);

                return View("_OrderSeccessful");
            }

            return Index();
        }
    }
}
