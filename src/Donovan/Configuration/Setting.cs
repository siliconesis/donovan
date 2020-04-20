namespace Donovan.Configuration
{
    public class Setting
    {
        /// <summary>
        /// Gets or sets the namespace of the configuration setting.
        /// </summary>
        /// <remarks>
        /// Categories are organized hierarchically in dot-separated, Pascal-cased
        /// identifiers like namespaces.
        /// </remarks>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the key of the setting. The key must be unique within a namespace.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value of the setting.
        /// </summary>
        public string Value { get; set; }
    }
}
