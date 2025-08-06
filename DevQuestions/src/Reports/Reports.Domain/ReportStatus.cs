namespace Reports.Domain
{
    public enum ReportStatus
    {
        /// <summary>
        /// Статус открыт.
        /// </summary>
        Open,

        /// <summary>
        /// Статус в процессе.
        /// </summary>
        InProgress,

        /// <summary>
        /// Статус решен.
        /// </summary>
        Resolved,

        /// <summary>
        /// Статус закрыт.
        /// </summary>
        Dissmissed
    }
}
