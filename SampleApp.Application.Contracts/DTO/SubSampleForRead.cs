namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO with the SubSample information
    /// </summary>
    public class SubSampleForRead
    {
        public string Code { get; set; }

        public string Info { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {Code} - Name: {Info}";
        }
    }
}
