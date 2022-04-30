import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { WorkFlow } from '../models/workflow.model';

@Injectable({ providedIn: 'root' })
export class WorkFlowsService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) {}

  editWorkFlow(workflow: WorkFlow, id:any): Observable<WorkFlow> {
    return this.http.put<WorkFlow>(this.baseUrl + 'Workflows/'+id, workflow);
  }

  getAllWorkFlows(): Observable<WorkFlow[]> {
    //to run local
      // return this.http.get<WorkFlow[]>(
      //   this.baseUrl + 'Workflows'
      // )

      //to run deployment
    return this.http.get<WorkFlow[]>(
     this.baseUrl + 'Workflows/GetWorkflows'
    );
  }
}
