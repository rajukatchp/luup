export interface WorkFlow {
  id?: number;
  workflowName?: string; // missed call date
  workflowDesc?: string; //user should close the workflow on time
  frequency?: string;
  frequencyTime?: string;
  lastExecutedOn?: string;
  createdOn?: string;
  createdBy?: string;
  modifiedOn?: string;
  modifiedBy?: string;
  isActive?: boolean;
  entity?: string;
  adaptiveCardJSON?: string;
  actions?: ActionDataType[];
  conditions?: WorkflowCondition[];
  template?: {
    id?: number;
    entity?: string;
    title?: string;
    templateAdaptiveCardJSON?:string,
    templateTitle?:string,
    description?: string; // Description
    type?: string; //['outlook','team']
    createdOn?: string;
    modifiedOn?: string;
    createdBy?: string;
    modifiedBy?: string;
    recipientsTo?: string;
    recipientsCc?: string;
    recipientsBcc?: string;
  };
}
export interface ActionDataType {
  id?: number;
  title?: string;
  type?: string;
  api?: string;
  parameters?: string;
  successJSON?: string;
  errorJSON?: string;
}

export interface WorkflowCondition {
  id?: number;
  attributeName?: string;
  operator?: string;
  attributeValue?: string;
  conditionsOperator?: string;
}
