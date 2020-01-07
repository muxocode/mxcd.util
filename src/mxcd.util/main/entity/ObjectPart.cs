namespace mxcd.util.entity
{
    internal class ObjectPart : IObjectPart
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public TypeObjectPart TypePart { get; set; }
    }
}