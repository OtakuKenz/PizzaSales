namespace PizzaSalesApi.Models.DTOs
{
    /// <summary>
    /// Generic result for import operations.
    /// </summary>
    public class ImportResultDto
    {
        /// <summary>
        /// Number of records successfully inserted.
        /// </summary>
        public int Inserted { get; set; }

        /// <summary>
        /// Number of duplicate records skipped.
        /// </summary>
        public int Duplicates { get; set; }
    }
}