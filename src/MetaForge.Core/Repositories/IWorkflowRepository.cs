using MetaForge.Core.Entities.Workflow;

namespace MetaForge.Core.Repositories;

/// <summary>
/// Repositorio para gestión de workflows
/// </summary>
public interface IWorkflowRepository
{
    Task<WorkflowDefinition?> GetDefinitionByIdAsync(int id);
    Task<WorkflowDefinition?> GetDefinitionByCodeAsync(string code);
    Task<List<WorkflowDefinition>> GetAllDefinitionsAsync(bool activeOnly = false);
    Task<WorkflowDefinition> CreateDefinitionAsync(WorkflowDefinition definition);
    Task<WorkflowDefinition> UpdateDefinitionAsync(WorkflowDefinition definition);
    Task DeleteDefinitionAsync(int id);
    
    Task<WorkflowInstance?> GetInstanceByIdAsync(int id);
    Task<List<WorkflowInstance>> GetInstancesByDefinitionAsync(int definitionId);
    Task<List<WorkflowInstance>> GetInstancesByStatusAsync(string status);
    Task<WorkflowInstance> CreateInstanceAsync(WorkflowInstance instance);
    Task<WorkflowInstance> UpdateInstanceAsync(WorkflowInstance instance);
    
    Task<List<WorkflowStep>> GetInstanceStepsAsync(int instanceId);
    Task<WorkflowStep> CreateStepAsync(WorkflowStep step);
    Task<WorkflowStep> UpdateStepAsync(WorkflowStep step);
}
