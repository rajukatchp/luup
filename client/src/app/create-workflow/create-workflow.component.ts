import { Template } from '@angular/compiler/src/render3/r3_ast';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import * as AdaptiveCards from 'adaptivecards';
import { LoginService } from '../login/login.service';
import {
  ActionDataType,
  WorkFlow,
  WorkflowCondition,
} from '../models/workflow.model';
import { AdaptiveCardType } from '../notifications/model/notification.model';
import { StaticDataSource } from '../notifications/model/static.datasource';
import { CreateWorkFlowService } from './create-workflow.service';
import {MatChipInputEvent} from '@angular/material/chips';

const PAGEMODEEDIT = 'Edit';

@Component({
  selector: 'app-create-workflow',
  templateUrl: './create-workflow.component.html',
  styleUrls: ['./create-workflow.scss'],
})
export class CreateWorkFlowComponent implements OnInit {
  workFlowID: any;
  pageMode: any;
  currentWorkFlowRecord: WorkFlow;
  workFlowCardData: any;
  workFlowForm: FormGroup;
  notificationTemplateJSONData: any;
  recipientsToList: any = [];
  recipientsCcList: any = [];
  recipientsBccList: any = [];
  isConditionAdded: boolean = false;
  currentWorkFlowTemplate: AdaptiveCardType = {};
  workFlowData: WorkFlow;
  templateAdaptiveCardJSON: string;
  @ViewChild('cardContainer', { static: true }) cardContainer: ElementRef;

  actionTypes: string[] = [
    'Action.Submit',
    'Action.OpenUrl',
    'Action.ShowCard',
  ];
  ReceipientTypes: string[] = ['user@test.com', 'user2@test.com'];

  constructor(
    private dataService: StaticDataSource,
    private fb: FormBuilder,
    private createWorkFlowService: CreateWorkFlowService,
    private route: ActivatedRoute,
    private router: Router,
    private loginService: LoginService
  ) {
    this.workFlowID = this.route.snapshot.paramMap.get('workFlowID');
    this.pageMode = this.route.snapshot.paramMap.get('pageMode');
  }

  ngOnInit(): void {
    this.buildWorkFlowForm();
    const currentDate = new Date();
    let currentTime = currentDate.getTime();
    this.workFlowForm.controls.timePickerCtrl.patchValue(currentTime);
    if (this.pageMode === PAGEMODEEDIT) {

      this.getEditWorkFlowData();
    }


   if(this.pageMode!==PAGEMODEEDIT) {
    this.workFlowForm.controls.entityCtrl.valueChanges.subscribe(res => {
      if (res=== "lead") {
        this.getWorkFlowData(res);

      }
    })
  } else {
    this.workFlowForm.controls.entityCtrl.disable();
    this.workFlowForm.controls.selectTemplateCtrl.disable();
  }
  }

  buildWorkFlowForm() {
    this.workFlowForm = this.fb.group({
      templateTitleCtrl: null,
      templateDescriptionCtrl: null,
      workflowSummaryCtrl: null,
      entityCtrl: null,
      frequencyCtrl: null,
      outlookCtrl: null,
      dynamicsCtrl: null,
      teamCtrl: null,
      reciepientToCtrl: null,
      reciepientCcCtrl: null,
      recipientsBccCtrl: null,
      timePickerCtrl: null,
      selectTemplateCtrl: null,
      enableWorkFlowCtrl: null,
      actions: this.fb.array([]),
      workFlowConditions: this.fb.array([]),
    });
  }

  addActionControlsToNewForm() {
    let actionLengthinCard = this.getTemplateActionsLength();
    for( let i = 0 ; i< actionLengthinCard; i++) {
    this.actions.push(this.buildActionsData());

    }
  }

  addConditionControlsToNewForm() {
    this.workFlowConditions.push(this.buildConditionData());
  }

  addConditionActionControlsToEditForm(workflowData?: WorkFlow) {
    if (workflowData) {
      if (workflowData?.actions?.length) {
        workflowData.actions.forEach((action) =>
          this.actions.push(this.buildActionsData(action))
        );
      } else {
        this.addActionControlsToNewForm();
      }
    }

    if (workflowData?.conditions?.length) {
      workflowData.conditions.forEach((condition) =>
        this.workFlowConditions.push(this.buildConditionData(condition))
      );
    } else {
      this.addActionControlsToNewForm();
    }
  }

  //getting record from templated
  getWorkFlowData(selectedEntityName : string) {
    let seletectedEntity = selectedEntityName;
    let tempWorkFlow: WorkFlow  = this.getNewWorkFlowObject();
    this.createWorkFlowService
      .getTemplateWorkFlow(seletectedEntity)
      .subscribe((result: any) => {
        tempWorkFlow.template = result[0];
        this.currentWorkFlowRecord = {...tempWorkFlow};

        if (this.currentWorkFlowRecord !== undefined) {
          this.workFlowCardData = this.currentWorkFlowRecord?.template?.templateAdaptiveCardJSON;

          if (this.workFlowCardData !== null) {
            this.showPreview(this.currentWorkFlowRecord?.template?.templateAdaptiveCardJSON);
            this.templateAdaptiveCardJSON = this.workFlowCardData;
          }
          this.setFormValues(this.currentWorkFlowRecord);
          this.addActionControlsToNewForm();
          this.addConditionControlsToNewForm();
        }
      });

    //to add one more action
  }

  getEditWorkFlowData() {
    this.createWorkFlowService
      .getCurentWorkFlow(this.workFlowID)
      .subscribe((result: any) => {
        //for local run
        //this.currentWorkFlowRecord = result;

        //PRODRUN
        this.currentWorkFlowRecord = result[0];
        if (this.currentWorkFlowRecord !== undefined) {
          this.addConditionActionControlsToEditForm(this.currentWorkFlowRecord);
          this.workFlowCardData = this.currentWorkFlowRecord.adaptiveCardJSON;
          if (this.workFlowCardData !== null) {
            this.showPreview(this.workFlowCardData);
            this.templateAdaptiveCardJSON = this.workFlowCardData;
          }

          this.setFormValues(this.currentWorkFlowRecord);
        }
      });
  }
  get actions(): FormArray {
    return <FormArray>this.workFlowForm.get('actions');
  }

  get workFlowConditions(): FormArray {
    return <FormArray>this.workFlowForm.get('workFlowConditions');
  }

  buildActionsData(actionData?: ActionDataType): FormGroup {
    if (actionData) {
      return this.fb.group({
        id: actionData.id,
        type: actionData.type,
        title: actionData.title,
        api: actionData.api,
        parameters: actionData.parameters,
      });
    } else {
      return this.fb.group({
        type: '',
        title: '',
        api: '',
        parameters: '',
      });
    }
  }

  buildConditionData(conditionData?: WorkflowCondition): FormGroup {
    if (conditionData) {
      return this.fb.group({
        id: conditionData.id,
        conditionsOperator: conditionData.conditionsOperator,
        attributeName: conditionData.attributeName,
        operator: conditionData.operator,
        attributeValue: conditionData.attributeValue,
      });
    } else {
      return this.fb.group({
        conditionsOperator: '',
        attributeName: '',
        operator: '',
        attributeValue: '',
      });
    }
  }

  setFormValues(workFlowData: any) {


    if(this.pageMode === PAGEMODEEDIT) {
    this.workFlowForm.controls.entityCtrl.setValue(workFlowData.entity);
    }

    // setting team, outlook,dynamics controls
    if (workFlowData.template.type.indexOf('team') !== -1) {
      this.workFlowForm.controls.teamCtrl.setValue(true);
    }
    if (workFlowData.template.type.indexOf('outlook') !== -1) {
      this.workFlowForm.controls.outlookCtrl.setValue(true);
    }
    if (workFlowData.template.type.indexOf('dynamic365') !== -1) {
      this.workFlowForm.controls.dynamicsCtrl.setValue(true);
    }

    this.workFlowForm.controls.selectTemplateCtrl.setValue(
      workFlowData.template.templateTitle
    );

    this.workFlowForm.controls.workflowSummaryCtrl.setValue(
      workFlowData.workflowDesc
    );

    this.workFlowForm.controls.enableWorkFlowCtrl.setValue(
      workFlowData.isActive
    );

    //setting template title
    let templateTitle = this.getTemplateTitleFromCard();

    if (templateTitle && this.pageMode !== PAGEMODEEDIT) {
      this.workFlowForm.controls.templateTitleCtrl.setValue(
        templateTitle
      );
    } else {
      this.workFlowForm.controls.templateTitleCtrl.setValue(
        workFlowData.template.title
      );
    }

    let tempDescription = this.getTemplateDescriptionFromCard();

    if (tempDescription && this.pageMode !== PAGEMODEEDIT) {
      this.workFlowForm.controls.templateDescriptionCtrl.setValue(
        tempDescription
      );
    } else {
      this.workFlowForm.controls.templateDescriptionCtrl.setValue(
        workFlowData.template.description
      );
    }

    //setting email
    if (workFlowData.template.recipientsTo !== '') {
      let tempRecipientsToList = workFlowData.template.recipientsTo.split(',');
      tempRecipientsToList.forEach((element: string) => {
          this.recipientsToList.push(element);
      })

    }


    if (workFlowData.template.recipientsBcc !== '') {
      let tempRecipientsBccList = workFlowData.template.recipientsBcc.split(',');
      tempRecipientsBccList.forEach((element: string) => {
          this.recipientsBccList.push(element);
      })
    }

    if (workFlowData.template.recipientsCc !== '') {
      let tempRecipientsCcList = workFlowData.template.recipientsCc.split(',');
      tempRecipientsCcList.forEach((element: string) => {
          this.recipientsCcList.push(element);
      })

    }
    //setting emails end

    this.workFlowForm.controls.frequencyCtrl.setValue(workFlowData.frequency);
  }

  showPreview(workFlowCardeData?: any) {
    if (workFlowCardeData) {
      const jsonCardData: string = JSON.parse(workFlowCardeData);
      let card = new AdaptiveCards.AdaptiveCard();

      card.parse(jsonCardData);
      let renderedCard = card.render();

      this.cardContainer.nativeElement.append(renderedCard);
    }
  }

  updatePreview() {
    const jsonCardData: string = this.getUpdateAdaptiveCardTemplate();
    let card = new AdaptiveCards.AdaptiveCard();

    card.parse(jsonCardData);
    let renderedCard = card.render();
    this.cardContainer.nativeElement.children[0].remove();
    this.cardContainer.nativeElement.append(renderedCard);
  }



  addRecipientsToList(recipientsTo: string): void {
    this.recipientsToList?.push(recipientsTo);

    //this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  removerecipientsToFromList(recipient: string) : void {
    const index = this.recipientsToList.indexOf(recipient)
    if (index >= 0) {
      this.recipientsToList.splice(index, 1);
    }
  }

  addRecipientsCcList(recipientsCc: string): void {
    this.recipientsCcList.push(recipientsCc);
  }


  removeRecipientsCcList(recipient: string): void {
    const index = this.recipientsCcList.indexOf(recipient)
    if (index >= 0) {
      this.recipientsCcList.splice(index, 1);
    }
  }

  addRecipientsBCcList(recipientsCc: string): void {
    this.recipientsBccList.push(recipientsCc);
  }

  removeRecipientsBCcList(recipient: string): void {
    const index = this.recipientsBccList.indexOf(recipient)
    if (index >= 0) {
      this.recipientsBccList.splice(index, 1);
    }
  }

  getTemplateTitleFromCard() {
    if (this.workFlowCardData) {
      let tempTemplate = JSON.parse(this.workFlowCardData);
      return tempTemplate.body[0].text;
    }
  }

  getTemplateDescriptionFromCard() {
    if (this.workFlowCardData) {
      let tempTemplate = JSON.parse(this.workFlowCardData);

      return tempTemplate.body[1].text;
    }
  }

  getTemplateActionsLength() {

    if (this.workFlowCardData) {
      let tempTemplate = JSON.parse(this.workFlowCardData);
     return tempTemplate.actions.length;
    }
  }

  /**updated adaptive card value from form controls */
  getUpdateAdaptiveCardTemplate() {
    if (this.workFlowCardData) {
       let tempTemplate = JSON.parse(this.workFlowCardData);
      console.log(tempTemplate);
      tempTemplate.body[0].text =
        this.workFlowForm.controls.templateTitleCtrl.value;
      tempTemplate.body[1].text =
        this.workFlowForm.controls.templateDescriptionCtrl.value;
      console.log(this.actions.controls[0].value);
      tempTemplate.actions = [];
      this.actions.controls.forEach((control) => {
        console.log(control.value);
        tempTemplate.actions.push(control.value);
      });

      return tempTemplate;
    }
  }

  getTemplateType(): string {
    let templateType = '';

    if (this.workFlowForm.controls.teamCtrl.value === true) {
      if (templateType !== '') {
        templateType = templateType + ',' + 'team';
      } else {
        templateType = 'team';
      }
    }

    if (this.workFlowForm.controls.outlookCtrl.value === true) {
      if (templateType !== '') {
        templateType = templateType + ',' + 'outlook';
      } else {
        templateType = 'outlook';
      }
    }

    if (this.workFlowForm.controls.dynamicsCtrl.value === true) {
      if (templateType !== '') {
        templateType = templateType + ',' + 'dynamics365';
      } else {
        templateType = 'dynamics365';
      }
    }

    return templateType;
  }

  getNewWorkFlowObject(): WorkFlow {

    let tempWorkFlowObject: WorkFlow = {};
    tempWorkFlowObject.actions = [];
    tempWorkFlowObject.conditions = [];
    tempWorkFlowObject.template = {};
    return tempWorkFlowObject;
  }

  saveForm(): void {
    let tempWorkFlow: WorkFlow = this.getNewWorkFlowObject()


    let workflowTemplate = this.getUpdateAdaptiveCardTemplate();
    let currentDate = new Date();
    //conver Date time to UTC
    const dateTime = new Date(this.workFlowForm.controls.timePickerCtrl.value);
    tempWorkFlow.frequencyTime = dateTime.toISOString();

    tempWorkFlow.lastExecutedOn = currentDate.toISOString();
    tempWorkFlow.workflowName =
      this.workFlowForm.controls.templateTitleCtrl.value;
    tempWorkFlow.frequency = this.workFlowForm.controls.frequencyCtrl.value;
    tempWorkFlow.entity = this.workFlowForm.controls.entityCtrl.value;

    tempWorkFlow.isActive = this.workFlowForm.controls.enableWorkFlowCtrl.value;

    tempWorkFlow.workflowDesc =
      this.workFlowForm.controls.workflowSummaryCtrl.value;

    //tempWorkFlow.entity = this.workFlowForm.controls.entityCtrl.value || '';

    if (workflowTemplate !== null) {
      tempWorkFlow!.adaptiveCardJSON = JSON.stringify(workflowTemplate);
    } else {
      tempWorkFlow!.adaptiveCardJSON = '';
    }


    //all template fields

    tempWorkFlow!.template!.description =
      this.workFlowForm.controls.templateDescriptionCtrl.value;

    tempWorkFlow!.template!.createdBy = this.loginService.getLoginName();

    tempWorkFlow!.template!.title =
      this.workFlowForm.controls.templateTitleCtrl.value;

    tempWorkFlow!.template!.type = this.getTemplateType();

    tempWorkFlow!.template!.recipientsTo = this.recipientsToList.join();
    tempWorkFlow!.template!.recipientsCc = this.recipientsCcList.join();
    tempWorkFlow!.template!.recipientsBcc = this.recipientsBccList.join();
    tempWorkFlow!.template!.templateAdaptiveCardJSON = this.templateAdaptiveCardJSON;
    tempWorkFlow!.template!.entity = this.workFlowForm.controls.entityCtrl.value;
    tempWorkFlow!.template!.templateTitle = this.workFlowForm.controls.selectTemplateCtrl.value;


    //action fields
    let tempActionRecord: ActionDataType = {};

    this.actions.controls.forEach((control) => {
      tempActionRecord.id = control.value.id;
      tempActionRecord.api = control.value.api;
      tempActionRecord.parameters = control.value.parameters;
      tempActionRecord.title = control.value.title;
      tempActionRecord.type = control.value.type;
      tempWorkFlow?.actions?.push({ ...tempActionRecord });
    });

    //condition fields
    let tempWorkflowCondition: WorkflowCondition = {};
    this.workFlowConditions.controls.forEach((control) => {
      tempWorkflowCondition.id = control.value.id;
      tempWorkflowCondition.attributeName = control.value.attributeName;
      tempWorkflowCondition.attributeValue = control.value.attributeValue;
      tempWorkflowCondition.operator = control.value.operator;
      tempWorkflowCondition.conditionsOperator =
        control.value.conditionsOperator;
      tempWorkFlow?.conditions?.push({ ...tempWorkflowCondition });
    });



    if (this.pageMode === PAGEMODEEDIT) {
      //edit fowrkflow
      tempWorkFlow.modifiedBy = this.loginService.getUserEmail();
      if (this.currentWorkFlowRecord.id) {
        tempWorkFlow.id = this.currentWorkFlowRecord.id;
      }
      if (this.currentWorkFlowRecord?.template?.id) {
        if( tempWorkFlow.template) {
          tempWorkFlow.template.id = this.currentWorkFlowRecord.template.id;
        }
      }

      this.createWorkFlowService
        .editWorkFlow(tempWorkFlow, this.workFlowID)
        .subscribe((res) => {
          console.log(PAGEMODEEDIT, res);
          this.router.navigate(['notificatons'], {
            skipLocationChange: true,
          });
        });
    } else {

      //New workflow
      tempWorkFlow.createdBy = this.loginService.getUserEmail();
      this.createWorkFlowService.addWorkFlow(tempWorkFlow).subscribe((res) => {
        console.log('new record', res);
        this.router.navigate(['notificatons'], {
          skipLocationChange: true,
        });
      });
    }
  }

  resetForm(): void {}
  addCondition() {
    this.workFlowConditions.push(this.buildConditionData());
  }
}
