using Gatherly.Domain.Gatherly.Persistance.OutBox;
using Gatherly.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Gatherly.Persistance.Interceptors
{
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if(dbContext is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            var outboxMessages = dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(aggregateRoot =>
                {
                    var domainEvents = aggregateRoot.GetDomainEvents();
                    aggregateRoot.ClearDomainEvents();
                    return domainEvents;
                })
                .Select(domainEvent => new OutBoxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUTC = DateTime.UtcNow,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

            dbContext.Set<OutBoxMessage>().AddRange(outboxMessages);

            //Add in Program.cs
            //builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
            //and adddbContext call in program.cs changed to
            //builder.Services.AddDbContext<ApplicationDbContext>(
            //(sp, optionBuilder) => 
            //{
            //    var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            //    optionBuilder.UseSqlServer(connectionstring)
            //     .AddInterceptor(interceptor);
            //});

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
