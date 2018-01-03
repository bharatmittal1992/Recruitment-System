﻿using RS.Common.CommonData;
using RS.Common.Enums;
using RS.Common.Extensions;
using RS.Data.Interfaces;
using RS.Entity.Models;
using RS.Service.Interfaces;
using RS.ViewModel.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RS.Service.Logic
{
    public class SkillManagerService : ISkillManagerService
    {
        private readonly ISkillRepository _skillRepository;
        public SkillManagerService(ISkillRepository skillRepository)
        {
            this._skillRepository = skillRepository;
        }

        public IResult CreateSkill(SkillViewModel skill)
        {
            var result = new Result
            {
                Operation = Operation.Create,
                Status = Status.Success
            };
            try
            {
                var skillModel = new Skills();
                skillModel.MapFromViewModel(skill);
                var duplicateSkill = _skillRepository.GetFirstOrDefault(x => x.Name == skill.Name);
                if (duplicateSkill != null)
                {
                    result.Status = Status.Fail;
                    result.Message = SkillStatusNotification.DuplicateSkill;
                    result.Body = null;
                }
                else
                {
                    _skillRepository.Create(skillModel);
                    _skillRepository.SaveChanges();
                    result.Body = skillModel;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        public IResult DeleteSkill(int id)
        {
            throw new NotImplementedException();
        }

        public IResult GetAllSkill()
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var skills = new List<SkillViewModel>();
                var allSkills = _skillRepository.GetAll().ToList();
                result.Body = skills.MapFromModel<Skills, SkillViewModel>(allSkills);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        public IResult GetSkillById(int id)
        {
            var result = new Result
            {
                Operation = Operation.Read,
                Status = Status.Success
            };
            try
            {
                var skill = new SkillViewModel();
                var getSkill = _skillRepository.GetByID(id);
                result.Body = skill.MapFromModel(getSkill);
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }

        public IResult UpdateSkill(SkillViewModel skill)
        {
            var result = new Result
            {
                Operation = Operation.Update,
                Status = Status.Success
            };
            try
            {
                var skillModel = new Skills();
                skillModel.MapFromViewModel(skill);
                var duplicateSkill = _skillRepository.GetFirstOrDefault(x => x.Name == skill.Name);
                if (duplicateSkill != null)
                {
                    result.Status = Status.Fail;
                    result.Message = SkillStatusNotification.DuplicateSkill;
                    result.Body = null;
                }
                else
                {
                    _skillRepository.Update(skillModel);
                    _skillRepository.SaveChanges();
                    result.Body = skillModel;
                }
               
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Status = Status.Fail;
            }
            return result;
        }
    }
}
