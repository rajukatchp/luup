import { Injectable } from '@angular/core';
import { from, Observable, of, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { WorkFlow } from '../models/workflow.model';
import { environment } from '../../environments/environment';

import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { workflowTemplateRecord } from '../models/workflowStaticData';

@Injectable({ providedIn: 'root' })
export class CreateWorkFlowService {
  baseUrl = environment.baseUrl;
  workFlowsList: WorkFlow[];
  currentWorkFlow: WorkFlow = {};

  constructor(private http: HttpClient) {}

  getAllWorkFlows(): Observable<WorkFlow> {
    return this.http.get(this.baseUrl + '/workFlows');
  }
  //https://luupwebapi.azurewebsites.net/api/Workflows/GetWorkflowById/%7BId%7D

  getCurentWorkFlow(id: number) {
    //to run deployment
    return this.http.get(this.baseUrl + 'Workflows/GetWorkflowById/' + id);

    //to run local
    //return this.http.get(this.baseUrl + 'Workflows/' + id);
  }

  getTemplateWorkFlow(entityName: string): Observable<WorkFlow> {
    let queryParams = new HttpParams();
    queryParams = queryParams.append("entityName", entityName);

    return this.http.get(this.baseUrl + 'Workflows/GetTemplatesByEntity/', {
      params: queryParams
    });
  }


  setWorkFlow(workFlowRecord: WorkFlow) {
    this.workFlowsList.push(workFlowRecord);
  }

  editWorkFlow(workflow: WorkFlow, id:any): Observable<WorkFlow> {
    return this.http.put<WorkFlow>(this.baseUrl + 'Workflows/'+id, workflow);
  }

  addWorkFlow(workflow: WorkFlow): Observable<WorkFlow> {
    return this.http.post<WorkFlow>(this.baseUrl + 'Workflows', workflow);
    //return this.http.post<WorkFlow>(this.baseUrl + 'Workflows', workflow);
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
       console.error('An error occurred:', error.error);
    } else {
        console.error(
        `Backend returned code ${error.status}, body was: `,
        error.error
      );
    }
    // Return an observable with a user-facing error message.
    return throwError(
      () => new Error('Something bad happened; please try again later.')
    );
  }
}
