[![License: MIT](https://www.rabbitmq.com/img/RabbitMQ-logo.svg)](https://www.rabbitmq.com/)

# RabbitMq Unofficial v1.9

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![License: MIT](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![License: MIT](https://img.shields.io/github/release/srburton/RabbitMQ.EventBus.NetCore.svg)]()
[![License: MIT](https://img.shields.io/github/tag-date/srburton/RabbitMQ.EventBus.NetCore.svg)]()
[![License: MIT](https://img.shields.io/github/languages/count/srburton/RabbitMQ.EventBus.NetCore.svg)]()
[![License: MIT](https://img.shields.io/github/last-commit/srburton/RabbitMQ.EventBus.NetCore.svg)]()
[![License: MIT](https://img.shields.io/github/languages/code-size/srburton/RabbitMQ.EventBus.NetCore.svg)]()
[![License: MIT](https://img.shields.io/github/issues-raw/srburton/RabbitMQ.EventBus.NetCore.svg)]()
[![License: MIT](	https://img.shields.io/github/issues-closed/srburton/RabbitMQ.EventBus.NetCore.svg)]()





### Install

```shell
>> Install-Package RabbitMQ.EventBus.NetCore
 or
>> dotnet add package RabbitMQ.EventBus.NetCore
```

### Startup
```c#
services.AddRabbitMQEventBus("amqp://you-queue", eventBusOptionAction: eventBusOption =>
{
   eventBusOption.ClientProvidedAssembly<Startup>();
   eventBusOption.EnableRetryOnFailure(true, 5000, TimeSpan.FromSeconds(30));
   eventBusOption.RetryOnFailure(TimeSpan.FromSeconds(1));
});

app.RabbitMQEventBusAutoSubscribe();

```

### Event 
```c#
    [EventBus(Exchange = "rabbitmq.eventBus.mail", RoutingKey = "rabbitmq.eventbus.mail")]
    public class MailModel : IEvent
    { 
        public string Email {get; set;} 
        public string SubTitle {get; set;}         
        public string Body { get; set; }
        public DateTimeOffset Time { get; set; }
    }
```

### A event with N RouteKey
```c#
    [EventBus(Exchange = "rabbitmq.eventBus.mail", RoutingKey = "rabbitmq.eventbus.mail")]
    [EventBus(Exchange = "rabbitmq.eventBus.mail", RoutingKey = "rabbitmq.eventbus.mail-other")]
    public class MailOtherModel : IEvent
    {
        public string Email {get; set;} 
        public string SubTitle {get; set;}         
        public string Body { get; set; }
        public object Data { get; set; }
        public DateTimeOffset Time { get; set; }
    }
```
### Subscribe

```c# 
    public class MailBodyHandle : IEventHandler<MailOtherModel>, IDisposable
    {       
        public Task Handle(MailModel message)
        {
           //...
        }
        
        public Task Handle(MailOtherModel message)
        {
          //...
        }
    }    
```

### Publish
```c# 
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IRabbitMQEventBus _eventBus;

        public ValuesController(IRabbitMQEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
                _eventBus.Publish(new {
                    Body = "Hello Word",
                    Time = DateTimeOffset.Now
                }, exchange: "rabbitmq.eventBus.mail", routingKey: "rabbitmq.eventbus.mail");
               
            return "Ok";
        }
}
```
