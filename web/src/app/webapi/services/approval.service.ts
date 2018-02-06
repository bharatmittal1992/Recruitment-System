/* tslint:disable */
import { Injectable } from '@angular/core';
import {
  HttpClient, HttpRequest, HttpResponse, 
  HttpHeaders, HttpParams } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { Observable } from 'rxjs/Observable';
import { map } from 'rxjs/operators/map';
import { filter } from 'rxjs/operators/filter';

import { IResult } from '../models/iresult';
import { ApprovalEventRoleViewModel } from '../models/approval-event-role-view-model';


@Injectable()
export class ApprovalService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param approvalId - undefined
   */
  ApiApprovalGetApprovalEventsGetResponse(approvalId: number): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (approvalId != null) __params = __params.set("approvalId", approvalId.toString());
    let req = new HttpRequest<any>(
      "GET",
      this.rootUrl + `/api/Approval/GetApprovalEvents`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   * @param approvalId - undefined
   */
  ApiApprovalGetApprovalEventsGet(approvalId: number): Observable<IResult> {
    return this.ApiApprovalGetApprovalEventsGetResponse(approvalId).pipe(
      map(_r => _r.body)
    );
  }
  /**
   */
  ApiApprovalGetAllApprovalEventsGetResponse(): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      "GET",
      this.rootUrl + `/api/Approval/GetAllApprovalEvents`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   */
  ApiApprovalGetAllApprovalEventsGet(): Observable<IResult> {
    return this.ApiApprovalGetAllApprovalEventsGetResponse().pipe(
      map(_r => _r.body)
    );
  }
  /**
   */
  ApiApprovalGetAllApprovalEventRolesApprovalEventRolesGetResponse(): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      "GET",
      this.rootUrl + `/api/Approval/GetAllApprovalEventRoles/ApprovalEventRoles`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   */
  ApiApprovalGetAllApprovalEventRolesApprovalEventRolesGet(): Observable<IResult> {
    return this.ApiApprovalGetAllApprovalEventRolesApprovalEventRolesGetResponse().pipe(
      map(_r => _r.body)
    );
  }
  /**
   * @param approvalEventRoleViewModel - undefined
   */
  ApiApprovalCreateEventRolePostResponse(approvalEventRoleViewModel?: ApprovalEventRoleViewModel): Observable<HttpResponse<IResult>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = approvalEventRoleViewModel;
    let req = new HttpRequest<any>(
      "POST",
      this.rootUrl + `/api/Approval/createEventRole`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      filter(_r => _r instanceof HttpResponse),
      map(_r => {
        let _resp = _r as HttpResponse<any>;
        let _body: IResult = null;
        _body = _resp.body as IResult
        return _resp.clone({body: _body}) as HttpResponse<IResult>;
      })
    );
  }

  /**
   * @param approvalEventRoleViewModel - undefined
   */
  ApiApprovalCreateEventRolePost(approvalEventRoleViewModel?: ApprovalEventRoleViewModel): Observable<IResult> {
    return this.ApiApprovalCreateEventRolePostResponse(approvalEventRoleViewModel).pipe(
      map(_r => _r.body)
    );
  }}

export module ApprovalService {
}
