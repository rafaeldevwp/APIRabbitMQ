using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIRabbitMQ.Controllers
{
    [Route("[controller]")]
    public class CPFController : Controller
    {
        private readonly ILogger<CPFController> _logger;
        private readonly CPFProducer _producer;
        private readonly CPFConsumer _consummer;

        public CPFController(ILogger<CPFController> logger, CPFProducer producer, CPFConsumer consumer)
        {
            _logger = logger;
            _producer = producer;
            _consummer = consumer;

        }


        [HttpPost]
        public IActionResult PostCPF([FromBody] string cpf)
        {
            _producer.PublishCPF(cpf);
            return Ok();
        }

        // [HttpGet("StartConsumer")]
        // public IActionResult StartConsumer()
        // {
        //     _consummer.ConsumerCPF();
        //     return Ok();

        // }
    }
}