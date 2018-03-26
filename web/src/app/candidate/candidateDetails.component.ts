import { Component, OnInit } from '@angular/core';
import { CandidateViewModel, ApprovalTransactionViewModel } from '../webapi/models';
import { Params, ActivatedRoute } from '@angular/router';
import { CandidateServiceApp } from './shared/candidate.serviceApp';
import { isNullOrUndefined } from 'util';
import { Status, ApprovalType } from '../app.enum';
import { DisplayMessageService } from '../shared/toastr/display.message.service';
import { QualificationsServiceApp } from '../admin/qualifications/shared/qualifications.serviceApp';
import { OpeningServiceApp } from '../opening/shared/opening.serviceApp';
import { Approvals } from '../webapi/models/approvals';
import { ApprovalServiceApp } from '../approval/shared/approval.serviceApp';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-candidatedetails',
    templateUrl: 'candidateDetails.component.html'
})

export class CandidateDetailsComponent implements OnInit {

    candidateModel: CandidateViewModel = {} as CandidateViewModel;
    approvalTransaction: ApprovalTransactionViewModel = {} as ApprovalTransactionViewModel;
    approval: any;
    opening: any;
    candidate: any;
    qualification: any;
    isDataAvailable = false;
    gender: any;

    constructor(private route: ActivatedRoute, private candidateServiceApp: CandidateServiceApp,
        private msgService: DisplayMessageService, private qualificationsServiceApp: QualificationsServiceApp,
        private openingServiceApp: OpeningServiceApp, private approvalServiceApp: ApprovalServiceApp,
        private translateService: TranslateService) { }

    ngOnInit() {
        this.candidate = this.candidateModel;
        this.approval = ApprovalType.Candidate;
        this.getCandidateById();
    }

    getCandidateById() {
        this.route.params.subscribe((params: Params) => {
            const candidateId = params['candidateId'];
            if (!isNullOrUndefined(candidateId)) {
                this.candidateServiceApp.getCandidateById(candidateId).subscribe(
                    (data) => {
                        if (data.status === Status.Success) {
                            this.candidateModel = data.body;
                            this.candidate = this.candidateModel;
                            this.getGender(this.candidateModel.gender);
                        } else {
                            this.msgService.showError('Error');
                        }
                        this.isDataAvailable = true;
                    }
                );
            }
        });
    }

    getGender(id) {
        const genderName = id === 1 ? 'CANDIDATE.MALE' : 'CANDIDATE.FEMALE';
        this.translateService.get(genderName).subscribe(
            (data) => {
                this.gender = data;
            }
        );
    }
}
