namespace PashaInsuranceTest.DTOs.Interfaces
{
    public interface IEditableData<TKey>
    {
        TKey Id { get; set; }
    }
}
