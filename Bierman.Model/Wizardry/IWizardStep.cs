namespace Bierman.Wizardry
{
    public interface IWizardStep<T> where T : IWizardData
    {
        string? Title { get; }
        T? Data { get; }
        IWizardStep<T>? Previous { get; set; }
        IWizardStep<T>? Next { get; set; }
    }
}
