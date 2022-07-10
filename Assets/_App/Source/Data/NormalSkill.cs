namespace Source
{
    public struct NormalSkill : ISkill
    {
        public int Id { get; set; }
        public int SlotId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}