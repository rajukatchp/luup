import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CreateWorkFlowService } from '../create-workflow/create-workflow.service';
import { WorkFlow } from '../models/workflow.model';
import { WorkFlowsService } from './workflows.service';

@Component({
  selector: 'app-workflows',
  templateUrl: './workflows.component.html',
  styleUrls: ['./workflows.component.scss'],
})
export class WorkFlowsComponent implements OnInit {
  displayedColumns: string[] = [
    'workflowName',
    'workflowDesc',
    'IsActive',
    'entity',
    'RecipientsTo',
    'type',
    'actions'
  ];
  selected = 'sandbox';
  workFlowsList: WorkFlow[] = [];
  dataSource = new MatTableDataSource<WorkFlow>();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private dataService: CreateWorkFlowService,
    private router: Router,
    private workFlowService: WorkFlowsService
  ) {}

  ngOnInit(): void {
    // this.dataService.getWorkFlowsList().subscribe((result) => {
    //   this.dataSource.data = result;
    //   this.workFlowsList = [...result];
    // });

    this.workFlowService.getAllWorkFlows().subscribe((result) => {
      this.dataSource.data = result;
      this.workFlowsList = [...result];
    });
  }


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  onFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getWorkFlowDetails(id: number) {
    this.router.navigate([
      'edit-notification',
      { workFlowID: id, pageMode: 'Edit' },
    ], {
      skipLocationChange: true,
    });
  }


  isActiveSlideToggle(event:MatSlideToggleChange, workflow: WorkFlow) {
    let tempWorkFlow: WorkFlow;
    console.log(event.checked, workflow);

    tempWorkFlow = {...workflow};
    tempWorkFlow.isActive = event.checked;
    this.saveWorkflow(tempWorkFlow);
    //this.saveWorkflow(workflow);
  }
  saveWorkflow(workflow : WorkFlow) {

    this.workFlowService.editWorkFlow(workflow, workflow.id)
    .subscribe((res: WorkFlow) =>
      console.log(res))
  }
}
