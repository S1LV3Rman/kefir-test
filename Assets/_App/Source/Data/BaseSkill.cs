namespace Source
{
    public struct BaseSkill : ISkill
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public string Name { get; set; }
    }
}