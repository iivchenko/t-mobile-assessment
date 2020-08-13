using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Ordering.Services;
using Ordering;
using Ordering.Commands;
using Ordering.Queries;
using Stroopwafels.Models;

namespace Stroopwafels.Controllers
{
    public class StroopwafelController : Controller
    {
        private readonly QuotesQueryHandler _quotesQueryHandler;
        private readonly OrderCommandHandler _orderCommandHandler;

        public StroopwafelController()
        {
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

        public ActionResult Index()
        {
            var viewModel = new OrderDetailsViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetQuotes(OrderDetailsViewModel formModel)
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

                return View(viewModel);
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

        public ActionResult Order(QuoteViewModel formModel)
        {
            if (this.ModelState.IsValid)
            {
                var orderDetails = this.GetOrderDetails(formModel.OrderRows);
                
                var command = new OrderCommand(orderDetails, formModel.SelectedSupplier);
                this._orderCommandHandler.Handle(command);

                return View();
            }

            return Index();
        }
    }
}