using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Source
{
    public class SkillsService : ISkillService
    {
        public List<Skill> _availableSkills;

        public SkillsService()
        {
            _availableSkills = new List<Skill>();
        }

        public List<Skill> GetAvailableSkills()
        {
            return _availableSkills;
        }

        public Skill GetBaseSkill()
        {
            try
            {
                return _availableSkills.First(skill => skill.Type == SkillType.Base);
            }
            catch (Exception _)
            {
                Debug.LogError("Base skill not found!");
                throw;
            }
        }
    }
}