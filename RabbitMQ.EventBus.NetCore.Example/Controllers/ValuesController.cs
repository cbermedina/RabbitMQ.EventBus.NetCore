﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.EventBus.NetCore.Attributes;
using RabbitMQ.EventBus.NetCore.Events;

namespace RabbitMQ.EventBus.NetCore.Example.Controllers
{

    [EventBus(Exchange = "RabbitMQ.EventBus.Simple", RoutingKey = "rabbitmq.eventbus.test")]
    public class MessageBody : IEvent
    {
        public string Body { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    [EventBus(Exchange = "RabbitMQ.EventBus.Simple", RoutingKey = "rabbitmq.eventbus.test")]
    [EventBus(Exchange = "RabbitMQ.EventBus.Simple", RoutingKey = "rabbitmq.eventbus.test1")]
    public class MessageBody1 : IEvent
    {
        public string Body { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class MessageBodyHandle : IEventHandler<MessageBody1>, IDisposable
    {
        private Guid id;
        private readonly ILogger<MessageBodyHandle> _logger;

        public MessageBodyHandle(ILogger<MessageBodyHandle> logger)
        {
            id = Guid.NewGuid();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }

        public Task Handle(MessageBody1 message/*, MessageEventArgs args*/)
        {
            Console.WriteLine(id + "=>" + typeof(MessageBody1).Name);
            Console.WriteLine(message.Serialize());
            //Console.WriteLine(args.Serialize());
            return Task.CompletedTask;
        }
    }





    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRabbitMQEventBus _eventBus;

        public ValuesController(IRabbitMQEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            for (int i = 0; i < 1; i++)
            {
                _eventBus.Publish(new
                {
                    Body = $"rabbitmq.eventbus.testQuiroz => {i}",
                    Time = DateTimeOffset.Now
                }, exchange: "RabbitMQ.EventBus.SimpleExchange", routingKey: "testQuiroz");
                _eventBus.Publish(new
                {
                    Body = $"rabbitmq.eventbus.testBermudez => {i}",
                    Time = DateTimeOffset.Now
                }, exchange: "RabbitMQ.EventBus.SimpleExchange", routingKey: "testBermudez");
            }
    
            return "Ok";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
