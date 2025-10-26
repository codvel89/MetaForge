using MetaForge.Core.Context;
using MetaForge.Core.Entities.Workflow;
using Microsoft.EntityFrameworkCore;

namespace MetaForge.Core.Repositories;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly MetadataDbContext _context;

    public WorkflowRepository(MetadataDbContext context)
    {
        _context = context;
    }

    public async Task<WorkflowDefinition?> GetDefinitionByIdAsync(int id)
    {
        return await _context.WorkflowDefinitions
            .Include(w => w.Instances)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<WorkflowDefinition?> GetDefinitionByCodeAsync(string code)
    {
        return await _context.WorkflowDefinitions
            .FirstOrDefaultAsync(w => w.Code == code);
    }

    public async Task<List<WorkflowDefinition>> GetAllDefinitionsAsync(bool activeOnly = false)
    {
        var query = _context.WorkflowDefinitions.AsQueryable();

        if (activeOnly)
            query = query.Where(w => w.IsActive);

        return await query
            .OrderBy(w => w.Name)
            .ToListAsync();
    }

    public async Task<WorkflowDefinition> CreateDefinitionAsync(WorkflowDefinition definition)
    {
        definition.CreatedAt = DateTime.UtcNow;
        _context.WorkflowDefinitions.Add(definition);
        await _context.SaveChangesAsync();
        return definition;
    }

    public async Task<WorkflowDefinition> UpdateDefinitionAsync(WorkflowDefinition definition)
    {
        definition.UpdatedAt = DateTime.UtcNow;
        _context.WorkflowDefinitions.Update(definition);
        await _context.SaveChangesAsync();
        return definition;
    }

    public async Task DeleteDefinitionAsync(int id)
    {
        var definition = await _context.WorkflowDefinitions.FindAsync(id);
        if (definition != null)
        {
            _context.WorkflowDefinitions.Remove(definition);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<WorkflowInstance?> GetInstanceByIdAsync(int id)
    {
        return await _context.WorkflowInstances
            .Include(w => w.WorkflowDefinition)
            .Include(w => w.Steps)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<List<WorkflowInstance>> GetInstancesByDefinitionAsync(int definitionId)
    {
        return await _context.WorkflowInstances
            .Where(w => w.WorkflowDefinitionId == definitionId)
            .OrderByDescending(w => w.StartedAt)
            .ToListAsync();
    }

    public async Task<List<WorkflowInstance>> GetInstancesByStatusAsync(string status)
    {
        return await _context.WorkflowInstances
            .Where(w => w.Status == status)
            .OrderByDescending(w => w.StartedAt)
            .ToListAsync();
    }

    public async Task<WorkflowInstance> CreateInstanceAsync(WorkflowInstance instance)
    {
        instance.StartedAt = DateTime.UtcNow;
        _context.WorkflowInstances.Add(instance);
        await _context.SaveChangesAsync();
        return instance;
    }

    public async Task<WorkflowInstance> UpdateInstanceAsync(WorkflowInstance instance)
    {
        _context.WorkflowInstances.Update(instance);
        await _context.SaveChangesAsync();
        return instance;
    }

    public async Task<List<WorkflowStep>> GetInstanceStepsAsync(int instanceId)
    {
        return await _context.WorkflowSteps
            .Where(s => s.WorkflowInstanceId == instanceId)
            .OrderBy(s => s.StepOrder)
            .ToListAsync();
    }

    public async Task<WorkflowStep> CreateStepAsync(WorkflowStep step)
    {
        step.StartedAt = DateTime.UtcNow;
        _context.WorkflowSteps.Add(step);
        await _context.SaveChangesAsync();
        return step;
    }

    public async Task<WorkflowStep> UpdateStepAsync(WorkflowStep step)
    {
        _context.WorkflowSteps.Update(step);
        await _context.SaveChangesAsync();
        return step;
    }
}
