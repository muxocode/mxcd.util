namespace mxcd.util.entity
{
    /// <summary>
    /// Type of part of an object
    /// </summary>
    public enum TypeObjectPart
    {
        /// <summary>
        /// Property type
        /// </summary>
        Property,
        /// <summary>
        /// Filed type
        /// </summary>
        Field
    }
    /// <summary>
    /// Part of an object
    /// </summary>
    public interface IObjectPart
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Value
        /// </summary>
        object Value { get; }
        /// <summary>
        /// Type
        /// </summary>
        TypeObjectPart TypePart { get; }
    }
}