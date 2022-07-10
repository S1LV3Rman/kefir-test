namespace Source
{
    public interface IGraphSearchService
    {
        public bool IsSkillLearnable(SkillsGraph skillsGraph, int skillId);
        public bool IsSkillRemovable(SkillsGraph skillsGraph, int skillId);
    }
}