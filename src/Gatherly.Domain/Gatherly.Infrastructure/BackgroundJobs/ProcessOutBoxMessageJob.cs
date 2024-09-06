using Gatherly.Domain.Gatherly.Persistance;
using Gatherly.Domain.Gatherly.Persistance.OutBox;
using Gatherly.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gatherly.Domain.Gatherly.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutBoxMessageJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPublisher _publisher;
        public ProcessOutBoxMessageJob(ApplicationDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _dbContext
                .Set<OutBoxMessage>()
                .Where(m => m.ProcessedOnUTC == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            foreach(OutBoxMessage outboxMessage in messages)
            {
                IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content);
                if(domainEvent is null)
                {
                    continue;
                }
                await _publisher.Publish(domainEvent, context.CancellationToken);

                outboxMessage.ProcessedOnUTC = DateTime.UtcNow;
            }

            await _dbContext.SaveChangesAsync();

            //Register background job in Program.cs
            //builder.Services.AddQuartz(configure => {
            //   var jobKey = new JobKey(nameof(ProcessOutboxMessageJob));
            //   configure.AddJob<ProcessOutboxMessageJob>(jobKey).AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever));
            //  configure.UseMicrosoftDependencyInjectionJobFactory();
            //});
            //builder.Services.AddQuartzHostedService();
        }
    }
}
