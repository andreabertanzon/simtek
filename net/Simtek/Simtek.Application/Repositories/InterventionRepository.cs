using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Simtek.Application.Repositories.Mappings;
using Simtek.Data;
using Simtek.Domain;
using Intervention = Simtek.Domain.Intervention;

namespace Simtek.Application.Repositories;

public class InterventionRepository (IDbContextFactory<SimtekContext> contextFactory): IInterventionRepository
{
    public async Task<IEnumerable<Intervention>> GetInterventionsAsync(int page, int pageSize, CancellationToken cancellationToken = default,
        Func<Data.Intervention, bool>? predicate = null, bool includeCustomer = false)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        
        // if (includeCustomer)
        // {
            var interventions = context.Interventions
                .Include(i => i.Site)
                .ThenInclude(x=>x.Customer)
                .Include(i => i.InterventionWorkers)
                .ThenInclude(iw => iw.Worker)
                .Where(predicate ?? (_ => true))
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return interventions.Select(i => i.ToDomain(i.InterventionWorkers.Select(iw => iw.Worker)));
        // }
        // else
        // {
        //     var interventions = context.Interventions
        //         .Include(i => i.Site)
        //         .Include(i => i.InterventionWorkers)
        //         .ThenInclude(iw => iw.Worker)
        //         .Where(predicate ?? (_ => true))
        //         .Skip(page * pageSize)
        //         .Take(pageSize)
        //         .ToList();
        //     return interventions.Select(i => i.ToDomain(i.InterventionWorkers.Select(iw => iw.Worker)));
        // }
    }

    public async Task<Intervention> GetInterventionAsync(int id, CancellationToken cancellationToken = default)
    {
        var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var intervention = await context.Interventions
            .Include(i => i.Site)
            .ThenInclude(x=>x.Customer)
            .Include(i => i.InterventionWorkers)
            .ThenInclude(iw => iw.Worker)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);

        if (intervention is not null)
        {
            return intervention.ToDomain(intervention.InterventionWorkers.Select(iw => iw.Worker));
        }
        else
        {
            throw new NoDataFoundException("Intervention not found.");
        }
    }

    public async Task<Intervention> CreateInterventionAsync(Intervention intervention, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        
        // if the intervention exists update it 
        var interventionExists = await context.Interventions
            .Include(i => i.InterventionWorkers)
            .FirstOrDefaultAsync(i => i.Id == intervention.Id, cancellationToken);
        if (interventionExists is not null)
        {
            interventionExists.Description = intervention.Description;
            interventionExists.Date = intervention.Date;
            interventionExists.Material = JsonSerializer.Serialize(intervention.Materials);
            interventionExists.SiteId = intervention.Site.Id;
            interventionExists.Notes = intervention.Notes;
            context.Interventions.Update(interventionExists);
            
            await context.SaveChangesAsync(cancellationToken);
            foreach (var op in intervention.Operators)
            {
                var interventionWorker = interventionExists.InterventionWorkers.FirstOrDefault(iw => iw.WorkerId == op.Key.Id);
                if (interventionWorker is not null)
                {
                    interventionWorker.HourSpent = op.Value;
                    context.InterventionWorkers.Update(interventionWorker);
                }
                else
                {
                    context.Add(new InterventionWorker
                    {
                        InterventionId = intervention.Id,
                        WorkerId = op.Key.Id,
                        HourSpent = op.Value
                    });
                }
            }
            await context.SaveChangesAsync(cancellationToken);
            return intervention;
        }
        
        var interventionNew = context.Interventions.Add(intervention.ToData());
        await context.SaveChangesAsync(cancellationToken);
        foreach (var op in intervention.Operators)
        {
            context.Add(new InterventionWorker
            {
                InterventionId = interventionNew.Entity.Id,
                WorkerId = op.Key.Id,
                HourSpent = op.Value
            });
        }
        await context.SaveChangesAsync(cancellationToken);
        return intervention;
    }

    public Task<Intervention> UpdateInterventionAsync(Intervention intervention, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Intervention> DeleteInterventionAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}