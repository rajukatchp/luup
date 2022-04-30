
export interface WorkFlow1 {
  id?: number;
  workflowName?: string;
  workflowDesc?: string;
  frequency?: string;
  frequencyTime?: string;
  lastExecutedOn?: string;
  createdOn?: string;
  createdBy?:string;
  modifiedOn?: string;
  modifiedBy?:string;
  isActive?:boolean;
  RecipientsTo?: [];
  RecipientsCC?: [];
  Templatebody?: string;
  entity?: string;
}

export interface AdaptiveCardType  {
  id? : string;
  type? : string;
  size? : string;
  weight?: string;
  text?: string;
  wrap?: boolean;
  style?: string;
  placeholder?: string;
  isMultiline?: boolean;
  body?: AdaptiveCardType[];
  $schema?: string ;
  actions?:AdaptiveCardType[];
}


export interface CardActions {
  type?: string;
  card?: AdaptiveCardType;
  title?: string;
  style?: string;
  toootip?: string;

}


