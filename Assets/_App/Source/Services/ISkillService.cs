using System.Collections.Generic;

namespace Source
{
    public interface ISkillService
    {
        public List<Skill> GetAvailableSkills();
        public Skill GetBaseSkill();
    }
}