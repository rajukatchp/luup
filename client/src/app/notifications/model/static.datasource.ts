import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { AdaptiveCardType, CardActions, WorkFlow1 } from './notification.model';

const MissingDateCard = {
  type: 'AdaptiveCard',
  body: [
    {
      type: 'TextBlock',
      size: 'Medium',
      weight: 'Bolder',
      text: 'Missed close date',
      wrap: true,
      style: 'heading',
    },
    {
      type: 'TextBlock',
      text: 'Dear @opty.owner.name, An estimated due date @opty.duedate for an open opportunity @opty.name has passed. Please take below actions',
      wrap: true,
    },
  ],
  actions: [
    {
      type: 'Action.Submit',
     title: 'Update Due Date',
      tooltip: 'Pleaseupdatetheduedate',
      style: 'positive',
    },
    {
      type: 'Action.OpenUrl',
      title: 'Close Opportunity',
      style: 'positive',
    },
  ],
  $schema: 'http://adaptivecards.io/schemas/adaptive-card.json',
  version: '1.4',
};

@Injectable({
  providedIn: 'root',
})
export class StaticDataSource {
  private tempCardData: AdaptiveCardType | undefined;
  private workFlowList: WorkFlow1[];

  constructor() {
    this.workFlowList = [];
  }

  addToWorkFlowList(workflow: WorkFlow1) {
    this.workFlowList.push(workflow);
  }

  getNotifications(): Observable<WorkFlow1[]> {
    return from([this.workFlowList]);
  }

  createCardData(): any {}

  setCardData(
    cardTitle: string,
    cardDescription?: string,
    firstActionTitle?: string,
    secondActionTitle?: string
  ) {
    if (secondActionTitle === null) {
      secondActionTitle = 'Close Opportunity';
    }

    if (firstActionTitle === null) {
      firstActionTitle = 'Update Due Date';
    }

    let cardData: AdaptiveCardType | undefined;
    let bodyCardDataSecond: AdaptiveCardType | undefined;
    let bodyCardActionsFirst: CardActions | undefined;

    cardData = { ...MissingDateCard };
    console.log(cardData);

    // @ts-ignore
    cardData.body[0].text = cardTitle; //setting title
    // @ts-ignore
    cardData.body[1].text = cardDescription; // setting description

    // @ts-ignore
    cardData.actions[0].title = firstActionTitle;
    // @ts-ignore
    cardData.actions[1].title = secondActionTitle;

    this.tempCardData = { ...cardData };
  }

  getCardData(): any {
    return JSON.stringify(MissingDateCard);
  }
}
