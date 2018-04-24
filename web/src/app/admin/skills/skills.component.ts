import { Component, OnInit } from '@angular/core';
import { SkillsServiceApp } from './shared/skills.serviceApp';
import { SkillViewModel } from '../../webapi/models/skill-view-model';
import { DisplayMessageService } from '../../shared/toastr/display.message.service';

@Component({
  selector: 'app-skills',
  templateUrl: 'skills.component.html',
  styleUrls: ['shared/skills.scss']
})

export class SkillsComponent implements OnInit {

  skillsModel: SkillViewModel = {} as SkillViewModel;
  skills: SkillViewModel[] = [] as SkillViewModel[];
  isCreateOrUpdate: boolean;
  submitted = false;
  isSkillExists: boolean = false;

  constructor(private skillsServiceApp: SkillsServiceApp,private displayMessage: DisplayMessageService) {
  }

  ngOnInit() {
    this.listSkill();
    this.isCreateOrUpdate = true;
  }

  onSubmit(skillsform) {
    this.submitted = true;
    if (skillsform.valid) {
      if (this.skillsModel['skillId'] === undefined) {
        this.skillsServiceApp.addSkill(this.skillsModel).subscribe(
          (data) => {
            if (data.status === 0) {
            } else {
            }
            this.listSkill();
          }
        );
      } else {
        this.skillsServiceApp.updateSkill(this.skillsModel).subscribe(
          (data) => {
            this.listSkill();
          }
        );
      }
    }
  }
  checkNameExitsOnBlur() {
    let $this = this;
    let skillName = $this.skillsModel.name;
    this.skills.every(function (skill) {
      if (skill['name'] === skillName) {
        $this.isSkillExists = true;
        return false;
      }else{
        $this.isSkillExists = false;
        return true;
      }
    });
  }

  listSkill() {
    this.skillsServiceApp.listSkill().subscribe((data) => {
        if (data) {
          this.skills = data.body;
        }
      });
  }

  deleteSkills(i) {
    this.skills.splice(i, 1);
    this.skillsServiceApp.deleteSkill().subscribe((data) => {
      if (data) {
      }
    });
  }

  editSkills(skills) {
    this.skillsModel.skillId = skills.skillId;
    this.skillsModel.name = skills.name;
    this.skillsModel.description = skills.description;
    this.isCreateOrUpdate = false;
  }
}
