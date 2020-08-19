using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stroopwafels.Application.Commands.PlaceOrder;
using Stroopwafels.Web.Models;
using MediatR;
using Stroopwafels.Application.Queries.GetQuotes;
using AutoMapper;

namespace Stroopwafels.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediator mediator, IMapper mapper, ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult Index()
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
                var query = _mapper.Map<GetQuotesQuery>(model);
                var quote = await _mediator.Send(query);
                var viewModel = _mapper.Map<OrderViewModel>(quote);
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
                var command = _mapper.Map<PlaceOrderCommand>(model);

                await _mediator.Send(command);

                return PartialView("_OrderSeccessful");
            }

            return Index();
        }
    }
}
