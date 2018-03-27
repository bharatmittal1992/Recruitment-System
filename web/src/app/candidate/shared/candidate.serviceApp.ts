import { Injectable } from '@angular/core';
import { Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import 'rxjs/Rx';

// Service
import { CandidateService } from '../../webapi/services/candidate.service';

@Injectable()
export class CandidateServiceApp {
    constructor(private apiCandidateSevice: CandidateService) {
    }

    addCandidate(candidateModel): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateAddCandidatePost(candidateModel).map(x => (x));
    }

    updateCandidate(candidateModel): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateUpdateCandidatePut(candidateModel).map(x => (x));
    }

    getAllCandidates(): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateGetAllCandidateGet().map(x => (x));
    }

    getCandidateById(candidateId): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateGetCandidateByIdGet(candidateId).map(x => (x));
    }

    addUserForCandidate(candidateAssignedUsers): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateAddUserForCandidatePost(candidateAssignedUsers).map(x => (x));
    }

    getAssignedUsersById(candidateId): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateGetAssignedUsersByIdGet(candidateId).map(x => (x));
    }

    getCandidatesCorrespondingToLoggedUser(userId): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateGetCandidatesCorrespondingToLoggedUserGet(userId).map(x => (x));
    }

    approvedForInterview(candidateId): Observable<any> {
        return this.apiCandidateSevice.ApiCandidateApprovedForInterviewPut(candidateId).map(x => (x));
    }

}
