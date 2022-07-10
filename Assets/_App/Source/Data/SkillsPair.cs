using System;

namespace Source
{
    public struct SkillsPair : IEquatable<SkillsPair>
    {
        public ISkill firstSkill;
        public ISkill secondSkill;

        public SkillsPair(ISkill firstSkill, ISkill secondSkill)
        {
            this.firstSkill = firstSkill;
            this.secondSkill = secondSkill;
        }

        public bool Equals(SkillsPair other)
        {
            return firstSkill.Id == other.firstSkill.Id && secondSkill.Id == other.secondSkill.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is SkillsPair other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (firstSkill.Id * 397) ^ secondSkill.Id;
            }
        }
    }
}